using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public abstract class TransformOperation
    {
        public abstract void ApplyTo(Transform transform);
        public abstract string ToString();
    }
}