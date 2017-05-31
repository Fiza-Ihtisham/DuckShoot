using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duckshooter
{
    class Cimagebase:IDisposable
    {
        bool disposed = false;
        Bitmap _bitmap;
        private int X;
        private int Y;
        public int left{ get{return X;} set{X=value;}}
        public int top { get { return Y; } set { Y = value; } }

        public Cimagebase(Bitmap resource)
        {
           _bitmap = new Bitmap(resource);
        }
        public void drawImage(Graphics gfx)
        {
            gfx.DrawImage(_bitmap, X, Y);

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if(disposing)
            {
                _bitmap.Dispose();

            }
            disposed = true;
        }

    }
}
