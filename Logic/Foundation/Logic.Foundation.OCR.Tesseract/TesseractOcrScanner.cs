using Fateblade.Components.CrossCutting.ExceptionFormatter.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.OCR.Contract;
using Fateblade.Components.Logic.Foundation.OCR.Contract.DataClasses;
using Ghostscript.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tesseract;

namespace Fateblade.Components.Logic.Foundation.OCR.Tesseract
{
    internal class TesseractOcrScanner : IOcrScanner
    {
        private readonly ILogger _logger;
        private readonly IExceptionMessageFormatter _exceptionMessageFormatter;

        public TesseractOcrScanner(ILogger logger, IExceptionMessageFormatter exceptionMessageFormatter)
        {
            _logger = logger;
            _exceptionMessageFormatter = exceptionMessageFormatter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        /// <remarks>Requires corresponding (32/64 bit) ghostscript installation on executing machine https://ghostscript.com/releases/gsdnld.html</remarks>
        public ScanInfo ScanPdfUsingJpgConversion(string fullPath, int resolution)
        {
            var tmpFilePath = string.Empty;
            try
            {
                tmpFilePath = GetTempFile("jpg");
                
                var jpgDevice = new GhostscriptJpegDevice(GhostscriptJpegDeviceType.Jpeg)
                {
                    GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                    TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                    ResolutionXY = new GhostscriptImageDeviceResolution(resolution, resolution),
                    JpegQuality = 100,
                    OutputPath = tmpFilePath
                };
                jpgDevice.InputFiles.Add(fullPath);
                jpgDevice.Process();

                return ScanImage(tmpFilePath);
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LoggingPriority.High,
                    LoggingType.Exception,
                    _exceptionMessageFormatter.FormatMessagesAndStackTracesToString(ex));

                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(tmpFilePath) && File.Exists(tmpFilePath))
                {
                    File.Delete(tmpFilePath);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        /// <remarks>Requires corresponding (32/64 bit) ghostscript installation on executing machine https://ghostscript.com/releases/gsdnld.html</remarks>
        public ScanInfo ScanPdfUsingPngConversion(string fullPath, int resolution)
        {
            var tmpFilePath = string.Empty;
            try
            {
                var tempFilePath = GetTempFile("png");

                var jpgDevice = new GhostscriptPngDevice(GhostscriptPngDeviceType.PngAlpha)
                {
                    GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                    TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                    ResolutionXY = new GhostscriptImageDeviceResolution(resolution, resolution),
                    OutputPath = tempFilePath
                };
                jpgDevice.InputFiles.Add(fullPath);
                jpgDevice.Process();

                return ScanImage(tempFilePath);
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LoggingPriority.High,
                    LoggingType.Exception,
                    _exceptionMessageFormatter.FormatMessagesAndStackTracesToString(ex));

                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(tmpFilePath) && File.Exists(tmpFilePath))
                {
                    File.Delete(tmpFilePath);
                }
            }
        }

        public ScanInfo ScanImage(string fullPath)
        {
            try
            {
                using (var engine = new TesseractEngine("./tessdata_best", "deu", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(fullPath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            var info = new ScanInfo
                            {
                                Confidence = page.GetMeanConfidence(),
                                ScannedFilePath = fullPath,
                                ScannedFullContent = page.GetText()
                            };

                            separateScannedContent(info, page);

                            return info;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LoggingPriority.High,
                    LoggingType.Exception,
                    _exceptionMessageFormatter.FormatMessagesAndStackTracesToString(ex));

                throw;
            }
        }

        private void separateScannedContent(ScanInfo info, Page page)
        {
            info.ScannedSeparatedContent = new List<string>();
            StringBuilder lineBuilder = new StringBuilder();

            using (var iterator = page.GetIterator())
            {
                iterator.Begin();

                do
                {
                    do
                    {
                        do
                        {
                            lineBuilder.Clear();

                            do
                            {
                                if (iterator.IsAtBeginningOf(PageIteratorLevel.Block))
                                {
                                    //Console.WriteLine("<BLOCK>");
                                }

                                if (lineBuilder.Length > 0) lineBuilder.Append(" ");

                                lineBuilder.Append(iterator.GetText(PageIteratorLevel.Word));
                                

                                if (iterator.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                {
                                    info.ScannedSeparatedContent.Add(lineBuilder.ToString());
                                }
                            } while (iterator.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                            if (iterator.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                            {
                                //Console.WriteLine();
                            }
                        } while (iterator.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                    } while (iterator.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                } while (iterator.Next(PageIteratorLevel.Block));
            }
        }

        protected string GetTempFile(string extension)
        {
            var tempFilePath = Path.GetTempFileName();

            var tmpExtensionFilePath = Path.ChangeExtension(tempFilePath, extension);
            if (File.Exists(tempFilePath)) { File.Delete(tempFilePath); }

            return tmpExtensionFilePath;
        }

    }
}
