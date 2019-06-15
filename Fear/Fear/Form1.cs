using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fear
{
    public partial class Form1 : Form
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        bool pause = false;
        bool end = false;
        int RedTeamStartCnt = 40;
        int BlueTeamStartCnt = 100;
        int ticks = 0;

        bool IsSelecting = false;
        float X0, Y0;

        float Money = 0;

        public static FUnit BlueTeam = new FUnit();
        public static FUnit RedTeam = new FUnit();

        List<FearObject> Selected = null;

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            BlueTeam.Init();
            RedTeam.Init();

            for (int i = 0; i < BlueTeamStartCnt; i++)
            {
                var soldier = GetRandomSoldier(Brushes.BlueViolet);
                BlueTeam.Objects.Add(soldier);

                soldier.P = new FPoint { X = rand.Next(10, 25), Y = rand.Next(300, 400) };
            }

            for (int i = 0; i < RedTeamStartCnt; i++)
            {
                var soldier = GetRandomSoldier(Brushes.Red);
                RedTeam.Objects.Add(soldier);

                soldier.P = new FPoint { X = rand.Next(400, 425), Y = rand.Next(300, 400) };
            }

            SetTeamsAndEnemies();
        }

        private void SetTeamsAndEnemies()
        {
            foreach (var fo in BlueTeam.Objects)
            {
                fo.EnemyTeam = RedTeam;
                fo.MyTeam = BlueTeam;
            }

            foreach (var fo in RedTeam.Objects)
            {
                fo.EnemyTeam = BlueTeam;
                fo.MyTeam = RedTeam;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var fo in BlueTeam.Objects)
            {
                fo.FillMe(e);
            }

            foreach (var fo in RedTeam.Objects)
            {
                fo.FillMe(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pause || end) return;

            BlueTeam.Update();
            RedTeam.Update();

            foreach (var fo in BlueTeam.Objects.Where(f => !f.IsDead))
            {
                fo.Act();
            }

            foreach (var fo in RedTeam.Objects.Where(f => !f.IsDead))
            {
                fo.Act();
            }

            int blueCounter = BlueTeam.Objects.Where(fo => !fo.IsDead).Count();
            int redCounter = RedTeam.Objects.Where(fo => !fo.IsDead).Count();

            UpdateStats();

            if (blueCounter == 0)
            {
                timer1.Stop();
                if (!end)
                {
                    //MessageBox.Show("Defeat!!!");
                    end = true;
                }
            }

            if (redCounter == 0)
            {
                timer1.Stop();
                if (!end)
                {
                    //MessageBox.Show("Victory!!!");
                    Money += RedTeam.Objects.Count * 3;
                    MoneyTB.Text = Money.ToString();

                    // for free
                    for (int i = 0; i < 10; i++)
                        BlueTeam.Objects.Add(GetRandomSoldier(Brushes.BlueViolet));

                    BlueTeam.Objects.Add(GetEliteRandomSoldier(Brushes.BlueViolet));

                    foreach (var fo in BlueTeam.Objects)
                    {
                        fo.BaseFitness += rand.Next(0, 3);
                    }

                    BlueTeam.Objects = BlueTeam.Objects.Where(s => !s.IsDead).ToList();
                    var dead = RedTeam.Objects.Where(s => s.IsDead).Union(BlueTeam.Objects.Where(s => s.IsDead));
                    int deadCnt = dead.Count();

                    // take Amunition From the dead
                    for (int i = 0; i < 3000; i++)
                    {
                        var sol = BlueTeam.Objects.ElementAt(rand.Next(0, BlueTeam.Objects.Count));
                        var corpse = dead.ElementAt(rand.Next(0, deadCnt));
                        sol.SwapBetterAmunition(corpse);
                    }

                    UpdateStats();

                    end = true;
                }
            }

            if(++ticks % 3 == 0)
                Invalidate();
        }

        private string GetStatForTeam(int counter, List<FearObject> team)
        {
            float Stamina = team.Where(fo => !fo.IsDead).Average(fo => fo.Stamina);
            float Fitness = team.Where(fo => !fo.IsDead).Average(fo => fo.Fitness);
            float Sword = team.Where(fo => !fo.IsDead).Average(fo => fo.Sword);
            float Shield = team.Where(fo => !fo.IsDead).Average(fo => fo.Shield);
            float Fit = team.Where(fo => !fo.IsDead).Average(fo => fo.BaseFitness);

            return string.Format("Cnt={0}, Sta={1}, Fit={2}, Swo={3}, Shi={4}, BF={5}", counter, Stamina, Fitness, Sword, Shield, Fit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pause == false)
            {
                timer1.Stop();
                pause = true;
                button1.Text = "Resume";
            }
            else if (pause == true)
            {
                timer1.Start();
                pause = false;
                button1.Text = "Pause";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RedTeam.Reset();

            RedTeamStartCnt += 10;

            int x = rand.Next(5,15);

            int y = rand.Next(200, 400);

            foreach ( var fo in BlueTeam.Objects)
            {
                //fo.P = new FPoint { X = x + rand.Next(10, 25), Y = y + rand.Next(20, 40) };
                fo.Stamina = 100;
                fo.Fitness = fo.BaseFitness;
                fo.Moral = Moral.Normal;
                fo.Fear = 0;
            }

            Formations.BuildFormation(BlueTeam.Objects, 5, 25);

            for (int i = 0; i < RedTeamStartCnt; i++)
            {
                var soldier = GetRandomSoldier(Brushes.Red);
                RedTeam.Objects.Add(soldier);

                //soldier.P = new FPoint { X = rand.Next(400, 425), Y = rand.Next(300, 400) };
            }
            Formations.BuildFormation(RedTeam.Objects, 500, 525);

            SetTeamsAndEnemies();

            end = false;
            timer1.Start();
        }

        FearObject GetRandomSoldier(Brush brush) {
            return new FearObject
            {
                P = new FPoint { X = 0, Y = 0 },
                Height = 5,
                Width = 5,
                Brush = brush,
                Stamina = 100,
                Fitness = 65 + rand.Next(0, 10),
                BaseFitness = 65 + rand.Next(0, 10),
                Sword = 5 + rand.Next(0, 20),
                Shield = 5 + rand.Next(0, 20),
                Moral = Moral.Normal
            };
        }

        FearObject GetProfessionalRandomSoldier(Brush brush)
        {
            return new FearObject
            {
                P = new FPoint { X = 0, Y = 0 },
                Height = 5,
                Width = 5,
                Brush = brush,
                Stamina = 100,
                Fitness = 75 + rand.Next(0, 10),
                BaseFitness = 75 + rand.Next(0, 10),
                Sword = 25 + rand.Next(0, 20),
                Shield = 25 + rand.Next(0, 20),
                Moral = Moral.Normal
            };
        }

        FearObject GetEliteRandomSoldier(Brush brush)
        {
            return new FearObject
            {
                P = new FPoint { X = 0, Y = 0 },
                Height = 6,
                Width = 6,
                Brush = brush,
                Stamina = 100,
                Fitness = 80 + rand.Next(0, 10),
                BaseFitness = 80 + rand.Next(0, 10),
                Sword = 35 + rand.Next(0, 20),
                Shield = 35 + rand.Next(0, 20),
                Moral = Moral.Normal
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for(int i = 0; i < 10; i++)
                    BlueTeam.Objects.Add(GetRandomSoldier(Brushes.BlueViolet));
            }
            UpdateStats();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Money >= BlueTeam.Objects.Count)
            {
                Money -= BlueTeam.Objects.Count;

                foreach (var fo in BlueTeam.Objects)
                {
                    fo.BaseFitness += rand.Next(1, 3);
                }
            }
            UpdateStats();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for (int i = 0; i < 10; i++)
                {
                    float worstShield = BlueTeam.Objects.Min(fo => fo.Shield);

                    foreach (var fo in BlueTeam.Objects)
                    {
                        if (fo.Shield == worstShield)
                        {
                            fo.Shield = 50;
                            break;
                        }
                    }
                }
            }
            UpdateStats();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for (int i = 0; i < 10; i++)
                {
                    float worstSword = BlueTeam.Objects.Min(fo => fo.Sword);

                    foreach (var fo in BlueTeam.Objects)
                    {
                        if (fo.Sword == worstSword)
                        {
                            fo.Sword = 50;
                            break;
                        }
                    }
                }
            }

            UpdateStats();
        }

        private void UpdateStats()
        {
            MoneyTB.Text = Money.ToString();

            int blueCounter = BlueTeam.Objects.Where(fo => !fo.IsDead).Count();
            int redCounter = RedTeam.Objects.Where(fo => !fo.IsDead).Count();

            if (blueCounter != 0)
                teamBlueCounter.Text = GetStatForTeam(blueCounter, BlueTeam.Objects);

            if (redCounter != 0)
                teamRedCounter.Text = GetStatForTeam(redCounter, RedTeam.Objects);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for (int i = 0; i < 3; i++)
                {
                    BlueTeam.Objects.Add(GetEliteRandomSoldier(Brushes.BlueViolet));
                }
            }
            UpdateStats();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Do nothing it we're not selecting an area.
            if (!IsSelecting) return;

            // Save the new point.
            float X1 = e.X;
            float Y1 = e.Y;

            if (Selected != null)
            {
                var Targets = RedTeam.Objects.Where(o => o.P.X > X0 && o.P.X < X1 && o.P.Y > Y0 && o.P.Y < Y1).ToList();

                foreach (var s in Selected)
                {
                    s.Target = s.GetClosesLeavingTarget(Targets);
                }
                Selected = null;
            }
            else
            {
                Selected = BlueTeam.Objects.Where(o => o.P.X > X0 && o.P.X < X1 && o.P.Y > Y0 && o.P.Y < Y1).ToList();
            }

            IsSelecting = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Interval = Math.Max(timer1.Interval / 2, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Interval *= 2;
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Init();
            end = false;
            timer1.Start();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            IsSelecting = true;

            // Save the start point.
            X0 = e.X;
            Y0 = e.Y;
        }
    }
}

