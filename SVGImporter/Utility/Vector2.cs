using System.Text;

namespace SVGImporter.Utility
{
    public struct Vector2
    {
        public float x, y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a) => a;
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);

        public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);

        public static Vector2 operator *(Vector2 a, int scalar) => new Vector2(a.x * scalar, a.y * scalar);
        public static Vector2 operator *(Vector2 a, float scalar) => new Vector2(a.x * scalar, a.y * scalar);
        public static Vector2 operator *(int scalar, Vector2 a) => a * scalar;
        public static Vector2 operator *(float scalar, Vector2 a) => a * scalar;

        public static Vector2 operator /(Vector2 a, int scalar) => new Vector2(a.x / scalar, a.y / scalar);
        public static Vector2 operator /(Vector2 a, float scalar) => new Vector2(a.x / scalar, a.y / scalar);

        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 one = new Vector2(1, 1);
        public static readonly Vector2 right = new Vector2(1, 0);
        public static readonly Vector2 up = new Vector2(0, 1);
        public static readonly Vector2 down = new Vector2(0, -1);
        public static readonly Vector2 left = new Vector2(-1, 0);

        public override string ToString()
        {
            return $"<{x}, {y}>";
        }

        internal static string ToSVG(List<Vector2> points)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Vector2 point in points)
            {
                stringBuilder.Append(ToSVG(point));
                stringBuilder.Append(' ');
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        internal static string ToSVG(Vector2 point)
        {
            return $"{point.x},{point.y}";
        }
    }
}
