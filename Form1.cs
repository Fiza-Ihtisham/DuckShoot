//#define mydebug

using duckshooter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApplication1;
namespace duckshooter
{

    public partial class duckshooterform : Form
    {
        bool splat = false;
        int gameframe = 0;
        int splattime = 0;

        int hits = 0;
        int misses = 0;
        int totalshots=0;

#if mydebug
        int cursX = 0;
        int cursY = 0;
#endif

        CDuck _duck;
        Csign _sign;
        Csplat _splat;
        Cscoreframe _scoreframe;
        Random rnd = new Random();
        public duckshooterform()
        {
            InitializeComponent();
            _duck = new CDuck()
            {
                left = 10,
                top = 400
            };
            _scoreframe = new Cscoreframe()
            {
                left = 10,
                top = 10
            };
            _splat = new Csplat();
            _sign = new Csign()
            {
                left = 850,
                top = 10
            };
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if(gameframe>=8)
            {
                updateduck();
                gameframe = 0;
            }
            if(splat)
            {
                if(splattime<=3)
                {
                    splat = false;
                    splattime = 0;
                    updateduck();
                }
            }
            gameframe++;
            this.Refresh();
        }

        private void updateduck()
        {

            _duck.update(rnd.Next(Resources.duck.Width,this.Width-Resources.duck.Width),
                rnd.Next(this.Height/2,this.Height-Resources.duck.Height*2)
                );
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            
            if(splat==true)
            {
                _splat.drawImage(dc);
            }
            else
            {
                _duck.drawImage(dc);
           
            }
          
            _scoreframe.drawImage(dc);
           // _splat.drawImage(dc);
            _sign.drawImage(dc);
#if mydebug
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "X=" + cursX.ToString() + ":" + "Y=" + cursY.ToString(), _font, new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif         
            TextFormatFlags flags = TextFormatFlags.Left;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, "Shots:" + totalshots.ToString(), _font, new Rectangle(30, 32, 120, 20), SystemColors.ControlText, flags);

            TextRenderer.DrawText(e.Graphics, "Hits:" + hits.ToString(), _font, new Rectangle(30, 52, 120, 20), SystemColors.ControlText, flags);

            TextRenderer.DrawText(e.Graphics, "Misses:" + misses.ToString(), _font, new Rectangle(30, 72, 120, 20), SystemColors.ControlText, flags);



            base.OnPaint(e);
        }

        private void duckshooterform_MouseMove(object sender, MouseEventArgs e)
        {
#if mydebug
            cursX = e.X;
            cursY = e.Y;
#endif
            this.Refresh();
        }

        private void duckshooterform_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X > 907 && e.X < 931 && e.Y > 30 && e.Y < 41)
            {
                timerGameLoop.Start();
            }
             else if (e.X > 907 && e.X < 931 && e.Y > 48 && e.Y < 58)
            {
                timerGameLoop.Stop();
            } 
            else if (e.X > 907 && e.X < 931 && e.Y > 64 && e.Y < 74)
            {
                timerGameLoop.Stop();
            } 
            else if (e.X > 907 && e.X < 931 && e.Y > 80 && e.Y < 93)
            {
                this.Hide();
               // pingpongform pg = new pingpongform();
                //pg.Show();

            }
            else
            {
                if(_duck.hit(e.X,e.Y))
                {
                    splat = true;
                    _splat.left = _duck.left - Resources.splash.Width / 3;
                    _splat.top = _duck.top - Resources.splash.Height / 3;
                    hits++;
                }
                else{
                    misses++;
                
                }
                totalshots = misses + hits;
            }
            firegun();
        }
        private void firegun()
        {
            SoundPlayer simplesound = new SoundPlayer(Resources.shotgun);
            simplesound.Play();
        }
    }
}
