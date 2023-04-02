using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class MatrixOperation : TransformOperation
    {
        public Matrix4x4 Matrix { get; }

        public MatrixOperation(Matrix4x4 matrix)
        {
            Matrix = matrix;
        }

        public MatrixOperation(float a, float b, float c, float d, float e, float f)
        {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(a, c, 0, e),
                new Vector4(b, d, 0, f),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public override void ApplyTo(Transform transform)
        {
            Matrix4x4 currentMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Matrix4x4 newMatrix = Matrix * currentMatrix;
            transform.position = newMatrix.GetColumn(3);
            transform.rotation = Quaternion.LookRotation(newMatrix.GetColumn(2), newMatrix.GetColumn(1));
            transform.localScale = newMatrix.lossyScale;
        }
    }
}