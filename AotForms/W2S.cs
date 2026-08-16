using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AotForms
{
    internal static class W2S
    {
        internal static Vector2 WorldToScreen(Matrix4x4 m, Vector3 pos, float width, float height)
        {
            float clipX = pos.X * m.M11 + pos.Y * m.M21 + pos.Z * m.M31 + m.M41;
            float clipY = pos.X * m.M12 + pos.Y * m.M22 + pos.Z * m.M32 + m.M42;
            float clipW = pos.X * m.M14 + pos.Y * m.M24 + pos.Z * m.M34 + m.M44;

            if (clipW < 0.1f)
                return new Vector2(-1, -1);

            float ndcX = clipX / clipW;
            float ndcY = clipY / clipW;

            float screenX = (width / 2f) * (ndcX + 1f);
            float screenY = (height / 2f) * (1f - ndcY);

            return new Vector2(screenX, screenY);
        }

    }
}
