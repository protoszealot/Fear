using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear
{
    public class Formations
    {
        static Random rand = new Random(Guid.NewGuid().GetHashCode());

        public static void BuildFormation(IEnumerable<FearObject> soldiers, int x1, int x2)
        {
            // 3 rectangulars
            int cnt = soldiers.Count();
            int y1 = 10, y2 = 50, y3 = 70, y4 = 110, y5 = 150, y6 = 190;

            for (int i = 0; i < cnt; i++)
            {
                var obj = soldiers.ElementAt(i);
                if (i < cnt / 3)
                {
                    obj.P = new FPoint { X = rand.Next(x1, x2), Y = rand.Next(y1, y2) };
                }
                else if (i < 2* cnt / 3)
                {
                    obj.P = new FPoint { X = rand.Next(x1, x2), Y = rand.Next(y3, y4) };
                }
                else
                {
                    obj.P = new FPoint { X = rand.Next(x1, x2), Y = rand.Next(y5, y6) };
                }

            }
        }
    }
}
