using AotForms;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal static class DIVEKILL
    {
        private static Task tpTask;
        private static CancellationTokenSource cts = new();
        private static bool isRunning = false;
        private static int teleportDelay = 15;
        // XOÁ DÒNG hideDepth vì đã dùng Config

        internal static void Work()
        {
            if (isRunning) return;
            isRunning = true;

            if (cts == null || cts.IsCancellationRequested)
                cts = new CancellationTokenSource();

            tpTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (!Config.teliport)
                    {
                        await Task.Delay(5, cts.Token);
                        continue;
                    }

                    try
                    {
                        ulong rawLocalPlayer = Core.LocalPlayer;
                        if (rawLocalPlayer == 0)
                        {
                            await Task.Delay(teleportDelay, cts.Token);
                            continue;
                        }

                        uint localPlayer = (uint)rawLocalPlayer;

                        if (!InternalMemory.Read<uint>(localPlayer + (uint)Bones.Root, out uint rootPtr) || rootPtr == 0)
                            continue;
                        if (!InternalMemory.Read<uint>(rootPtr + 0x8, out uint t1) || t1 == 0)
                            continue;
                        if (!InternalMemory.Read<uint>(t1 + 0x8, out uint t2) || t2 == 0)
                            continue;
                        if (!InternalMemory.Read<uint>(t2 + 0x20, out uint matrixPtr) || matrixPtr == 0)
                            continue;
                        if (!InternalMemory.Read<Vector3>(matrixPtr + 0x80, out Vector3 currentPos))
                            continue;

                        // DÙNG Config.DiveKillDepth
                        Vector3 undergroundPos = new Vector3(
                            currentPos.X,
                            currentPos.Y - Config.DiveKillDepth,  // ← DÙNG CONFIG
                            currentPos.Z
                        );

                        InternalMemory.Write<Vector3>(matrixPtr + 0x80, undergroundPos);

                        await Task.Delay(teleportDelay, cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch { }

                    await Task.Delay(teleportDelay, cts.Token);
                }
            }, cts.Token);
        }

        internal static void Stop()
        {
            if (!isRunning) return;
            try { cts.Cancel(); } catch { }
            isRunning = false;
        }

        // XOÁ 2 HÀM SetHideDepth và GetHideDepth vì đã dùng Config trực tiếp
    }
}