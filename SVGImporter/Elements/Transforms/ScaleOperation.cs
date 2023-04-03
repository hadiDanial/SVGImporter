using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class ScaleOperation : TransformOperation
    {
        public Vector3 Scale { get; }

        public ScaleOperation(float sx, float sy)
        {
            Scale = (sx.Equals(sy)) ? new Vector3(sx, sx, sx) : new Vector3(sx, sy, 1f);
        }

        public override Vector2 ApplyTo(Vector2 point)
        {
            point = new Vector3(point.x, point.y, 1);
            return Vector3.Scale(point, Scale);
        }

        public override void ApplyTo(Transform transform)
        {
            transform.localScale = Vector3.Scale(transform.localScale, Scale);
        }
        public override string ToString()
        {
            return $"Scale: {Scale.ToString()}";
        }
    }
}