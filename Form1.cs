using System;
using System.Windows.Forms;

namespace _2024._11._26.elektron
{
    public partial class Form1 : Form
    {
        Timer electronTimer = new Timer();
        //int voltSpeed = 2;
        int voltSize = 12;
        int iranyX = 0;      // 1 - jobbra; 0 - fel vagy le; -1 - balra
        int iranyY = -1;      // 1 - le; 0 - jobbra vagy balra; -1 - fel
        int electronResults = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Voltage";
            Start();
        }
        void Start()
        {
            electronTimer.Interval = 30;
            AddEvents();
            electronTimer.Start();
            electronTimer.Tick += ElectronSpeed;
        }
        void AddEvents()
        {
            felButton.Click += upVoltage;
            leButton.Click += downVoltage;
        }
        void upVoltage(object s, EventArgs e)
        {
            if (voltSize < 24)
            {
                voltSize += 1;
                kijelzo.Text = voltSize.ToString() + "V";
            }
        }
        void downVoltage(object s, EventArgs e)
        {
            if (voltSize > 1)
            {
                voltSize -= 1;
                kijelzo.Text = voltSize.ToString() + "V";
            }
        }
        void ElectronSpeed(object s, EventArgs e)
        {
            //  felfelé megy
            if (iranyX == 0 && iranyY == -1)
            {
                if (elektron.Top > FentiKabel.Top - 2)
                {
                    elektron.Top -= voltSize;
                }
                else
                {
                    elektron.Top = FentiKabel.Top - 2;
                    iranyX = -1;
                    iranyY = 0;
                }

            }
            //  lefelé megy
            else if (iranyX == 0 && iranyY == 1)
            {
                if (elektron.Top < LentiKabel.Bottom - elektron.Height + 2)
                {
                    elektron.Top += voltSize;
                }
                else
                {
                    elektron.Top = LentiKabel.Bottom - elektron.Height + 2;
                    iranyX = 1;
                    iranyY = 0;
                }
            }
            //  balra megy
            else if (iranyX == -1 && iranyY == 0)
            {
                if (elektron.Left > FentiKabel.Left - 2)
                {
                    elektron.Left -= voltSize;
                }
                else
                {
                    elektron.Left = FentiKabel.Left - 2;
                    iranyX = 0;
                    iranyY = 1;
                    electronResults += 1;
                    result.Text = $"Electrons passed: {electronResults}";
                }
            }
            //  jobbra megy
            else if (iranyX == 1 && iranyY == 0)
            {
                if (elektron.Left < JobbKabel.Right - elektron.Width + 2)
                {
                    elektron.Left += voltSize;
                    if (elektron.Left >= LentiKabel.Right - 2)
                    {
                        elektron.Visible = false;
                    }
                }
                else
                {
                    elektron.Left = JobbKabel.Right - elektron.Width + 2;
                    iranyX = 0;
                    iranyY = -1;
                    elektron.Visible = true;
                }
            }
        }

    }
}