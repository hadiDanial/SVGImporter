using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SVGImporter.Utility
{
    internal static class Vector2Utility
    {
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
            return $"{point.X},{point.Y}";
        }
        public static string ToString(Vector2 point)
        {
            return $"<{point.X}, {point.Y}>";
        }
    }
}
