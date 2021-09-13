using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manipulacion_Imagenes
{
    public partial class Form1 : Form
    {
        private Bitmap bmpOG;
        private Bitmap bmpNew;
        public Form1()
        {
            InitializeComponent();      
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (bmpNew != null)
                {
                    e.Graphics.DrawImage(bmpNew, 400 - bmpNew.Width / 2, 300 - bmpNew.Height / 2);
                }
            }
            catch (Exception) { }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                bmpOG = (Bitmap) Image.FromFile(dlg.FileName);
                bmpNew = (Bitmap) bmpOG.Clone();
            }

            Invalidate();
        }

        private void buttonGrayScale_Click(object sender, EventArgs e)
        {
            Color pColor;
            int grayScale;

            for(int x = 0; x < bmpNew.Width; x++)
            {
                for(int y = 0; y < bmpNew.Height; y++)
                {
                    pColor = bmpNew.GetPixel(x, y);
                    grayScale = (int) Math.Round(pColor.R * 0.299 + pColor.G * 0.587 + pColor.B * 0.114);
                    bmpNew.SetPixel(x, y, Color.FromArgb(pColor.A,grayScale,grayScale,grayScale));
                }
            }

            Invalidate();
        }

        private void buttonBW_Click(object sender, EventArgs e)
        {
            Color pColor;
            int colorValue;
            
            for (int x = 0; x < bmpNew.Width; x++)
            {
                for (int y = 0; y < bmpNew.Height; y++)
                {
                    pColor = bmpNew.GetPixel(x, y);
                    colorValue = ((pColor.R + pColor.G + pColor.B) / 3) > 128 ? 255 : 0;

                    bmpNew.SetPixel(x, y, Color.FromArgb(pColor.A, colorValue, colorValue, colorValue));
                }
            }

            Invalidate();
        }

        private void buttonSepia_Click(object sender, EventArgs e)
        {
            Color pColor;
            int tr, tg, tb;

            for (int x = 0; x < bmpNew.Width; x++)
            {
                for (int y = 0; y < bmpNew.Height; y++)
                {
                    pColor = bmpNew.GetPixel(x, y);

                    tr = (int)Math.Round((0.393 * pColor.R + 0.769 * pColor.G + 0.189 * pColor.B));
                    tg = (int)Math.Round((0.349 * pColor.R + 0.686 * pColor.G + 0.168 * pColor.B));
                    tb = (int)Math.Round((0.272 * pColor.R + 0.534 * pColor.G + 0.131 * pColor.B));

                    tr = tr > 255 ? 255 : tr;
                    tg = tg > 255 ? 255 : tg;
                    tb = tb > 255 ? 255 : tb;

                    bmpNew.SetPixel(x, y, Color.FromArgb(pColor.A, tr, tg, tb));
                }
            }

            Invalidate();
        }

        private void buttonNegative_Click(object sender, EventArgs e)
        {
            Color pColor;
            int nr, ng, nb;

            for (int x = 0; x < bmpNew.Width; x++)
            {
                for (int y = 0; y < bmpNew.Height; y++)
                {
                    pColor = bmpNew.GetPixel(x, y);
                    
                    nr = 255 - pColor.R;
                    ng = 255 - pColor.G;
                    nb = 255 - pColor.B;

                    bmpNew.SetPixel(x, y, Color.FromArgb(pColor.A,nr,ng,nb));
                }
            }

            Invalidate();
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            bmpNew = (Bitmap)bmpOG.Clone();
            Invalidate();
        }
    }
}
