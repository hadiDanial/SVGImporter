using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class TranslateOperation : TransformOperation
    {
        public Vector2 Translation { get; }

        public TranslateOperation(float tx, float ty)
        {
            Translation = new Vector2(tx, ty);
        }

        public override Vector2 ApplyTo(Vector2 point)
        {
            return point + Translation;
        }

        public override void ApplyTo(Transform transform)
        {
            transform.Translate(Translation);
        }
        
        public override string ToString()
        {
            return $"Translate: {Translation.ToString()}";
        }
    }
}