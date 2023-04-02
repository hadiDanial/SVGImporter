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