using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CFCreator
{
    public partial class CFCreatorForm : Form
    {
        private Size _GridSize = new Size(50, 50);
        public Size GridSize
        {
            get { return _GridSize; }
        }
        public CFCreatorForm()
        {
            InitializeComponent();
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;

        }
        private void CFCreatorForm_Load(object sender, EventArgs e)
        {
            pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            pictureBox.Image = new Bitmap(1000, 1000);
            //initializes foreground image
            Functions.MakeGrid(this);
            //meat of drawing the grid on the pictureBox image (foreground)
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        /// <summary>
        /// Resets Cursor to default arror when leaving pictureBox
        /// </summary>
        {
            Cursor = Cursors.Default;
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        /// <summary>
        /// Changes cursor to cross when inside pictureBox
        /// </summary>
        {
            Cursor = Cursors.Cross;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        /// <summary>
        /// Event for moving mouse over square.
        /// Contains if statement to handle holding down left mouse button.
        /// </summary>
        {
            if (e.Button == MouseButtons.Left)
                Functions.ClickMapTile(e.Location, this, waitForExit: true);
            Functions.HighlightMapTile(e.Location, this);
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        /// <summary>
        /// Handles mouse click over square
        /// </summary>
        {
            Functions.ClickMapTile(e.Location, this);
        }

        private void DrawWafer_Click(object sender, EventArgs e)
        {
            int row = (int)TgtRows.Value;
            int col = (int)TgtCols.Value;
            _GridSize = new Size(col, row);
            pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            pictureBox.Image = new Bitmap(1000, 1000);
            Functions.MakeGrid(this);
        }

        private void CreateCF_Click(object sender, EventArgs e)
        {
            Functions.CountTiles(this);
        }
    }
}
