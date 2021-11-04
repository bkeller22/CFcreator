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
        public List<MapTile> MapTileList = new List<MapTile>();
        private static List<Point> ClickedTiles = new List<Point>();
        private static RectangleF InfoRectangle;
        public Size GridSize = new Size(50, 50);
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

        private void DrawWafer_Click(object sender, EventArgs e)
        {
            WaferMaps[0].RegGridSize = new Size ((int)TgtRegRows.Value, (int)TgtRegCols.Value);
            WaferMaps[0].ClustGridSize = new Size((int)TgtPrintRows.Value, (int)TgtPrintCols.Value);
            WaferMaps[1].RegGridSize = new Size((int)SrcRegRows.Value, (int)SrcRegCols.Value);
            WaferMaps[1].ClustGridSize = new Size((int)SrcClustRows.Value, (int)SrcClustCols.Value);
            WaferMaps[0].pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            WaferMaps[0].pictureBox.Image = new Bitmap(1000, 1000);
            WaferMaps[1].pictureBox.BackgroundImage = new Bitmap(1000, 1000);
            WaferMaps[1].pictureBox.Image = new Bitmap(1000, 1000);
            MakeGrid(0);
            MakeGrid(1);
        }
        #region making grid
        private void CreateCF_Click(object sender, EventArgs e)
        {
            Functions.CountTiles(this);
        }
        public static void MakeGrid(int n)
        {
            WaferMaps[n].MapTileList.Clear();
            Size size = WaferMaps[n].TotGridSize;
            Size regions = WaferMaps[n].RegGridSize;
            Size clusters = WaferMaps[n].ClustGridSize;
            PictureBox pbx = WaferMaps[n].pictureBox;
            Bitmap bitmap = (Bitmap)pbx.Image.Clone();
            //new bitmap is clone of blank form created in picturebox
            Size cellSize = new Size(bitmap.Width / size.Width, bitmap.Height / size.Height);
            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            double radius = Math.Min((cellSize.Width * size.Width) + cellSize.Width, (cellSize.Height * size.Height) + cellSize.Height) / 2;
            for (int i = 0; i < regions.Width; i++)
            {
                for (int j = 0; j < regions.Height; j++)
                {
                    for (int k = 0; k < clusters.Width; k++)
                    {
                        for (int l = 0; l < clusters.Height; l++)
                        {
                            Rectangle rectangle = new Rectangle(i * cellSize.Width, j * cellSize.Height, cellSize.Width, cellSize.Height);
                            TileID loc = new TileID(i, j, k, l);
                            WaferMaps[n].MapTileList.Add(new MapTile(rectangle, loc));
                            //Use this to only draw in a circle
                            //if (Functions.CheckRectInCircle(rectangle, center, radius))
                               //WaferMaps[n].MapTileList.Add(new MapTile(rectangle, i, j));
                        }
                    }
                }
            }
            DrawGrid(pbx, size, n);
            DrawTiles(pbx, n);
        }

        #endregion

        #region Drawing

        public static void DrawGrid(PictureBox pbx, Size size, int n)
        {
            Bitmap bitmap = (Bitmap)pbx.Image.Clone();
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                foreach (MapTile tile in WaferMaps[n].MapTileList)
                {
                    g.DrawRectangle(Pens.Black, tile.Rectangle);
                }
            }
            pbx.Image = bitmap;
        }
        public static void DrawTiles(PictureBox pbx, int n)
        {
            //for timing how long a process takes
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            Bitmap bitmap = (Bitmap)pbx.BackgroundImage.Clone();
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //Clear last tile
                g.FillRectangle(new SolidBrush(SystemColors.Control), InfoRectangle);

                MapTile tileUnderCursor = null;
                foreach (MapTile tile in WaferMaps[n].MapTileList.Where(x => x.NeedsUpdate || x.Highlight))
                {
                    if (tile.Highlight)
                    {

                        //cell will always be highlighted when it changes color
                        g.FillRectangle(new SolidBrush(tile.Color), tile.Rectangle);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(90, Color.Black)), tile.Rectangle);
                        tileUnderCursor = tile;
                    }
                    else
                    {
                        //look for previously highlighted cells
                        double A = bitmap.GetPixel(tile.Rectangle.X, tile.Rectangle.Y).A;
                        if (A != 100)
                            g.FillRectangle(new SolidBrush(tile.Color), tile.Rectangle);
                        // A=100 is the color value for highlighted
                    }
                }

                // display for cursor position in wafer coordinates
                if (tileUnderCursor != null)
                {
                    //These options help with text drawing
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    //Draw new text for tile location
                    string location = string.Format(tileUnderCursor.ID.ToString());
                    Font font = new Font("Tahoma", 25);
                    SizeF size = g.MeasureString(location, font);
                    InfoRectangle = new RectangleF(bitmap.Width - size.Width * 1.1f, size.Height * 1.1f, size.Width, size.Height);
                    g.DrawString(location, font, Brushes.Black, InfoRectangle);
                }

            }
            //updates background image to be the highlighted and colored tiles
            pbx.BackgroundImage = bitmap;

            //Output the time for this method's duration
            sw.Stop();
            //Debug.WriteLine(sw.Elapsed);
        }

        #endregion
    }
}
