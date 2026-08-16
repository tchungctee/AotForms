using AotForms;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    internal class SilentAimLegit
    {
        private static Thread aimLockThread;
        private static CancellationTokenSource cancellationTokenSource;
        private static readonly string encryptedAimKey = "U2lsZW50S2V5";
        private static Entity lastTarget = null;
        private static readonly Random random = new Random();
        private static int writeCount = 0;

        private static string DecryptString(string encrypted)
        {
            var base64EncodedBytes = Convert.FromBase64String(encrypted);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        internal static void Start()
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            aimLockThread = new Thread(() => Work(cancellationTokenSource.Token))
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal,
                Name = GenerateRandomThreadName()
            };
            aimLockThread.Start();
        }

        internal static void Stop()
        {
            cancellationTokenSource?.Cancel();
        }

        private static string GenerateRandomThreadName()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var nameChars = new char[12];
            for (int i = 0; i < nameChars.Length; i++)
                nameChars[i] = chars[random.Next(chars.Length)];
            return new string(nameChars);
        }

        private static Vector3 CalculateAimPoint(Entity target)
        {
            // Tính điểm chest từ head và offset
            Vector3 chestPos = target.Head + new Vector3(0, Config.ChestOffsetY, 0);

            // Random chọn head hoặc chest theo tỷ lệ
            double randomValue = random.NextDouble();

            if (randomValue < Config.HeadRate)
            {
                return target.Head; // Aim head
            }
            else
            {
                return chestPos; // Aim chest
            }
        }

        public static void Work(CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var writeBuffer = new List<(ulong address, Vector3 value)>();

            while (!cancellationToken.IsCancellationRequested)
            {
                writeCount++;

                if (Debugger.IsAttached)
                {
                    continue;
                }

                if (!Config.Slient2)
                {
                    lastTarget = null;
                    continue;
                }

                if ((WinAPI.GetAsyncKeyState(Keys.LButton) & 0x8000) == 0)
                {
                    lastTarget = null;
                    continue;
                }

                if (Core.Width == -1 || Core.Height == -1 || !Core.HaveMatrix)
                {
                    continue;
                }

                float minFovDistanceSquared = float.MaxValue;
                var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);
                var fov = 360f;
                var fovSquared = fov * fov;
                Entity target = null;

                foreach (var entity in Core.Entities.Values)
                {
                    if (entity.IsDead || (Config.IgnoreKnocked && entity.IsKnocked))
                        continue;

                    var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    if (head2D.X < 1 || head2D.Y < 1)
                        continue;

                    var x = head2D.X - screenCenter.X;
                    var y = head2D.Y - screenCenter.Y;
                    var crosshairDistSquared = x * x + y * y;

                    if (crosshairDistSquared > fovSquared)
                        continue;

                    if (crosshairDistSquared < minFovDistanceSquared)
                    {
                        minFovDistanceSquared = crosshairDistSquared;
                        target = entity;
                    }
                }
                lastTarget = target;

                if (target != null)
                {
                    try
                    {
                        if (InternalMemory.Read<bool>(Core.LocalPlayer + Offsets.sAim2, out bool success1))
                        {
                            if (InternalMemory.Read<uint>(Core.LocalPlayer + Offsets.sAim1, out uint baseAddress))
                            {
                                if (baseAddress != 0)
                                {
                                    if (InternalMemory.Read<Vector3>(baseAddress + 0x38, out Vector3 offsetPos))
                                    {
                                        // SỬA CHỖ NÀY - dùng HeadRate và ChestRate
                                        Vector3 aimPoint = CalculateAimPoint(target);

                                        Vector3 aimPointWithOffset = aimPoint + new Vector3(
                                            (float)(random.NextDouble() - 0.5) * 0.1f,
                                            (float)(random.NextDouble() - 0.5) * 0.1f,
                                            (float)(random.NextDouble() - 0.5) * 0.1f
                                        );

                                        var delta = aimPointWithOffset - offsetPos;
                                        writeBuffer.Clear();
                                        writeBuffer.Add((address: (ulong)baseAddress + 0x2C, value: delta));

                                        if (random.NextDouble() > 0.1)
                                        {
                                            foreach (var write in writeBuffer)
                                            {
                                                InternalMemory.Write(write.address, write.value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        lastTarget = null;
                        continue;
                    }
                }

                Thread.Yield();
                if (random.NextDouble() < 0.02)
                {
                    Thread.Yield();
                }
            }
        }
    }
}
