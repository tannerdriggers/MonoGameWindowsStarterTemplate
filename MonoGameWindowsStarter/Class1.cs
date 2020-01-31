using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return !(X > other.X + other.Width
                || X + this.Width < other.X
                || Y > other.Y + other.Height
                || Y + this.Height < other.Y);
        }
    }

}
