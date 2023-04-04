using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public abstract class TransformOperation
    {
        /// <summary>
        /// Apply the transform operation to a Vector2 point.
        /// </summary>
        /// <param name="point">The point that this operation will be applied to.</param>
        /// <returns>The modified point.</returns>
        public abstract Vector2 ApplyTo(Vector2 point);
        /// <summary>
        /// Apply the transform operation to a transform.
        /// </summary>
        /// <param name="transform">The transform that this operation will be applied to.</param>
        public abstract void ApplyTo(Transform transform, float scaleFactor = 1f);

        /// <summary>
        /// Returns true if this operation should be applied to a transform and false if it should be applied to a point.
        /// </summary>
        public abstract bool IsAppliedToTransform();
        public abstract override string ToString();
    }
}