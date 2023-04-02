using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class TranslateOperation : TransformOperation
    {
        public Vector3 Translation { get; }

        public TranslateOperation(float tx, float ty)
        {
            Translation = new Vector3(tx, ty);
        }

        public override void ApplyTo(Transform transform)
        {
            transform.Translate(Translation);
        }
    }
}