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
using static CFCreator.Functions;
using System.Drawing.Drawing2D;

namespace CFCreator
{
    public partial class CFCreatorForm : Form
    {
        public CFCreatorForm()
        {
            InitializeComponent();

            WaferMaps.Add(new WaferMap()
            {
                Name = "Target",
                Dock = DockStyle.Fill
            });

            WaferMaps.Add(new WaferMap()
            {
                Name = "Source",
                Dock = DockStyle.Fill
            });
            tableLayoutPanel1.Controls.Add(WaferMaps[0], 1, 0);
            tableLayoutPanel1.Controls.Add(WaferMaps[1], 1, 1);

        }

        public static string targetid;
        public static string sourceid;
        public static int picksperfield;
        public static int startindex;
        private void DrawWafer_Click(object sender, EventArgs e)
        {
            WaferMaps[0].RegGridSize = new Size ((int)TgtRegCols.Value, (int)TgtRegRows.Value);
            WaferMaps[0].ClustGridSize = new Size((int)TgtPrintCols.Value, (int)TgtPrintRows.Value);
            WaferMaps[1].RegGridSize = new Size((int)SrcRegCols.Value, (int)SrcRegRows.Value);
            WaferMaps[1].ClustGridSize = new Size((int)SrcClustCols.Value, (int)SrcClustRows.Value);
            WaferMaps[0].pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            WaferMaps[0].pictureBox.Image = new Bitmap(1000, 1000);
            WaferMaps[1].pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            WaferMaps[1].pictureBox.Image = new Bitmap(1000, 1000);
            MakeGrid(0);
            MakeGrid(1);
            foreach (WaferMap wafer in WaferMaps)
            {
                wafer.OrderClickedTiles.Clear();
            }
        }
        private void CreateCF_Click(object sender, EventArgs e)
        {
            //uses a 2nd list of clicked tiles in order to preserve the OrderClickedTiles list
            //allows user to check/uncheck box without losing the order they clicked
            WaferMaps[0].preserveclick = TgtClickOrderCheckBox.Checked;
            WaferMaps[1].preserveclick = SrcClickOrderCheckBox.Checked;
            foreach (WaferMap wafer in WaferMaps)
            {
                if (wafer.preserveclick == false)
                {
                    Functions.CountTiles(wafer);
                }
                else wafer.ClickedTiles = wafer.OrderClickedTiles;
            }
            targetid = TargetID.Text;
            sourceid = SourceID.Text;
            picksperfield = (int)PicksPerField.Value;
            startindex = (int)StartIndex.Value;
            Functions.WriteCF(this);
        }
        
        //Creates a list of map tiles, draws the overlay grid, then calls WaferMaps.DrawTiles
        public static void MakeGrid(int n)
        {
            WaferMaps[n].MapTileList.Clear();
            Size size = WaferMaps[n].TotGridSize;
            Size regions = WaferMaps[n].RegGridSize;
            Size clusters = WaferMaps[n].ClustGridSize;
            PictureBox pbx = WaferMaps[n].pictureBox;
            //temp bitmap is clone of picturebox
            Bitmap bitmap = (Bitmap)pbx.Image.Clone();
            Size cellSize = new Size(bitmap.Width / size.Width, bitmap.Height / size.Height);
            Size regionSize = new Size(bitmap.Width / regions.Width, bitmap.Height / regions.Height);
            //Use 'center' and 'radius' for the CheckRectInCircle function
            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            double radius = Math.Min((cellSize.Width * size.Width) + cellSize.Width, (cellSize.Height * size.Height) + cellSize.Height) / 2;
            for (int j = 0; j < regions.Height; j++)
            {
                for (int i = 0; i < regions.Width; i++)
                {
                    for (int l = 0; l < clusters.Height; l++)
                    {
                        for (int k = 0; k < clusters.Width; k++)
                        {
                            Rectangle rectangle = new Rectangle((i * regionSize.Width) + (k * cellSize.Width), ((regions.Height - j - 1) * regionSize.Height) + ((clusters.Height - l - 1) * cellSize.Height), cellSize.Width, cellSize.Height);
                            TileID loc = new TileID(j + 1, i + 1, l + 1, k + 1);
                            WaferMaps[n].MapTileList.Add(new MapTile(rectangle, loc));
                            //Use this to draw only tiles inscribed in a circle
                            //if (Functions.CheckRectInCircle(rectangle, center, radius))
                               //WaferMaps[n].MapTileList.Add(new MapTile(rectangle, i, j));
                        }
                    }
                }
            }
            Rectangle waferborder = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Pen regionpen = new Pen(Brushes.Blue, 5.0F);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawEllipse(regionpen, waferborder);
                for (int i = 0; i < regions.Width; i++)
                {
                    for (int j = 0; j< regions.Height; j++)
                    {
                        Rectangle regionrect = new Rectangle(i * regionSize.Width, j * regionSize.Height, regionSize.Width, regionSize.Height);
                        g.DrawRectangle(regionpen, regionrect);
                    }
                }
                foreach (MapTile tile in WaferMaps[n].MapTileList)
                {
                    g.DrawRectangle(Pens.Black, tile.Rectangle);
                }
            }
            //sets picturebox image to the edited bitmap
            pbx.Image = bitmap;
            WaferMaps[n].DrawTiles(pbx);
        }

        private void OpenRecipeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
