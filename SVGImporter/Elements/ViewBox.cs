using System.Numerics;

namespace SVGImporter.Elements
{
    public class ViewBox
    {
        private Vector2 size;
        private Vector2 origin;
        private bool defined = false;
        public ViewBox(Vector2 size, Vector2 origin)
        {
            this.size = size;
            this.origin = origin;
            defined = true;
        }
        public ViewBox() { }

        public bool Defined { get => defined; }

        //public Vector2 GetPoint(Vector2 point)
        //{

        //}
    }
}
