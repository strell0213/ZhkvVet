using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using VetZhukova.DB;
using VetZhukova.ServiceFolder;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace VetZhukova.Word
{
    public class WordHelper
    {
        private string _filePath;
        private readonly PatientService _patientService;
        private readonly ServiceService _serviceService;

        public WordHelper(string filePath)
        {
            _filePath = filePath;
            _patientService = new PatientService();
            _serviceService = new ServiceService();
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

                body.Append(CreateParagraph($"Пациент: {patientName}"));
                body.Append(CreateParagraph($"Владелец питомца: {ownerName}"));
                body.Append(CreateParagraph($"Принимающий: {employeeName}"));
                body.Append(CreateParagraph($"Услуга: {serviceName}"));
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

        public void CreateDoneVisitByEmp(List<Visit> visits)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                _filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(CreateParagraph("Информация о выполненных записях", bold: true, centered: true, size: "28"));

                int count=1;
                foreach(var visit in visits)
                {
                    var patient = _patientService.GetPatientByID(visit.PatientID);

                    string text="";
                    text += count.ToString();
                    text += ". ";
                    text += $"№{visit.VisitID} - {visit.visitDate}: ";
                    text += $"Пациент: {patient.name}. ";
                    text += $"{visit.notes}";
                    body.Append(CreateParagraph(text));
                    body.Append(CreateParagraph(""));
                    count++;
                }

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        public void CreateServices(List<Service> services)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                _filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(CreateParagraph("Информация об услугах", bold: true, centered: true, size: "28"));

                int count = 1;
                foreach (var service in services)
                {
                    string text = "";
                    text += count.ToString();
                    text += ". ";
                    text += service.serviceName; text += ", ";
                    text += $"Цена: {service.Price}. ";
                    text += $"Описание: {service.description}";
                    body.Append(CreateParagraph(text));
                    body.Append(CreateParagraph(""));
                    count++;
                }

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        public void CreateConsumbles(List<Consumable> consumables)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                _filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(CreateParagraph("Информация о расходных материалах", bold: true, centered: true, size: "28"));

                int count = 1;
                foreach (var consumable in consumables)
                {
                    string text = "";
                    text += count.ToString();
                    text += ". ";
                    text += consumable.name; text += ", ";
                    text += $"{consumable.Quantity} {consumable.unit}";
                    body.Append(CreateParagraph(text));
                    body.Append(CreateParagraph(""));
                    count++;
                }

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
        }

        public void CreateVisitPatient(List<Visit> visits)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                _filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(CreateParagraph("История записей", bold: true, centered: true, size: "28"));

                int count = 1;
                foreach (var visit in visits)
                {
                    var patient = _patientService.GetPatientByID(visit.PatientID);
                    var service = _serviceService.GetServiceByID(visit.ServiceID);

                    string text = "";
                    text += count.ToString();
                    text += ". ";
                    text += $"№{visit.VisitID} - {visit.visitDate}: ";
                    text += $"Пациент: {patient.name}. ";
                    text += $"Услуга: {service.serviceName}. ";
                    text += $"{visit.notes}";
                    body.Append(CreateParagraph(text));
                    body.Append(CreateParagraph(""));
                    count++;
                }

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
