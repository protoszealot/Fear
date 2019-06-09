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

        bool end = false;
        int Enemies = 100;

        float Money = 0;

        List<FearObject> BlueTeam = new List<FearObject>() { };

        List<FearObject> RedTeam = new List<FearObject>() {};

        public Form1()
        {
            InitializeComponent();

            Restart();
        }

        void Restart()
        {
            BlueTeam.Clear();
            RedTeam.Clear();

            for (int i = 0; i < 400; i++)
            {
                var soldier = GetRandomSoldier(Brushes.BlueViolet);
                BlueTeam.Add(soldier);

                soldier.P = new FPoint { X = rand.Next(10, 25), Y = rand.Next(300, 400) };
            }

            for (int i = 0; i < 100; i++)
            {
                var soldier = GetRandomSoldier(Brushes.Red);
                RedTeam.Add(soldier);

                soldier.P = new FPoint { X = rand.Next(400, 425), Y = rand.Next(300, 400) };
            }

            foreach (var fo in BlueTeam)
            {
                //fo.Target = RedTeam.ElementAt(rand.Next(0,RedTeam.Count()));
                fo.AllEnemies = RedTeam;
                fo.AllFriends = BlueTeam;
            }

            foreach (var fo in RedTeam)
            {
                //fo.Target = BlueTeam.ElementAt(rand.Next(0, BlueTeam.Count()));
                fo.AllEnemies = BlueTeam;
                fo.AllFriends = RedTeam;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var fo in BlueTeam)
            {
                fo.FillMe(e);
            }

            foreach (var fo in RedTeam)
            {
                fo.FillMe(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var fo in BlueTeam.Where(f => !f.IsDead))
            {
                fo.Act();
            }

            foreach (var fo in RedTeam.Where(f => !f.IsDead))
            {
                fo.Act();
            }

            int blueCounter = BlueTeam.Where(fo => !fo.IsDead).Count();
            int redCounter = RedTeam.Where(fo => !fo.IsDead).Count();

            UpdateStats();

            if (blueCounter == 0)
            {
                timer1.Stop();
                if (!end)
                {
                    MessageBox.Show("Defeat!!!");
                    end = true;
                }
            }

            if (redCounter == 0)
            {
                timer1.Stop();
                if (!end)
                {
                    MessageBox.Show("Victory!!!");
                    Money += RedTeam.Count * 3;
                    MoneyTB.Text = Money.ToString();

                    // for free
                    for (int i = 0; i < 10; i++)
                        BlueTeam.Add(GetRandomSoldier(Brushes.BlueViolet));
                    foreach (var fo in BlueTeam)
                    {
                        fo.BaseFitness += rand.Next(0, 3);
                    }

                    BlueTeam = BlueTeam.Where(s => !s.IsDead).ToList();
                    var dead = RedTeam.Where(s => s.IsDead).Union(BlueTeam.Where(s => s.IsDead));
                    int deadCnt = dead.Count();

                    // take Amunition From the dead
                    for (int i = 0; i < 3000; i++)
                    {
                        var sol = BlueTeam.ElementAt(rand.Next(0, BlueTeam.Count));
                        var corpse = dead.ElementAt(rand.Next(0, deadCnt));
                        sol.SwapBetterAmunition(corpse);
                    }

                    UpdateStats();

                    end = true;
                }
            }

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
            Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RedTeam.Clear();

            Enemies += 10;

            foreach ( var fo in BlueTeam)
            {
                fo.P = new FPoint { X = rand.Next(10, 25), Y = rand.Next(380, 400) };
                fo.Stamina = 100;
                fo.Fitness = fo.BaseFitness;
                fo.Moral = MoralMode.Normal;
                fo.Fear = 0;
            }

            for (int i = 0; i < Enemies; i++)
            {
                var soldier = GetRandomSoldier(Brushes.Red);
                RedTeam.Add(soldier);

                soldier.P = new FPoint { X = rand.Next(400, 425), Y = rand.Next(300, 400) };
            }

            foreach (var fo in BlueTeam)
            {
                fo.AllEnemies = RedTeam;
                fo.AllFriends = BlueTeam;
            }

            foreach (var fo in RedTeam)
            {
                fo.AllEnemies = BlueTeam;
                fo.AllFriends = RedTeam;
            }

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
                Moral = MoralMode.Normal
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
                Moral = MoralMode.Normal
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
                Moral = MoralMode.Normal
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for(int i = 0; i < 10; i++)
                    BlueTeam.Add(GetRandomSoldier(Brushes.BlueViolet));
            }
            UpdateStats();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Money >= BlueTeam.Count)
            {
                Money -= BlueTeam.Count;

                foreach (var fo in BlueTeam)
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
                    float worstShield = BlueTeam.Min(fo => fo.Shield);

                    foreach (var fo in BlueTeam)
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
                    float worstSword = BlueTeam.Min(fo => fo.Sword);

                    foreach (var fo in BlueTeam)
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

            int blueCounter = BlueTeam.Where(fo => !fo.IsDead).Count();
            int redCounter = RedTeam.Where(fo => !fo.IsDead).Count();

            if (blueCounter != 0)
                teamBlueCounter.Text = GetStatForTeam(blueCounter, BlueTeam);

            if (redCounter != 0)
                teamRedCounter.Text = GetStatForTeam(redCounter, RedTeam);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Money >= 100)
            {
                Money -= 100;

                for (int i = 0; i < 3; i++)
                {
                    BlueTeam.Add(GetEliteRandomSoldier(Brushes.BlueViolet));
                }
            }
            UpdateStats();
        }
    }
}

