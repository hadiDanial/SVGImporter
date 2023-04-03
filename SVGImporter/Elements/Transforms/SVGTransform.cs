using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SVGImporter.Elements.Transforms
{
    public class SVGTransform
    {
        private List<TransformOperation> operations;

        public SVGTransform()
        {
            operations = new List<TransformOperation>();
        }

        /// <summary>
        /// Add an operation to the Transform Operations list.
        /// </summary>
        /// <param name="operation">Transform Operation to be added.</param>
        public void AddOperation(TransformOperation operation)
        {
            operations.Add(operation);
        }

        /// <summary>
        /// Apply all the Transform Operations to a point and return the result.
        /// </summary>
        /// <param name="point">The point to be transformed.</param>
        /// <returns>Transformed point after all operations have been performed.</returns>
        public Vector2 ApplyTo(Vector2 point)
        {
            foreach (TransformOperation operation in operations)
            {
                point = operation.ApplyTo(point);
            }
            return point;
        } 
        
        /// <summary>
        /// Apply all the Transform Operations to a transform.
        /// </summary>
        /// <param name="transform">The transform that the operations will be applied to.</param>
        public void ApplyTo(Transform transform)
        {
            foreach (TransformOperation operation in operations)
            {
                operation.ApplyTo(transform);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SVG Transform:\n");
            foreach (TransformOperation transformOperation in operations)
            {
                stringBuilder.Append(transformOperation.ToString());
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}