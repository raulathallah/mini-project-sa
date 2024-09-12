using PdfSharpCore.Pdf;
using PdfSharpCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using LibraryManagementSystem.Domain.Service;

namespace LibraryManagementSystem.Application.Service
{
    public class PdfService : IPdfService
    {
        public byte[] OnGeneratePDF(string htmlcontent)
        {
            var document = new PdfDocument();

            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.SetMargins(12);
            config.PageSize = PageSize.A4;

            string cssStr = File.ReadAllText(@"./style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);

            PdfGenerator.AddPdfPages(document, htmlcontent, config, css);

            MemoryStream stream = new();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return bytes;
        }
    }
}
