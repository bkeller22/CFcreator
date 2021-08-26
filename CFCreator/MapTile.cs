using System;
using System.Drawing;

namespace CFCreator
{
    public class MapTile
    {
        private static Color[] myColors = new Color[] { Color.White, Color.Green };

        private bool _NeedsUpdate = true;
        ///<summary>
        ///This will reduce the number of drawing iterations
        ///</summary>
        public bool NeedsUpdate
        //will throw public flag for updating then turn false
        {
            get
            {
                if (_NeedsUpdate)
                {
                    //if private update returns true, then set public to true and turn private to false
                    _NeedsUpdate = false;
                    return true;
                }
                return false;
            }
        }

        private Rectangle _Rectangle;
        public Rectangle Rectangle
        {
            get { return _Rectangle; }
        }

        private Color _Color = myColors[0];
        public Color Color
        {
            get { return _Color; }
        }

        private bool _LastHighlight;
        private bool _Highlight;
        public bool Highlight
        {
            get { return _Highlight; }
            set
            {
                _Highlight = value;
                if (_LastHighlight != _Highlight)
                ///<summary>
                ///just a little extra funcitonality to flip the update flag
                ///... as a treat
                /// </summary>
                {
                    _NeedsUpdate = true;
                    _LastHighlight = _Highlight;
                }
            }
        }

        private Point _Location;
        public Point Location
        {
            get { return _Location; }
        }

        public MapTile(Rectangle rectangle, int row, int col)
        {
            _Rectangle = rectangle;
            _Location = new Point(col + 1, row + 1);
        }

        public void ChangeColor()
        {
            int colorIdx = Array.IndexOf(myColors, _Color);
            _Color = colorIdx < myColors.Length - 1 ? myColors[colorIdx + 1] : myColors[0];
            _NeedsUpdate = true;
        }
    }
}
