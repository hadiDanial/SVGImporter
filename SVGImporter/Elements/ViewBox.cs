using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Utility;

namespace SVGImporter.Elements
{
    public class ViewBox
    {
        private Vector2 size;
        private Vector2 origin;
        private bool defined = false;
        public ViewBox(Vector2 size, Vector2 origin)
        {
            this.Size = size;
            this.Origin = origin;
            defined = true;
        }
        public ViewBox() { }

        public bool Defined { get => defined; }
        public Vector2 Size { get => size; set => size = value; }
        public Vector2 Origin { get => origin; set => origin = value; }

        //public Vector2 GetPoint(Vector2 point)
        //{

        //}
    }
}
