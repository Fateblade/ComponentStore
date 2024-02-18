using System.Collections.Generic;

namespace Fateblade.Components.Logic.Foundation.OCR.Contract.DataClasses
{
    public class ScanInfo
    {
        public float Confidence { get; set; }
        public string ScannedFilePath { get; set; }
        public string ScannedFullContent { get; set; }
        public List<string> ScannedSeparatedContent { get; set; }
    }
}
