using duckshooter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duckshooter
{
    class CDuck: Cimagebase
    {
        private Rectangle duckhotspot = new Rectangle();

        public CDuck():base(Resources.duck)
        {
            duckhotspot.X = left + 20;
            duckhotspot.Y = top - 1;
            duckhotspot.Width = 20;
            duckhotspot.Height = 30;

        }
        public void update(int x,int y)
        {
            left = x;
            top = y;
            duckhotspot.X = left + 20;
            duckhotspot.Y = top - 1;

        }
        public bool hit(int X,int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);
            if(duckhotspot.Contains(c))
            {
                return true;
            }
            return false;
        }

    }
}
