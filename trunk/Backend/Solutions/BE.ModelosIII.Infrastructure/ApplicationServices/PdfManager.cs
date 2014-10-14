using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using BE.ModelosIII.Resources;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public class PdfManager : IPdfManager
    {
        private Document _document;
        private PdfWriter _pdfWriter;
        private Models.MostSoldHour.ReportInfo _mostSoldHourInfo;
        private Models.MostSoldMovie.ReportInfo _mostSoldMovieInfo;

        private readonly IFileSystem _fileSystem;
        private readonly string _workPath;


        public PdfManager(IFileSystem fileSystem,
                            string workPath)
        {
            _fileSystem = fileSystem;
            _workPath = workPath;
        }

        public byte[] GetMostSoldHourContent(Models.MostSoldHour.ReportInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");

            _mostSoldHourInfo = info;
            _document = new Document(PageSize.A4, 20f, 20f, 100f, 20f);
            var outputStream = new MemoryStream();

            try
            {
                _pdfWriter = PdfWriter.GetInstance(_document, outputStream);
                _pdfWriter.CloseStream = false;
                var pageEventHelper = new TwoColumnHeaderFooter(_fileSystem, _workPath)
                                          {
                                              Title = info.Title
                                          };
                _pdfWriter.PageEvent = pageEventHelper;
                _document.Open();
                GenerateMostSoldHourDocument();
            }
            finally
            {
                _document.Close();
            }
            outputStream.Flush();
            outputStream.Position = 0;

            byte[] outputPdf = outputStream.ToArray();

            return outputPdf;
        }

        private void GenerateMostSoldHourDocument()
        {
            AddMostSoldHourInfo();
            AddMostSoldHourChart();
        }

        private void AddMostSoldHourInfo()
        {
            string description = string.Format(ReportMessages.MostSoldHourDescription,
                                               string.IsNullOrEmpty(_mostSoldHourInfo.Multiplex) ? "todos los complejos" : "el complejo " + _mostSoldHourInfo.Multiplex,
                                               _mostSoldHourInfo.From,
                                               _mostSoldHourInfo.To);

            var p = new Paragraph
                        {
                            new Chunk(description)
                        };
            p.IndentationLeft = _document.PageSize.Width * 0.1f;
            _document.Add(p);

            string[] columns = { "Horario", "Candidad Vendidas", "Cantidad Películas" };
            var table = new PdfPTable(3)
                            {
                                WidthPercentage = 100,
                                TotalWidth = _document.PageSize.Width - 80
                            };
            table.SetWidths(new Single[] { 1, 1, 1 });
            table.SpacingBefore = 10;
            table.SpacingAfter = 80;

            foreach (string column in columns)
            {
                var cell = new PdfPCell(new Phrase(column))
                               {
                                   BackgroundColor = new BaseColor(204, 204, 204)
                               };
                table.AddCell(cell);
            }

            foreach (var item in _mostSoldHourInfo.Items)
            {
                table.AddCell(item.StartTime);
                table.AddCell(item.SoldTickets.ToString(CultureInfo.InvariantCulture));
                table.AddCell(item.MovieQuantity.ToString(CultureInfo.InvariantCulture));
            }

            _document.Add(table);
        }

        private void AddMostSoldHourChart()
        {
            if (_mostSoldHourInfo.Items == null || !_mostSoldHourInfo.Items.Any())
            {
                return;
            }

            const string chartFormat = "http://chart.googleapis.com/chart?chxl=1:|{0}&chxr=0,0,{1}&chxt=y,x&chbh=a,5,10&chs=450x375&cht=bvg&chco=3072F3&chds=0,{1}&chd=t:{2}&chdl={3}";
            string xAxisData = string.Join("|", _mostSoldHourInfo.Items.Select(i => HttpUtility.UrlEncode(i.StartTime)));
            string yAxisData = string.Join(",", _mostSoldHourInfo.Items.Select(i => i.SoldTickets));
            int yAxisMax = _mostSoldHourInfo.Items.Max(i => i.SoldTickets) + 5;

            string chart = string.Format(chartFormat, 
                xAxisData,
                yAxisMax, 
                yAxisData, 
                ReportMessages.MostSoldHourChartLabel);

            var uri = new Uri(chart);
            var chartImage = Image.GetInstance(uri);
            chartImage.SpacingBefore = 10f;
            chartImage.Alignment = Image.TEXTWRAP | Image.ALIGN_CENTER;
            _document.Add(chartImage);
        }

        public byte[] GetMostSoldMovieContent(Models.MostSoldMovie.ReportInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");

            _mostSoldMovieInfo = info;
            _document = new Document(PageSize.A4, 20f, 20f, 100f, 20f);
            var outputStream = new MemoryStream();

            try
            {
                _pdfWriter = PdfWriter.GetInstance(_document, outputStream);
                _pdfWriter.CloseStream = false;
                var pageEventHelper = new TwoColumnHeaderFooter(_fileSystem, _workPath)
                {
                    Title = info.Title
                };
                _pdfWriter.PageEvent = pageEventHelper;
                _document.Open();
                GenerateMostSoldMovieDocument();
            }
            finally
            {
                _document.Close();
            }
            outputStream.Flush();
            outputStream.Position = 0;

            byte[] outputPdf = outputStream.ToArray();

            return outputPdf;
        }

        private void GenerateMostSoldMovieDocument()
        {
            AddMostSoldMovieInfo();
            AddMostSoldMovieChart();
        }

        private void AddMostSoldMovieInfo()
        {
            string description = string.Format(ReportMessages.MostSoldMovieDescription,
                                               string.IsNullOrEmpty(_mostSoldMovieInfo.Multiplex) ? "todos los complejos" : "el complejo " + _mostSoldMovieInfo.Multiplex,
                                               _mostSoldMovieInfo.From,
                                               _mostSoldMovieInfo.To);

            var p = new Paragraph
                        {
                            new Chunk(description)
                        };
            p.IndentationLeft = _document.PageSize.Width * 0.1f;
            _document.Add(p);

            string[] columns = { "Candidad Vendidas", "Película", "Complejo" };
            var table = new PdfPTable(3)
            {
                WidthPercentage = 100,
                TotalWidth = _document.PageSize.Width - 80
            };
            table.SetWidths(new Single[] { 1, 1, 1 });
            table.SpacingBefore = 10;
            table.SpacingAfter = 80;

            foreach (string column in columns)
            {
                var cell = new PdfPCell(new Phrase(column))
                {
                    BackgroundColor = new BaseColor(204, 204, 204)
                };
                table.AddCell(cell);
            }

            foreach (var item in _mostSoldMovieInfo.Items)
            {
                table.AddCell(item.SoldTickets.ToString(CultureInfo.InvariantCulture));
                table.AddCell(item.Movie);
                table.AddCell(item.Multiplex);
            }

            _document.Add(table);
        }

        private void AddMostSoldMovieChart()
        {
            if (_mostSoldMovieInfo.Items == null || !_mostSoldMovieInfo.Items.Any())
            {
                return;
            }

            const string chartFormat = "http://chart.googleapis.com/chart?chs=600x200&cht=p&chco=3072F3&chd=t:{0}&chl={1}&{2}";
            string axisData = string.Join(",", _mostSoldMovieInfo.Items.Select(i => i.SoldTickets));
            string axisArrowData = string.Join("|", _mostSoldMovieInfo.Items.Select(i => HttpUtility.UrlEncode(i.Movie + " - " + i.Multiplex)));

            string chart = string.Format(chartFormat,
                axisData,
                axisArrowData,
                ReportMessages.MostSoldMovieChartLabel);

            var uri = new Uri(chart);
            var chartImage = Image.GetInstance(uri);
            chartImage.SpacingBefore = 10f;
            chartImage.Alignment = Image.TEXTWRAP | Image.ALIGN_CENTER;
            _document.Add(chartImage);
        }
    }

    public class TwoColumnHeaderFooter : PdfPageEventHelper
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _workPath;
        private readonly Font _headerFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
        private readonly BaseFont _bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        private const string LogoFileName = "logo.png";

        private PdfContentByte _cb;
        private PdfTemplate _template;
        private DateTime _printTime = DateTime.Now;

        public string Title { get; set; }

        public TwoColumnHeaderFooter(IFileSystem fileSystem, string workPath)
        {
            _fileSystem = fileSystem;
            _workPath = workPath;
        }

        // we override the onOpenDocument method
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            _printTime = DateTime.Now;
            _cb = writer.DirectContent;
            _template = _cb.CreateTemplate(50, 50);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            var img = Image.GetInstance(_fileSystem.Load(Path.Combine(_workPath, LogoFileName)));
            img.ScalePercent(30f);
            img.SetAbsolutePosition(pageSize.GetLeft(10), pageSize.GetTop((float) ((img.Height * 0.3) + 10)));
            _cb.AddImage(img);

            if ( Title != string.Empty)
            {
                var headerTable = new PdfPTable(2);
                headerTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                headerTable.TotalWidth = pageSize.Width - 80;
                headerTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);

                headerTable.AddCell(new PdfPCell { Border = Rectangle.NO_BORDER });

                var headerRightCell = new PdfPCell(new Phrase(8, Title, _headerFont))
                                          {
                                              HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
                                              Padding = 5,
                                              PaddingBottom = 8,
                                              Border = Rectangle.NO_BORDER
                                          };
                headerTable.AddCell(headerRightCell);
                headerTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(50), _cb);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            String text = "Página " + pageN + " de ";
            float len = _bf.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;

            _cb.SetRGBColorFill(100, 100, 100);

            _cb.BeginText();
            _cb.SetFontAndSize(_bf, 8);
            _cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            _cb.ShowText(text);
            _cb.EndText();

            _cb.AddTemplate(_template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            _cb.BeginText();
            _cb.SetFontAndSize(_bf, 8);
            _cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                "Impreso el " + _printTime,
                pageSize.GetRight(40),
                pageSize.GetBottom(30), 0);
            _cb.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            _template.BeginText();
            _template.SetFontAndSize(_bf, 8);
            _template.SetTextMatrix(0, 0);
            _template.ShowText("" + (writer.PageNumber - 1));
            _template.EndText();
        }

    }
}