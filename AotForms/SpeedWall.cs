//using AotForms;
//using System;
//using System.Numerics;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Client
//{
//    internal static class SpeedWall
//    {
//        private static Task? worker;
//        private static CancellationTokenSource cts = new();
//        private static bool isRunning = false;

//        private static uint GetLocalMatrixPtr()
//        {
//            bool ok1 = InternalMemory.Read<uint>(Core.LocalPlayer + (uint)Bones.Root, out var localRootBonePtr);
//            bool ok2 = InternalMemory.Read<uint>(localRootBonePtr + 0x8, out var localTransform);
//            bool ok3 = InternalMemory.Read<uint>(localTransform + 0x8, out var localTransformObj);
//            bool ok4 = InternalMemory.Read<uint>(localTransformObj + 0x20, out var matrixPtr);

//            if (ok1 && ok2 && ok3 && ok4 && matrixPtr != 0)
//                return matrixPtr;
//            return 0;
//        }

//        private static bool TryGetLocalPosition(out Vector3 pos)
//        {
//            pos = Vector3.Zero;
//            if (!InternalMemory.Read<uint>(Core.LocalPlayer + (uint)Bones.Root, out var rootPtr) || rootPtr == 0)
//                return false;
//            return Transform.GetNodePosition(rootPtr, out pos);
//        }

//        private static Vector3 GetFlatForward()
//        {
//            var m = Core.CameraMatrix;
//            Vector3 forward = new Vector3(-m.M13, -m.M23, -m.M33);
//            Vector3 right = new Vector3(m.M11, m.M21, m.M31);
//            forward.Y = 0f;
//            right.Y = 0f;
//            if (forward.LengthSquared() < 1e-4f) forward = Vector3.UnitZ;
//            if (right.LengthSquared() < 1e-4f) right = Vector3.UnitX;
//            forward = Vector3.Normalize(forward);
//            return forward;
//        }

//        private static Vector3 GetFlatRight()
//        {
//            var m = Core.CameraMatrix;
//            Vector3 forward = new Vector3(-m.M13, -m.M23, -m.M33);
//            Vector3 right = new Vector3(m.M11, m.M21, m.M31);
//            forward.Y = 0f;
//            right.Y = 0f;
//            if (forward.LengthSquared() < 1e-4f) forward = Vector3.UnitZ;
//            if (right.LengthSquared() < 1e-4f) right = Vector3.UnitX;
//            right = Vector3.Normalize(right);
//            return right;
//        }

//        public static void Work()
//        {
//            if (isRunning) return;
//            isRunning = true;
//            cts = new CancellationTokenSource();

//            worker = Task.Run(async () =>
//            {
//                try
//                {
//                    while (!cts.Token.IsCancellationRequested)
//                    {
//                        if (!Config.Speed || !Core.IsValidCamera())
//                        {
//                            await Task.Delay(50, cts.Token);
//                            continue;
//                        }

//                        bool anyMove = KeyHelper.IsKeyDown(Keys.W) || KeyHelper.IsKeyDown(Keys.A) ||
//                                       KeyHelper.IsKeyDown(Keys.S) || KeyHelper.IsKeyDown(Keys.D);
//                        if (!anyMove)
//                        {
//                            await Task.Delay(10, cts.Token);
//                            continue;
//                        }

//                        uint matrixPtr = GetLocalMatrixPtr();
//                        if (matrixPtr == 0 || !TryGetLocalPosition(out var curPos))
//                        {
//                            await Task.Delay(10, cts.Token);
//                            continue;
//                        }

//                        Vector3 fwd = GetFlatForward();
//                        Vector3 right = GetFlatRight();
//                        Vector3 moveDir = Vector3.Zero;

//                        if (KeyHelper.IsKeyDown(Keys.W)) moveDir += fwd;
//                        if (KeyHelper.IsKeyDown(Keys.S)) moveDir -= fwd;
//                        if (KeyHelper.IsKeyDown(Keys.D)) moveDir += right;
//                        if (KeyHelper.IsKeyDown(Keys.A)) moveDir -= right;

//                        if (moveDir.LengthSquared() < 1e-6f)
//                        {
//                            await Task.Delay(1, cts.Token);
//                            continue;
//                        }

//                        moveDir = Vector3.Normalize(moveDir);

//                        float baseStep = Math.Clamp(Config.TeleportSpeed, 0.02f, 0.30f);
//                        float sprintMul = KeyHelper.IsKeyDown(Keys.ShiftKey) ? 1.5f : 1.0f;
//                        float step = baseStep * 8.0f * sprintMul;
//                        step = Math.Clamp(step, 0.10f, 1.20f);

//                        Vector3 offset = moveDir * step;

//                        Vector3 newPos = new Vector3(curPos.X + offset.X,
//                                                     curPos.Y,
//                                                     curPos.Z + offset.Z);

//                        InternalMemory.Write<Vector3>(matrixPtr + 0x80, newPos);

//                        await Task.Delay(16, cts.Token);
//                    }
//                }
//                catch { }
//                finally
//                {
//                    isRunning = false;
//                }
//            }, cts.Token);
//        }

//        public static void Stop()
//        {
//            if (!isRunning) return;
//            try { cts.Cancel(); } catch { }
//            isRunning = false;
//        }
//    }
//}
