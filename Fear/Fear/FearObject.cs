using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fear
{
    public enum Moral { Normal, Afraid, Panic, Charge};

    public enum HealthState { Healthy, Wounded, HeavyWounded, Dead}

    public class FearObject
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        public FUnit EnemyTeam;

        public FUnit MyTeam;

        public Moral Moral { get; set; }

        public HealthState Health { get; set; }

        public float Shield { get; set; }

        public float Sword { get; set; }

        public FPoint P { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Brush Brush { get; set; }

        private float _stamina;

        public float Stamina { get { return _stamina; } set { _stamina = Math.Max(0, value); } }

        public float Fitness { get; set; }

        public float BaseFitness { get; set; }

        public int Fear { get; set; }

        public System.Drawing.Color Color { get; set; }

        public FearObject Target { get; set; }

        public float DistanceToTarget {
            get
            {
                return Distance(P, Target.P);
            }
        }

        public bool IsDead { get { return Health == HealthState.Dead; } }

        public void FillMe(PaintEventArgs e)
        {
            if (Health == HealthState.Dead)
            {
                //e.Graphics.FillRectangle(Brushes.Black, P.X, P.Y, 4, 4);
                return;
            }

            e.Graphics.FillRectangle(Brush, P.X, P.Y, Width+1, Height+1);
            e.Graphics.FillRectangle(Brush, P.X+3, P.Y+3, Width-1, Height-1);

            if (Moral == Moral.Panic)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, P.X, P.Y, 3, 3);
            }

            if (Moral == Moral.Afraid)
            {
                e.Graphics.FillRectangle(Brushes.LightGreen, P.X+1, P.Y+1, 4, 4);
            }

            if (Stamina < 33)
            {
                e.Graphics.FillRectangle(Brushes.White, P.X+2 , P.Y+2, 2, 2);
            }
        }

        private void RestricMovement()
        {
            P.X = Math.Min(1000, P.X);
            P.Y = Math.Min(500, P.Y);

            P.X = Math.Max(0, P.X);
            P.Y = Math.Max(0, P.Y);
        }

        public void MoveRandomly(float step)
        {
            float xMove = (float)random.NextDouble() * step;
            float yMove = (float)Math.Sqrt(step * step - xMove * xMove);

            P.X += (random.Next(0, 2) == 1) ? xMove : -xMove;
            P.Y += (random.Next(0, 2) == 1) ? yMove : -yMove;

            RestricMovement();
        }

        internal void Act()
        {
            if (Moral != Moral.Panic && random.Next(0, 5) == 0 && MyTeam.PanicingCount > 2)
            {
                int frieNormal = 0, friendPanic = 0, enemyNormal = 0, enemyPanic = 0;
                float radius = 20;

                int minIndexB = FindIndex(MyTeam.ArrayX, P.X - radius);
                int maxIndexB = FindIndex(MyTeam.ArrayX, P.X + radius);

                for (int i = minIndexB; i <= maxIndexB; i++)
                {
                    if (Math.Abs(MyTeam.ArrayX[i].P.Y - P.Y) > radius) continue;
                    if (Distance(MyTeam.ArrayX[i].P, P.X, P.Y) < radius)
                    {
                        if (MyTeam.ArrayX[i].Moral == Moral.Panic)
                            friendPanic++;
                        else
                            frieNormal++;
                    }
                }

                minIndexB = FindIndex(EnemyTeam.ArrayX, P.X - radius);
                maxIndexB = FindIndex(EnemyTeam.ArrayX, P.X + radius);

                for (int i = minIndexB; i <= maxIndexB; i++)
                {
                    if (Math.Abs(EnemyTeam.ArrayX[i].P.Y - P.Y) > radius) continue;
                    if (Distance(EnemyTeam.ArrayX[i].P, P.X, P.Y) < radius)
                    {
                        if (EnemyTeam.ArrayX[i].Moral == Moral.Panic)
                            enemyPanic++;
                        else
                            enemyNormal++;
                    }
                }

                float likelihoodToPanic = (float)(friendPanic - enemyPanic) * enemyNormal / frieNormal * enemyNormal / frieNormal;
                if (enemyNormal < 0)
                    likelihoodToPanic /= 4F;

                if (random.Next(0, 100) + 3 * likelihoodToPanic > 100)
                {
                    Moral = Moral.Panic;
                    Fear = 100;
                }
            }

            switch (Moral)
            {
                case Moral.Panic: DoPanicProgram(); break;
                case Moral.Afraid: DoAfraidProgram(); break;
                case Moral.Charge:
                case Moral.Normal: DoNormalProgram(); break;
            }
        }

        private void DoAfraidProgram()
        {
            int decision = random.Next(0, 5);
            if (decision == 0)
            {
                Moral = Moral.Normal;
            }
            else if (decision < 4)
            {
                RetreatFromTarget();
            }
        }

        private static bool CloserThan(FPoint p1, FPoint p2, float radius)
        {
            if (Math.Abs(p1.X - p2.X) > radius) return false;
            if (Math.Abs(p1.Y - p2.Y) > radius) return false;

            return Distance(p1, p2) < radius;
        }

        private void DoPanicProgram()
        {
            RetreatFromTarget();
            Fear -= random.Next(0, 3);

            if (Fear < 0)
            {
                Moral = Moral.Normal;
            }
        }

        private void DoNormalProgram()
        {
            if (Target == null || Target.IsDead || random.Next(0, 50) == 0)
            {
                Target = GetClosesLeavingTarget(EnemyTeam.Objects);
            }

            if (Target == null)
            {
                return;
            }

            if (DistanceToTarget > 5)
            {
                MoveTowardsTarget();
            }
            else
            {
                EngageWithTarget();
            }
        }

        public FearObject GetClosesLeavingTarget(IEnumerable<FearObject> enemies)
        {
            float minD = 10000000;
            FearObject closest = null;

            foreach (var en in enemies.Where(en => !en.IsDead))
            {
                if (Math.Abs(this.P.X - en.P.X) > minD) continue;
                if (Math.Abs(this.P.Y - en.P.Y) > minD) continue;
                float d = Distance(this.P, en.P);
                if (minD > d)
                {
                    minD = d;
                    closest = en;
                }
            }

            return closest;
        }

        public static float Distance(FPoint p1, FPoint p2)
        {
            return Distance(p1, p2.X, p2.Y);
        }

        public static float Distance(FPoint p1, float x, float y)
        {
            float Dx = p1.X - x;
            float Dy = p1.Y - y;
            return (float)Math.Sqrt(Dx * Dx + Dy * Dy);
        }

        private void EngageWithTarget()
        {
            // target is enemy for now
            if (random.Next(0, 10) > 7)
            {
                Target.Target = this;
            }

            float likelyhoodToHit = Stamina * Sword * Sword / Target.Stamina / Target.Shield / Target.Shield;

            // fight:
            if (random.Next(0, 1000) + 200 * likelyhoodToHit > 990)
            {
                Target.Health = HealthState.Dead;

                SwapBetterAmunition(Target);

                return;
            }

            if (random.Next(0, 1000) + 200 * likelyhoodToHit > 990)
            {
                Target.Stamina -= 50; // kinda wound
                Target.Target = this;
                return;
            }

            if (random.Next(0, 1000) + 200 * likelyhoodToHit > 990)
            {
                Target.Moral = Moral.Panic;
                Target.Fear = 100;
                return;
            }

            if (Target.Moral == Moral.Afraid)
            {
                Target.Target = this;
                Target.Moral = Moral.Normal;
            }
        }

        public void SwapBetterAmunition(FearObject target)
        {
            if (Shield < target.Shield)
            {
                float my = Shield;
                Shield = target.Shield;
                target.Shield = my;
            }

            if (Sword < target.Sword)
            {
                float my = Sword;
                Sword = target.Sword;
                target.Sword = my;
            }
        }

        private float GetStep()
        {
            if (Stamina < 33)
            {
                if (random.Next(0, 100) > 95) Stamina = Fitness;
                Fitness *= 0.985F; // Every time you exaust yourslf it is harder for you to get to normal

                if (Fitness < 40 && random.Next(0, 100) > 95)
                    Fitness = 55;

                return 0;
            }

            float baseSpeed = 5;

            if (Moral == Moral.Panic)
            {
                baseSpeed *= 1.5F;
            }

            if (Moral == Moral.Charge)
                return baseSpeed * 3 / 4;

            return baseSpeed / 2;
        }

        internal void MoveTowardsTarget()
        {
            float step = GetStep();
            if (step == 0) return;

            float Dx = Target.P.X - P.X;
            float Dy = Target.P.Y - P.Y;

            float distance = DistanceToTarget;

            if (DistanceToTarget < 30)
            {
                if (Moral != Moral.Charge && random.Next(0, 7) < 6 )
                    Moral = Moral.Afraid;
                else 
                    Moral = Moral.Charge;
            }
            else
                Moral = Moral.Normal;

            if (distance < 5 + step)
            {
                step = Math.Max(0.5F, distance - 5);
            }

            ReduceStamina(step);

            float coeff = step / distance;
            float newX = P.X + coeff * Dx;
            float newY = P.Y + coeff * Dy;

            if (IsThereAnyBodyAtTheFinalPoint(newX, newY, 2))
            {
                P.X = 0.8F * newX + 0.2F * P.X;
                P.Y = 0.8F * newY + 0.2F * P.Y;
                //MoveRandomly(0.5F);
                MoveSidewaysRandomly(newX - P.X, newY - P.Y);
            }
            else
            {
                P.X = newX;
                P.Y = newY;
            }

            if (IsThereAnyBodyAtTheFinalPoint(P.X, P.Y, 0.5F))
                MoveSidewaysRandomly(newX - P.X, newY - P.Y);

            if (IsThereAnyBodyAtTheFinalPoint(P.X, P.Y, 0.25F))
                MoveSidewaysRandomly(newX - P.X, newY - P.Y);

            RestricMovement();
        }

        private void MoveSidewaysRandomly(float dx, float dy)
        {
            float portion = 1F;

            if (random.Next(0, 2) == 0)
            {
                P.X += dy * portion; P.Y -= dx * portion;
            }
            else
            {
                P.X -= dy * portion; P.Y += dx * portion;
            }
        }

        private bool IsThereAnyBodyAtTheFinalPoint(float x, float y, float radius)
        {
            int minIndexX = FindIndex(MyTeam.ArrayX, P.X - radius);
            int maxIndexX = FindIndex(MyTeam.ArrayX, P.X + radius);

            int minIndexY = FindIndex(MyTeam.ArrayY, P.X - radius);
            int maxIndexY = FindIndex(MyTeam.ArrayY, P.X + radius);

            if (maxIndexX - minIndexX < maxIndexY - minIndexY)
            {
                for (int i = minIndexX; i <= maxIndexX; i++)
                {
                    if (Distance(MyTeam.ArrayX[i].P, x, y) < radius)
                        return true;
                }
            }
            else
            {
                for (int i = minIndexY; i <= maxIndexY; i++)
                {
                    if (Distance(MyTeam.ArrayY[i].P, x, y) < radius)
                        return true;
                }
            }

            return false;
        }

        public static int FindIndex(FearObject[] array, float v)
        {
            int min = 0;
            int max = array.Length - 1;
            int iterations = 0;

            while (min <= max)
            {
                iterations++;

                int mid = (min + max) / 2;
                float midV = array[mid].P.X;

                if (midV == v)
                    return mid;

                if (mid >= array.Length - 2)
                    return mid;

                if (midV < v && array[mid+1].P.X >= v)
                    return mid;

                else if (v < midV)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return 0;
        }

        private void ReduceStamina(float step)
        {
            if (Moral == Moral.Normal && random.Next(0, 100) + 20F*(Fitness / 100F) > 90F )
            {
                return;
            }

            float baseSpeed = 5;
            if (step <= baseSpeed/2)
            {
                Stamina -= 0.1F;
            }
            else if (step < baseSpeed * 0.75)
            {
                Stamina *= 0.985F;
            }
            else if (step < baseSpeed * 0.9)
            {
                Stamina *= 0.98F;
            }
            else
            {
                Stamina *= 0.9F;
            }
        }

        internal void RetreatFromTarget()
        {
            float step = GetStep();
            if (step == 0) return;

            float Dx = -(Target.P.X - P.X);
            float Dy = -(Target.P.Y - P.Y);

            float distance = (float)Math.Sqrt(Dx * Dx + Dy * Dy);
            ReduceStamina(step);

            float coeff = step / distance;
            P.X += coeff * Dx;
            P.Y += coeff * Dy;

            RestricMovement();
        }
    }
}
