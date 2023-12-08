using System;
using System.Numerics;

namespace ConsoleBros
{
    public struct AABB
    {
        public Vector2 center;
        public Vector2 halfsize;
        public AABB (Vector2 center, Vector2 halfsize)
        {
            this.center = center;
            this.halfsize = halfsize;
        }

        public bool Overlaps(AABB other)
        {
            if (MathF.Abs(center.X - other.center.X) > halfsize.X + other.halfsize.X) return false;
            if (MathF.Abs(center.Y - other.center.Y) > halfsize.Y + other.halfsize.Y) return false;
            return true;
        }
    }
}
