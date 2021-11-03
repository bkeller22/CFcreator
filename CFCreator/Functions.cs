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

        //initializes list of wafer maps
        public static List<WaferMap> WaferMaps = new List<WaferMap>();

        ///<summary>
        ///Bool to check if rectangle is inside circle
        /// </summary>
        public static bool CheckRectInCircle(Rectangle rectangle, Point center, double radius)
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
        public static void CountTiles(CFCreatorForm form)
        {
            foreach (WaferMap Wafer in Functions.WaferMaps)
            {
                Debug.WriteLine((Functions.WaferMaps.IndexOf(Wafer)));
                foreach (MapTile tile in Wafer.MapTileList.Where(x => x.Color == Color.Green))
                {
                    Wafer.ClickedTiles.Add(new Point(tile.Location.X, tile.Location.Y));
                    Debug.WriteLine(string.Format("{0}, {1}", tile.Location.X, tile.Location.Y));
                }
            }
        }

    }
}
