using System;
using System.Collections.Generic;
using System.Text;

namespace CFCreator
{
    public class TileID
    {
        public int RR, RC, R, C;
        public TileID(int i, int j, int k, int l)
        {
            RR = i;
            RC = j;
            R = k;
            C = l;
        }

        public override string ToString()
        {
            return $"{RR}, {RC}, {R}, {C}";
        }
    }
}
