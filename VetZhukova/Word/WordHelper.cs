using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace VetZhukova.Word
{
    public class WordHelper
    {
        private string _filePath;

        public WordHelper(string filePath)
        {
            _filePath = filePath;
        }

        public void CreateDocument(string title, string bodyText)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(_filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                // Создаем основной документ
                //MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                //mainPart.Document = new Document();
                //Body body = new Body();

                //// Заголовок
                //DocumentFormat.OpenXml.Drawing.Paragraph titleParagraph = CreateParagraph(title, bold: true, fontSize: "28");
                //body.Append(titleParagraph);

                //// Пробел
                //body.Append(new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new Text(" "))));

                //// Основной текст
                //DocumentFormat.OpenXml.Drawing.Paragraph bodyParagraph = CreateParagraph(bodyText, bold: false, fontSize: "24");
                //body.Append(bodyParagraph);

                //mainPart.Document.Append(body);
                //mainPart.Document.Save();
            }
        }

        public void CreateVisitDocument(
        string patientName,
        string ownerName,
        string employeeName,
        string serviceName,
        string comment,
        string price)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                _filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(CreateParagraph("Информация о записи", bold: true, centered: true, size: "28"));

                body.Append(CreateParagraph($"Пациент: {patientName}", color: "AA0000"));
                body.Append(CreateParagraph($"Владелец питомца: {ownerName}", color: "AA0000"));
                body.Append(CreateParagraph($"Принимающий: {employeeName}", color: "AA0000"));
                body.Append(CreateParagraph($"Услуга: {serviceName}", color: "AA0000"));
                body.Append(CreateParagraph("Комментарий:"));
                body.Append(CreateParagraph(comment));
                body.Append(CreateParagraph(""));
                body.Append(CreateParagraph($"К оплате: {price}"));
                body.Append(new Paragraph(new Run(new Text(""))));
                body.Append(CreateParagraph("Подпись: __________________________", size: "22"));

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        private Paragraph CreateParagraph(string text, bool bold = false, bool centered = false, string color = "000000", string size = "24")
        {
            var runProps = new RunProperties();
            runProps.Append(new FontSize() { Val = size });
            runProps.Append(new Color() { Val = color });
            if (bold)
                runProps.Append(new Bold());

            var run = new Run();
            run.Append(runProps);
            run.Append(new Text(text));

            var paragraph = new Paragraph();
            if (centered)
            {
                paragraph.ParagraphProperties = new ParagraphProperties(
                    new Justification() { Val = JustificationValues.Center });
            }

            paragraph.Append(run);
            return paragraph;
        }
    }
}
