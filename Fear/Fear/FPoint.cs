using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear
{
    public class FPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        internal static void Minus(FPoint p1, FPoint p2, FPoint res)
        {
            res.X = p2.X - p1.X;
            res.Y = p2.Y - p1.Y;
        }
    }
}
