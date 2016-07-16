using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoView
{
    public partial class Form1 : Form
    {
        private GBRom rom;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                rom = new GBRom();
            }
            catch (BadRomException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var graphicsData = rom.GetGraphicsData();
            var buffer = new Bitmap(256, 256, PixelFormat.Format8bppIndexed);
            var pal = buffer.Palette;
            pal.Entries[0] = Color.Black;
            pal.Entries[1] = Color.White;
            pal.Entries[2] = Color.FromArgb(130, 140, 128);
            pal.Entries[3] = Color.FromArgb(85, 90, 80);
            buffer.Palette = pal;

            var boundsRect = new Rectangle(0, 0, 256, 256);
            var bmpData = buffer.LockBits(boundsRect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            var ptr = bmpData.Scan0;
            Marshal.Copy(graphicsData, 0, ptr, graphicsData.Length);
            buffer.UnlockBits(bmpData);

            pbxCanvas.Image = buffer;

            Invalidate();
        }
    }
}
