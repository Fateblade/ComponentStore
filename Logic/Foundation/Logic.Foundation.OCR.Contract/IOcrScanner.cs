using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.OCR.Contract.DataClasses;
using Logic.Foundation.OCR.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.OCR.Contract
{
    [MapException(typeof(OcrException))]
    public interface IOcrScanner
    {
        ScanInfo ScanPdfUsingJpgConversion(string fullPath, int resolution);
        ScanInfo ScanPdfUsingPngConversion(string fullPath, int resolution);
        ScanInfo ScanImage(string fullPath);
    }
}
