using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fear
{
    enum MoralMode { Normal, Panic, Anger};

    enum HealthState { Healthy, Wounded, HeavyWounded, Dead}

    class FearObject
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        public IEnumerable<FearObject> AllEnemies { get; set; }
        public IEnumerable<FearObject> AllFriends { get; set; }

        public MoralMode Moral { get; set; }

        public HealthState Health { get; set; }

        public float Shield { get; set; }

        public float Sword { get; set; }

        public float Boots { get; set; }

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

            e.Graphics.FillRectangle(Brush, P.X, P.Y, Width, Height);
            if (Fear > 0)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, P.X, P.Y, 3, 3);
            }

            if (Stamina < 33)
            {
                e.Graphics.FillRectangle(Brushes.Purple, P.X , P.Y, 1, 1);
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
            if (random.Next(0, 3) == 0)
            {
                int frieNormal = 0, friendPanic = 0, enemyNormal = 0, enemyPanic = 0;
                int frieDead = 0;

                foreach (var enemy in AllEnemies.Where(f => Distance(P, f.P) < 30))
                {
                    if (enemy.Moral == MoralMode.Panic)
                        enemyPanic++;
                    else
                        enemyNormal++;
                }

                foreach (var friend in AllFriends.Where(f => Distance(P, f.P) < 30))
                {
                    if (friend.Moral == MoralMode.Panic)
                        friendPanic++;
                    else
                        frieNormal++;
                    if (friend.IsDead)
                        frieDead++;
                }
                if (friendPanic > 2)
                {
                    float likelihoodToPanic = (float)(friendPanic - enemyPanic) * enemyNormal / frieNormal * enemyNormal / frieNormal;
                    if (enemyNormal < 0)
                        likelihoodToPanic /= 2F;

                    if (random.Next(0, 100) + 3 * likelihoodToPanic > 100)
                    {
                        Moral = MoralMode.Panic;
                        Fear = 100;
                    }
                }
            }

            switch (Moral)
            {
                case MoralMode.Normal: DoNormalProgram(); break;
                case MoralMode.Panic: DoPanicProgram(); break;
                case MoralMode.Anger: DoAngerProgram(); break;
            }
        }

        private void DoAngerProgram()
        {
        }

        private void DoPanicProgram()
        {
            PanicFromTarget();
            Fear -= random.Next(0, 3);

            if (Fear < 0)
            {
                Moral = MoralMode.Normal;
            }
        }

        private void DoNormalProgram()
        {
            if (Target == null || Target.IsDead || random.Next(0,10) == 0)
            {
                Target = GetClosesLeavingTarget();
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

        private FearObject GetClosesLeavingTarget()
        {
            float minD = 10000000;
            FearObject closest = null;

            foreach (var en in AllEnemies.Where(en => !en.IsDead))
            {
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
            float Dx = p1.X - p2.X;
            float Dy = p1.Y - p2.Y;
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
            if (random.Next(0, 1000) + 100 * likelyhoodToHit > 900)
            {
                Target.Health = HealthState.Dead;

                SwapBetterAmunition(Target);

                return;
            }

            if (random.Next(0, 1000) + 100 * likelyhoodToHit > 900)
            {
                Target.Stamina -= 50; // kinda wound
                Target.Target = this;
                return;
            }

            if (random.Next(0, 1000) + 100 * likelyhoodToHit > 900)
            {
                Target.Moral = MoralMode.Panic;
                Target.Fear = 100;
                return;
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
                if (random.Next(0, 100) > 90) Stamina = Fitness;
                Fitness *= 0.9995F; // Every time you exaust yourslf it is harder for you to get to normal

                return 0;
            }

            float baseSpeed = 5;

            if (Moral == MoralMode.Panic)
            {
                baseSpeed *= 1.5F;
            }

            return baseSpeed * (Stamina) / (100);
        }

        internal void MoveTowardsTarget()
        {
            float step = GetStep();
            if (step == 0) return;

            float Dx = Target.P.X - P.X;
            float Dy = Target.P.Y - P.Y;

            float distance = DistanceToTarget;

            if (distance < 5 + step)
            {
                step = Math.Max(0.5F, distance - 5);
            }

            ReduceStamina(step);

            float coeff = step / distance;
            P.X += coeff * Dx;
            P.Y += coeff * Dy;

            RestricMovement();
        }

        private void ReduceStamina(float step)
        {
            if (random.Next(0, 100) + 5F*(Fitness / 100F) > 90F )
            {
                return;
            }

            float baseSpeed = 5;
            if (step < baseSpeed/2)
            {
                Stamina *= 0.98F;
            }
            else if (step < baseSpeed)
            {
                Stamina *= 0.97F;
            }
            else
            {
                Stamina *= 0.90F;
            }
        }

        internal void PanicFromTarget()
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
