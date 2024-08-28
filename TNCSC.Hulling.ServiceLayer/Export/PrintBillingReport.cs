using DevExpress.XtraRichEdit;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TNCSC.Hulling.Domain.Reports;

namespace TNCSC.Hulling.ServiceLayer.Export
{
    public class PrintBillingReport
    {
        #region Declartion
        DocumentManager documentManager = new DocumentManager();
        RunProperties runPropertiesValues = new RunProperties();
        //RunProperties runPropertiesHeading = new RunProperties();
        string language = string.Empty;
        #endregion

        #region Constructor
        public PrintBillingReport()
        {
            
            FontSize ofont = new FontSize() { Val = "20" };
            RunFonts runFont = new RunFonts { Ascii = "Poppins" };
            Justification justification1 = new Justification() { Val = JustificationValues.Center };
            ParagraphProperties User_heading = new ParagraphProperties();
            User_heading.Justification = justification1;
            runPropertiesValues.FontSize = ofont;
            runPropertiesValues.RunFonts = runFont;
             
            Color c = new Color() { Val = "#808080" };
            runPropertiesValues.Append(c);

        }
        #endregion
        string templateName = @"\BillingReport.docx"; 
        #region DownloadPDF
        /// <summary>
        /// DownloadPDF
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public byte[] DownloadPDF(List<BillingPaddy> Details,string month,string variety)
        {
            string rootFolder = Directory.GetCurrentDirectory();
              
            var memoryStream = new MemoryStream();
            {

                string sourcefileName = Path.Combine(rootFolder, "Template") + templateName;

                using (var fileStream = new FileStream(sourcefileName, FileMode.Open, FileAccess.Read))
                    fileStream.CopyTo(memoryStream);

                using (var mainDocument = WordprocessingDocument.Open(memoryStream, true))
                {
                    mainDocument.ChangeDocumentType(WordprocessingDocumentType.Document);
                    List<string> cptions = new List<string>() { "BillingTable"};
                    AddTableProperties(mainDocument, cptions);
                    var table1 = documentManager.GetTable(mainDocument, "BillingTable");

                    List<string> cptions1 = new List<string>() { "BillingTable1" };
                    AddTableProperties(table1, cptions1);
                    var table2 = documentManager.GetTable(mainDocument, "BillingTable1");

                    List<string> cptions2 = new List<string>() {"BillingTable2", "BillingTable3" };
                    AddTableProperties(table2, cptions2);
 
                    documentManager.UpdateHeader(mainDocument, month, variety, "KARTHIKEYAN MODERN RICE MILL");


                    documentManager.UpdateArtcicles(mainDocument, Details, runPropertiesValues);

                    mainDocument.Save();
                    mainDocument.Dispose();

                }
                memoryStream.Position = 0;
                MemoryStream pdfFinalMemoryStream = new MemoryStream();
                using (RichEditDocumentServer wordProcessor = new RichEditDocumentServer())
                {
                    wordProcessor.LoadDocument(memoryStream);
                    wordProcessor.ExportToPdf(pdfFinalMemoryStream);
                }

                return pdfFinalMemoryStream.ToArray();


            }
        }
        #endregion

        public void AddTableProperties(WordprocessingDocument mainDocument,List<string>tableCaptions)
        {
            var tables = mainDocument.MainDocumentPart.Document.Body.Descendants<Table>();
            int tableIndex = 0;
            foreach (var table in tables)
            {
                if (tableIndex < tableCaptions.Count)
                {
                    // Add table properties if they do not exist
                    TableProperties tableProperties = table.GetFirstChild<TableProperties>();
                    if (tableProperties == null)
                    {
                        tableProperties = new TableProperties();
                        table.InsertAt(tableProperties, 0);
                    }

                    // Set the table title
                    TableCaption tableCaption = tableProperties.GetFirstChild<TableCaption>();
                    if (tableCaption == null)
                    {
                        tableCaption = new TableCaption();
                        tableProperties.AppendChild(tableCaption);
                    }
                    tableCaption.Val = tableCaptions[tableIndex];

                    //// Set the table tag
                    //TableTag tableTagElement = tableProperties.GetFirstChild<TableTag>();
                    //if (tableTagElement == null)
                    //{
                    //    tableTagElement = new TableTag();
                    //    tableProperties.AppendChild(tableTagElement);
                    //}
                    //tableTagElement.Val = tableTags[tableIndex];

                    tableIndex++;
                }
                else
                {
                    break; // Stop if there are no more titles or tags provided
                }
            }
            mainDocument.Save();


        }

        public void AddTableProperties(Table otable, List<string> tableCaptions)
        {
            var tables = otable.Descendants<Table>();
            int tableIndex = 0;
            foreach (var table in tables)
            {
                if (tableIndex < tableCaptions.Count)
                {
                    // Add table properties if they do not exist
                    TableProperties tableProperties = table.GetFirstChild<TableProperties>();
                    if (tableProperties == null)
                    {
                        tableProperties = new TableProperties();
                        table.InsertAt(tableProperties, 0);
                    }

                    // Set the table title
                    TableCaption tableCaption = tableProperties.GetFirstChild<TableCaption>();
                    if (tableCaption == null)
                    {
                        tableCaption = new TableCaption();
                        tableProperties.AppendChild(tableCaption);
                    }
                    tableCaption.Val = tableCaptions[tableIndex];

                    //// Set the table tag
                    //TableTag tableTagElement = tableProperties.GetFirstChild<TableTag>();
                    //if (tableTagElement == null)
                    //{
                    //    tableTagElement = new TableTag();
                    //    tableProperties.AppendChild(tableTagElement);
                    //}
                    //tableTagElement.Val = tableTags[tableIndex];

                    tableIndex++;
                }
                else
                {
                    break; // Stop if there are no more titles or tags provided
                }
            }
            
             
        }
    }
}
