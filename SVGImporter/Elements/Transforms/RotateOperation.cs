﻿using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class RotateOperation : TransformOperation
    {
        public Quaternion Rotation { get; }

        public RotateOperation(float angle, float cx, float cy)
        {
            Vector3 axis = new Vector3(0, 0, 1); // rotate around the z-axis
            Vector3 center = new Vector3(cx, cy, 0);
            Quaternion rotation = Quaternion.AngleAxis(angle, axis);
            Matrix4x4 translation = Matrix4x4.Translate(center);
            Matrix4x4 inverseTranslation = Matrix4x4.Translate(-center);
            Rotation = translation.rotation * rotation * inverseTranslation.rotation;
        }

        public override Vector2 ApplyTo(Vector2 point)
        {
            return Rotation * point;
        }

        public override void ApplyTo(Transform transform)
        {
            transform.rotation *= Rotation;
        }
        
        public override string ToString()
        {
            return $"Rotation: {Rotation.ToString()}";
        }
    }
}