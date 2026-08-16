using AotForms;
using Memory;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal static class FlyMe
    {
        private static Task upTask;
        private static CancellationTokenSource cts = new();
        private static bool isRunning = false;

        // Lưu vị trí hiện tại để hiển thị ngoài UI
        public static Vector3 CurrentPosition { get; private set; }

        // Biến để lưu vị trí bay ban đầu
        private static Vector3? originalFlyPosition = null;

        internal static void Work()
        {
            if (isRunning) return;
            isRunning = true;

            upTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        // Kiểm tra đã bật tính năng bay hay chưa
                        if (!Config.teliport)
                        {
                            originalFlyPosition = null; // Reset khi tắt bay
                            await Task.Delay(10, cts.Token);
                            continue;
                        }

                        // Đọc các cấp con trỏ đến ma trận transform
                        if (!InternalMemory.Read(Core.LocalPlayer + (uint)Bones.Root, out uint rootBonePtr) || rootBonePtr == 0) continue;
                        if (!InternalMemory.Read(rootBonePtr + 0x8, out uint transform1) || transform1 == 0) continue;
                        if (!InternalMemory.Read(transform1 + 0x8, out uint transform2) || transform2 == 0) continue;
                        if (!InternalMemory.Read(transform2 + 0x20, out uint matrixPtr) || matrixPtr == 0) continue;

                        // Đọc vị trí hiện tại
                        if (!InternalMemory.Read(matrixPtr + 0x80, out Vector3 currentPos)) continue;

                        // Nếu chưa có vị trí bay ban đầu, lưu lại vị trí khi bật bay
                        if (!originalFlyPosition.HasValue)
                        {
                            // Đặt độ cao mong muốn (thấp hơn mặt đất một chút để bay lơ lửng)
                            float targetHeight = Config.flymeHeight; // Ví dụ: 1.5f để bay thấp
                            originalFlyPosition = new Vector3(currentPos.X, targetHeight, currentPos.Z);
                        }

                        // Giữ nguyên vị trí bay (ổn định, không bị rơi)
                        Vector3 newPos = originalFlyPosition.Value;

                        // Chỉ cập nhật Y nếu cần thiết (giữ độ cao ổn định)
                        if (Math.Abs(currentPos.Y - newPos.Y) > 0.1f)
                        {
                            currentPos.Y = newPos.Y;
                        }

                        // Giữ nguyên X, Z để không di chuyển trừ khi người chơi di chuyển
                        // (game engine sẽ tự cập nhật X, Z khi di chuyển)

                        // Ghi lại vị trí mới vào game
                        InternalMemory.Write(matrixPtr + 0x80, currentPos);

                        // Cập nhật vị trí để hiển thị ngoài UI
                        CurrentPosition = currentPos;
                    }
                    catch
                    {
                        // Bỏ qua lỗi
                    }

                    await Task.Delay(10, cts.Token);
                }
            }, cts.Token);
        }

        internal static void Stop()
        {
            if (!isRunning) return;

            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();
            isRunning = false;
            originalFlyPosition = null;
        }
    }
}