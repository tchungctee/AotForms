using System.Numerics;
using AotForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal static class AimbotLegit
    {
        private static CancellationTokenSource _cts;
        private static Entity _currentTarget;
        private static uint _originalAimTargetValue;

        private static readonly Random _random = new Random();
        private static DateTime _keyHeldStartTime = DateTime.MinValue;
        private static bool _isAimKeyHeld = false;

        // Để tránh đổi target liên tục (flick)
        private static DateTime _lastTargetLockTime = DateTime.MinValue;
        private const int TargetLockDurationMs = 300; // Giữ target ít nhất 0.3s

        public static Entity CurrentTarget => _currentTarget;

        public static void Work()
        {
            if (_cts != null)
                return; // Đang chạy rồi

            _cts = new CancellationTokenSource();
            Task.Run(() => Loop(_cts.Token));
        }

        public static void Stop()
        {
            _cts?.Cancel();
            _cts = null;
            ReleaseAimAndRestore();
        }

        private static async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (!Config.AimLegit || !Core.HaveMatrix)
                    {
                        ReleaseAimAndRestore();
                        await Task.Delay(200, token);
                        continue;
                    }

                    bool isAimKeyPressedNow = (WinAPI.GetAsyncKeyState(Config.AimbotKey) & 0x8000) != 0;

                    if (isAimKeyPressedNow)
                    {
                        if (!_isAimKeyHeld)
                            _keyHeldStartTime = DateTime.Now;

                        float effectiveDelay = Math.Max(Config.AimbotDelayHold, 0);

                        if ((DateTime.Now - _keyHeldStartTime).TotalMilliseconds >= effectiveDelay)
                        {
                            if (_currentTarget == null || _currentTarget.IsDead)
                                FindAndSetNewTarget();

                            if (_currentTarget != null && !_currentTarget.IsDead)
                                PerformSmoothedAim();
                        }
                    }
                    else
                    {
                        ReleaseAimAndRestore();
                    }

                    _isAimKeyHeld = isAimKeyPressedNow;

                    // Giảm tải CPU
                    int delay = Math.Clamp(Config.AIRender, 5, 30);
                    await Task.Delay(delay, token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LỖI] AimLegitV2: {ex.Message}");
                    ReleaseAimAndRestore();
                    await Task.Delay(100, token);
                }
            }

            ReleaseAimAndRestore();
        }

        private static void FindAndSetNewTarget()
        {
            // Nếu target còn tốt và chưa hết thời gian lock thì giữ nguyên
            if (_currentTarget != null && !_currentTarget.IsDead)
            {
                if ((DateTime.Now - _lastTargetLockTime).TotalMilliseconds < TargetLockDurationMs &&
                    IsCrosshairNearTarget(_currentTarget))
                    return;
            }

            _currentTarget = FindBestTarget();
            _originalAimTargetValue = 0;
            _lastTargetLockTime = DateTime.Now;
        }

        private static void PerformSmoothedAim()
        {
            if (_currentTarget == null || _currentTarget.IsDead) return;

            if (!IsCrosshairNearTarget(_currentTarget))
            {
                ReleaseAimAndRestore();
                return;
            }

            // Chọn điểm ngắm cố định cho target hiện tại (đầu hoặc ngực)
            Vector3 aimPoint = _random.NextDouble() < 0.25
                ? _currentTarget.Head
                : _currentTarget.Head;

            Vector2 targetScreen = W2S.WorldToScreen(Core.CameraMatrix, aimPoint, Core.Width, Core.Height);
            if (float.IsNaN(targetScreen.X) || float.IsNaN(targetScreen.Y)) return;

            // Lấy tâm màn hình
            Vector2 centerScreen = new(Core.Width / 2f, Core.Height / 2f);

            // Smooth aim bằng Lerp
            float smoothFactor = Math.Clamp(Config.Smoothness, 1f, 10f);
            Vector2 smoothed = Vector2.Lerp(centerScreen, targetScreen, 1f / smoothFactor);

            // Giới hạn góc nhìn (tránh out FOV)
            if (Vector2.Distance(centerScreen, smoothed) > Config.SilentFov)
                return;

            SetAimTargetTransform();
        }

        private static void SetAimTargetTransform()
        {
            if (_currentTarget == null) return;

            nuint aimTargetAddress = _currentTarget.Address + 0x4A8;
            nuint sourceTransformAddress = _currentTarget.Address + 0x54;

            try
            {
                if (_originalAimTargetValue == 0 &&
                    InternalMemory.Read<uint>(aimTargetAddress, out uint currentValue))
                {
                    _originalAimTargetValue = currentValue;
                }

                if (InternalMemory.Read<uint>(sourceTransformAddress, out uint visibleTransform) && visibleTransform != 0)
                {
                    InternalMemory.Write(aimTargetAddress, visibleTransform);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AimWrite] Lỗi ghi bộ nhớ: {ex.Message}");
            }
        }

        private static void ReleaseAimAndRestore()
        {
            try
            {
                if (_currentTarget != null && _originalAimTargetValue != 0)
                {
                    nuint aimTargetAddress = _currentTarget.Address + 0x4A8;

                    if (InternalMemory.Read<uint>(aimTargetAddress, out uint currentValue) &&
                        currentValue != _originalAimTargetValue)
                    {
                        InternalMemory.Write(aimTargetAddress, _originalAimTargetValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AimRestore] {ex.Message}");
            }

            _currentTarget = null;
            _originalAimTargetValue = 0;
        }

        private static Entity FindBestTarget()
        {
            Entity bestTarget = null;
            float closestDist = float.MaxValue;
            Vector2 centerScreen = new(Core.Width / 2f, Core.Height / 2f);

            foreach (var entity in Core.Entities.Values.ToList())
            {
                if (entity == null || entity.Address == 0 || entity.IsDead)
                    continue;
                if (Config.IgnoreKnocked && entity.IsKnocked)
                    continue;

                var screenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                if (float.IsNaN(screenPos.X) || float.IsNaN(screenPos.Y))
                    continue;

                // Giới hạn trong FOV
                float dist2D = Vector2.Distance(centerScreen, screenPos);
                if (dist2D > Config.SilentFov)
                    continue;

                // Ưu tiên gần tâm hơn
                if (dist2D < closestDist)
                {
                    closestDist = dist2D;
                    bestTarget = entity;
                }
            }

            return bestTarget;
        }

        private static bool IsCrosshairNearTarget(Entity target)
        {
            if (target == null) return false;

            var screenPos = W2S.WorldToScreen(Core.CameraMatrix, target.Head, Core.Width, Core.Height);
            if (float.IsNaN(screenPos.X) || float.IsNaN(screenPos.Y)) return false;

            Vector2 centerScreen = new(Core.Width / 2f, Core.Height / 2f);
            return Vector2.Distance(centerScreen, screenPos) <= Config.SilentFov;
        }
    }
}
