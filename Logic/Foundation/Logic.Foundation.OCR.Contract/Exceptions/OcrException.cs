using System;

namespace Logic.Foundation.OCR.Contract.Exceptions
{
    public class OcrException : Exception
    {
        public OcrException()
        {
        }

        public OcrException(string message) : base(message)
        {
        }

        public OcrException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
