using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class RotateOperation : TransformOperation
    {
        public Quaternion Rotation { get; }

        public RotateOperation(float angle, float cx, float cy)
        {
            float angleInRadians = angle * Mathf.Deg2Rad;
            Vector3 axis = new Vector3(0, 0, 1); // rotate around the z-axis
            Vector3 center = new Vector3(cx, cy, 0);
            Quaternion rotation = Quaternion.AngleAxis(angleInRadians, axis);
            Matrix4x4 translation = Matrix4x4.Translate(center);
            Matrix4x4 inverseTranslation = Matrix4x4.Translate(-center);
            Rotation = translation.rotation * rotation * inverseTranslation.rotation;
        }

        public override void ApplyTo(Transform transform)
        {
            transform.rotation *= Rotation;
        }
    }
}