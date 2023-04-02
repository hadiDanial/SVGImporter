using System.Collections.Generic;
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

        public void AddOperation(TransformOperation operation)
        {
            operations.Add(operation);
        }

        public void ApplyTo(Transform transform)
        {
            foreach (TransformOperation operation in operations)
            {
                operation.ApplyTo(transform);
            }
        }
    }
}