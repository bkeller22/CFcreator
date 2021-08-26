using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CFCreator
{
    static class Functions
    {
        private static List<MapTile> MapTileList = new List<MapTile>();
        private static List<Point> ClickedTiles = new List<Point>();
        private static RectangleF InfoRectangle;

        #region Tile iteration

        ///<summary>
        ///Populate a list of tiles
        /// </summary>
        /// <param name="form"></param>

        public static void MakeGrid(CFCreatorForm form)
        {
            MapTileList.Clear();
            Bitmap bitmap = (Bitmap)form.pictureBox.Image.Clone();
            //new bitmap is clone of blank form created in picturebox
            Size cellSize = new Size(bitmap.Width / form.GridSize.Width, bitmap.Height / form.GridSize.Height);
            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            double radius = Math.Min((cellSize.Width * form.GridSize.Width) + cellSize.Width, (cellSize.Height * form.GridSize.Height) + cellSize.Height) / 2;
            for (int i = 0; i < form.GridSize.Width; i++)
            {
                for (int j = 0; j < form.GridSize.Height; j++)
                {
                    Rectangle rectangle = new Rectangle(i * cellSize.Width, j * cellSize.Height, cellSize.Width, cellSize.Height);
                    if (CheckRectInCircle(rectangle, center, radius))
                        MapTileList.Add(new MapTile(rectangle, i, j));
                }
            }
            DrawGrid(form);
            DrawTiles(form);
        }
        private static bool CheckRectInCircle(Rectangle rectangle, Point center, double radius)
        {
            Point[] corners = new Point[]
            {
                new Point(rectangle.Left, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Left, rectangle.Bottom),
                new Point(rectangle.Right, rectangle.Bottom)
            };

            bool inCircle = true;
            foreach (Point point in corners)
            {
                double dist = Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
                if (dist > radius) inCircle = false;
            }
            return inCircle;
        }

        ///<summary>
        ///Toggle the highlight param for the tile under the cursor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="form"></param>
        public static void HighlightMapTile(Point position, CFCreatorForm form)
        {
            Point zoomPos = ZoomMousePos(position, form);
            MapTileList.ForEach(x => x.Highlight = x.Rectangle.Contains(zoomPos));
            DrawTiles(form);
        }

        ///<summary>
        ///change the color of the tile under the cursor
        /// </summary>
        /// <param name="click"></param>
        /// <param name="form"></param>
        /// <param name="waitForExit"></param>

        public static void ClickMapTile(Point click, CFCreatorForm form, bool waitForExit = false)
        {
            Point zoomClick = ZoomMousePos(click, form);
            foreach (MapTile tile in MapTileList.Where(x => x.Rectangle.Contains(zoomClick)))
            {
                if (tile.Rectangle.Contains(zoomClick))
                {
                    if (waitForExit && tile.Highlight)
                        return;
                    tile.ChangeColor();
                }
            }
            DrawTiles(form);
        }

        #endregion

        #region Drawing

        private static void DrawGrid(CFCreatorForm form)
        {
            Bitmap bitmap = (Bitmap)form.pictureBox.Image.Clone();
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Rectangle wafer = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                Pen waferpen = new Pen(Brushes.Black);
                waferpen.Width = 5.0F;
                g.DrawEllipse(waferpen, wafer);
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                foreach (MapTile tile in MapTileList)
                {
                    g.DrawRectangle(Pens.Black, tile.Rectangle);
                }
            }
            form.pictureBox.Image = bitmap;
        }

        private static void DrawTiles(CFCreatorForm form)
        {
            //for timing how long a process takes
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            Bitmap bitmap = (Bitmap)form.pictureBox.BackgroundImage.Clone();
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //Clear last tile
                g.FillRectangle(new SolidBrush(SystemColors.Control), InfoRectangle);

                MapTile? tileUnderCursor = null;
                foreach (MapTile tile in MapTileList.Where(x => x.NeedsUpdate || x.Highlight))
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
                    string location = string.Format("{0}, {1}", tileUnderCursor.Location.X, tileUnderCursor.Location.Y);
                    Font font = new Font("Tahoma", 25);
                    SizeF size = g.MeasureString(location, font);
                    InfoRectangle = new RectangleF(bitmap.Width - size.Width * 1.1f, size.Height * 1.1f, size.Width, size.Height);
                    g.DrawString(location, font, Brushes.Black, InfoRectangle);
                }

            }
            //updates background image to be the highlighted and colored tiles
            form.pictureBox.BackgroundImage = bitmap;

            //Output the time for this method's duration
            sw.Stop();
            //Debug.WriteLine(sw.Elapsed);
        }
        #endregion

        #region count highlighted tiles

        public static void CountTiles(CFCreatorForm form)
        {
            foreach (MapTile tile in MapTileList.Where(x => x.Color == Color.Green))
                {
                    ClickedTiles.Add(new Point(tile.Location.X, tile.Location.Y));
                    Debug.WriteLine(string.Format("{0}, {1}", tile.Location.X, tile.Location.Y));
            }
        }
        #endregion

        /// <summary>
        /// Copy paste method for adjusting mouse pos to pictureBox set to Zoom
        /// </summary>
        /// <param name="click"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Point ZoomMousePos(Point click, CFCreatorForm form)
        {
            PictureBox pbx = form.pictureBox;
            float BackgroundImageAspect = pbx.BackgroundImage.Width / (float)pbx.BackgroundImage.Height;
            float controlAspect = pbx.Width / (float)pbx.Height;
            PointF pos = new PointF(click.X, click.Y);
            if (BackgroundImageAspect > controlAspect)
            {
                float ratioWidth = pbx.BackgroundImage.Width / (float)pbx.Width;
                pos.X *= ratioWidth;
                float scale = pbx.Width / (float)pbx.BackgroundImage.Width;
                float displayHeight = scale * pbx.BackgroundImage.Height;
                float diffHeight = pbx.Height - displayHeight;
                diffHeight /= 2;
                pos.Y -= diffHeight;
                pos.Y /= scale;
            }
            else
            {
                float ratioHeight = pbx.BackgroundImage.Height / (float)pbx.Height;
                pos.Y *= ratioHeight;
                float scale = pbx.Height / (float)pbx.BackgroundImage.Height;
                float displayWidth = scale * pbx.BackgroundImage.Width;
                float diffWidth = pbx.Width - displayWidth;
                diffWidth /= 2;
                pos.X -= diffWidth;
                pos.X /= scale;
            }
            return new Point((int)pos.X, (int)pos.Y);
        }

    }
}
