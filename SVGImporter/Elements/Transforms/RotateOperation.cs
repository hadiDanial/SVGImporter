using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class RotateOperation : TransformOperation
    {
        public Quaternion Rotation { get; }
        private Vector3 pivotPoint;
        public RotateOperation(float angle, float cx, float cy)
        {
            Vector3 axis = new Vector3(0, 0, 1); // rotate around the z-axis
            pivotPoint = new Vector3(cx, cy, 0);
            Rotation = Quaternion.AngleAxis(angle, axis);
        }

        public override Vector2 ApplyTo(Vector2 point)
        {
            return Rotation * point;
        }

        public override void ApplyTo(Transform transform, float scaleFactor = 1f)
        {
            Vector3 originalPosition = transform.position;
            Transform originalParent = transform.parent;
            Transform pivot = (new GameObject()).GetComponent<Transform>();
            transform.SetParent(pivot);
            transform.localPosition = pivotPoint;
            transform.position = Rotation * (transform.position - pivotPoint) + pivotPoint;
            transform.rotation = Rotation * transform.rotation;
        }
        
        public override bool IsAppliedToTransform()
        {
            return true;
        }
        
        public override string ToString()
        {
            return $"Rotation: {Rotation.ToString()}";
        }
    }
}