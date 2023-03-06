using SVGImporter.Utility;

namespace SVGImporter.SVG.Elements
{
    internal class Rect: Element
    {
        private Vector2 position;
        private Vector2 size;
        private Vector2 cornerRadius;

        public Rect(string tagText, List<Attribute> attributes, Vector2 position, Vector2 size, Vector2 cornerRadius) : base(tagText, attributes)
        {
            this.position = position;
            this.size = size;
            this.cornerRadius = cornerRadius;
        }
        

        public override string ElementToSVGTag()
        {
            return $"<rect x=\"{position.x}\" y=\"{position.y}\" " +
                $"width=\"{size.x}\" height=\"{size.y}\" rx=\"{cornerRadius.x}\" ry=\"{cornerRadius.y}\" />";
        }

        public override string GetElementName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Rect: {position}, size = {size}, corners = {cornerRadius}";
        }

        public override string GetElementNameReadable()
        {
            return "Rect";
        }

        public static new Rect GetElement(string tagText)
        {
            throw new NotImplementedException();
        }
    }
}
