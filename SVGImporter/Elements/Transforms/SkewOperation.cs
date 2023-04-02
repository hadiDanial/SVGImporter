using UnityEngine;

namespace SVGImporter.Elements.Transforms
{

    public class SkewOperation : TransformOperation
    {
        public float AngleX { get; }
        public float AngleY { get; }

        public SkewOperation(float angleX, float angleY)
        {
            AngleX = angleX;
            AngleY = angleY;
        }

        public override void ApplyTo(Transform transform)
        {
            Matrix4x4 skewMatrix = Matrix4x4.identity;
            skewMatrix[0, 1] = Mathf.Tan(AngleY * Mathf.Deg2Rad);
            skewMatrix[1, 0] = Mathf.Tan(AngleX * Mathf.Deg2Rad);

            Matrix4x4 currentMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Matrix4x4 newMatrix = skewMatrix * currentMatrix;
            transform.position = newMatrix.GetColumn(3);
            transform.rotation = Quaternion.LookRotation(newMatrix.GetColumn(2), newMatrix.GetColumn(1));
            transform.localScale = newMatrix.lossyScale;
        }
    }
}