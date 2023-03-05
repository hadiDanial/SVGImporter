using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Utility;

namespace SVGImporter.SVG
{
    public class ViewBox
    {
        private Vector2 size;
        private Vector2 origin;

        public ViewBox(Vector2 size, Vector2 origin)
        {
            this.size = size;
            this.origin = origin;
        }

        //public Vector2 GetPoint(Vector2 point)
        //{

        //}
    }
}
