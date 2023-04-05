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
            Matrix = new Matrix4x4(
                new Vector4(a, b, 0, 0),
                new Vector4(c, d, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(e, f, 0, 1)
            );
        }


        public override Vector2 ApplyTo(Vector2 point)
        {
            return Matrix.MultiplyPoint(point);
        }

        public override void ApplyTo(Transform transform, float scaleFactor = 1f)
        {
            Matrix4x4 currentMatrix = Matrix4x4.TRS(transform.position * scaleFactor, transform.rotation, transform.localScale * scaleFactor);
            Matrix4x4 newMatrix = Matrix * currentMatrix;
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
            return $"Matrix: {Matrix.ToString()}";
        }
    }
}