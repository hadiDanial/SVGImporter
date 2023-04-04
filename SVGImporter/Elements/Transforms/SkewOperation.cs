using UnityEngine;

namespace SVGImporter.Elements.Transforms
{

    public class SkewOperation : TransformOperation
    {
        public float AngleX { get; }
        public float AngleY { get; }
        private Matrix4x4 skewMatrix;
        
        public SkewOperation(float angleX, float angleY)
        {
            AngleX = angleX;
            AngleY = angleY;
            skewMatrix = Matrix4x4.identity;
            skewMatrix[0, 1] = Mathf.Tan(-AngleX * Mathf.Deg2Rad);
            skewMatrix[1, 0] = Mathf.Tan(-AngleY * Mathf.Deg2Rad);
        }

        public override Vector2 ApplyTo(Vector2 point)
        {
            return skewMatrix.MultiplyPoint(point);
        }

        public override void ApplyTo(Transform transform, float scaleFactor = 1f)
        {
            Matrix4x4 currentMatrix = Matrix4x4.TRS(transform.position * scaleFactor, transform.rotation, transform.localScale * scaleFactor);
            Matrix4x4 newMatrix = skewMatrix * currentMatrix;
            transform.position = newMatrix.GetColumn(3);
            transform.rotation = Quaternion.LookRotation(newMatrix.GetColumn(2), newMatrix.GetColumn(1));
            transform.localScale = newMatrix.lossyScale;
        }
        
        public override bool IsAppliedToTransform()
        {
            return false;
        }
        
        public override string ToString()
        {
            return $"Skew: ({AngleX}, {AngleY})";
        }
    }
}