using System.Runtime.Serialization;

namespace SVGImporter
{
    [Serializable]
    internal class SVGException : Exception
    {
        public SVGException()
        {
        }

        public SVGException(string? message) : base(message)
        {
        }

        public SVGException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SVGException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}