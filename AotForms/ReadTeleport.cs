using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using AotForms;

namespace Client
{
    internal static class RealTeleport
    {
        private static Task teleTask;
        private static CancellationTokenSource cts = new();
        private static bool isRunning = false;
        private static Entity currentTarget = null;

        internal static void Work()  // ← ĐỔI THÀNH Work()
        {
            if (isRunning) return;

            cts = new CancellationTokenSource();
            isRunning = true;

            teleTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        if (!Config.RealTeleport || Core.LocalPlayer == 0)
                        {
                            currentTarget = null;
                            await Task.Delay(100, cts.Token);
                            continue;
                        }

                        // Tìm enemy gần nhất
                        var target = FindClosestEnemy();
                        if (target != null && target != currentTarget)
                        {
                            currentTarget = target;
                            PerformRealTeleport(target);
                        }

                        await Task.Delay(50, cts.Token);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Teleport error: {ex.Message}");
                        await Task.Delay(100, cts.Token);
                    }
                }
            }, cts.Token);
        }

        private static Entity FindClosestEnemy()
        {
            if (Core.Entities == null || !Core.Entities.Any()) return null;

            return Core.Entities.Values
                .Where(e => !e.IsDead &&
                           !e.IsKnocked &&
                           e.IsEnemy &&
                           Vector3.Distance(Core.LocalMainCamera, e.Head) <= Config.TeleportRange)
                .OrderBy(e => Vector3.Distance(Core.LocalMainCamera, e.Head))
                .FirstOrDefault();
        }

        private static void PerformRealTeleport(Entity enemy)
        {
            try
            {
                // Teleport đến vị trí HEAD của enemy (để behind)
                Vector3 teleportPosition = CalculateTeleportPosition(enemy.Head, enemy.Root);

                // Thay đổi vị trí player controller (REAL TELEPORT)
                if (InternalMemory.Read(Core.LocalPlayer + Offsets.MainCameraTransform, out uint cameraTransform) && cameraTransform != 0)
                {
                    InternalMemory.Write<Vector3>(cameraTransform + 0x80, teleportPosition);
                }

                // Thay đổi vị trí root bone (backup method)
                if (InternalMemory.Read(Core.LocalPlayer + (uint)Bones.Root, out uint rootBone) && rootBone != 0)
                {
                    if (InternalMemory.Read(rootBone + 0x8, out uint transformVal) && transformVal != 0 &&
                        InternalMemory.Read(transformVal + 0x8, out uint transformObj) && transformObj != 0 &&
                        InternalMemory.Read(transformObj + 0x20, out uint matrixVal) && matrixVal != 0)
                    {
                        InternalMemory.Write<Vector3>(matrixVal + 0x80, teleportPosition);
                    }
                }

                Console.WriteLine($"Teleported to enemy: {enemy.Name} at {teleportPosition}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Teleport failed: {ex.Message}");
            }
        }

        private static Vector3 CalculateTeleportPosition(Vector3 enemyHead, Vector3 enemyRoot)
        {
            Vector3 directionToEnemy = Vector3.Normalize(enemyHead - Core.LocalMainCamera);
            Vector3 behindEnemy = enemyRoot - (directionToEnemy * 2.0f);
            behindEnemy.Y += 1.0f;
            return behindEnemy;
        }

        internal static void TeleportToPosition(Vector3 targetPosition)
        {
            try
            {
                if (Core.LocalPlayer == 0) return;

                if (InternalMemory.Read(Core.LocalPlayer + Offsets.MainCameraTransform, out uint cameraTransform) && cameraTransform != 0)
                {
                    InternalMemory.Write<Vector3>(cameraTransform + 0x80, targetPosition);
                }

                if (InternalMemory.Read(Core.LocalPlayer + (uint)Bones.Root, out uint rootBone) && rootBone != 0)
                {
                    if (InternalMemory.Read(rootBone + 0x8, out uint transformVal) && transformVal != 0 &&
                        InternalMemory.Read(transformVal + 0x8, out uint transformObj) && transformObj != 0 &&
                        InternalMemory.Read(transformObj + 0x20, out uint matrixVal) && matrixVal != 0)
                    {
                        InternalMemory.Write<Vector3>(matrixVal + 0x80, targetPosition);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Position teleport failed: {ex.Message}");
            }
        }

        internal static void TeleportToTeammate()
        {
            var teammate = Core.Entities.Values
                .Where(e => !e.IsDead && !e.IsKnocked && !e.IsEnemy && e.Address != Core.LocalPlayer)
                .OrderBy(e => Vector3.Distance(Core.LocalMainCamera, e.Head))
                .FirstOrDefault();

            if (teammate != null)
            {
                Vector3 telePos = teammate.Root;
                telePos.Y += 1.0f;
                TeleportToPosition(telePos);
            }
        }

        internal static void Stop()
        {
            if (!isRunning) return;

            cts.Cancel();
            try
            {
                teleTask?.Wait(1000);
            }
            catch { }

            currentTarget = null;
            isRunning = false;
        }
    }
}