using DapperExtensions.Mapper;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using DevExpress.XtraSpreadsheet.Import.Xls;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using TNCSC.Hulling.Domain.Reports;

namespace TNCSC.Hulling.ServiceLayer.Export
{
    public class DocumentManager
    {
        #region UpdateReferenceValues
        /// <summary>
        /// UpdateReferenceValues
        /// </summary>
        /// <param name="mainDocument"></param>
        /// <param name="logBookReferenceValues"></param>
        public void UpdateReferenceValues(WordprocessingDocument mainDocument, Dictionary<string, string> logBookReferenceValues)
        {

            try
            {
                FindReplaceTextAllOccurances(mainDocument, logBookReferenceValues);

            }
            catch (Exception)
            {
                throw;

            }
        }
        #endregion

        #region FindReplaceTextAllOccurances
        /// <summary>
        /// FindReplaceTextAllOccurances
        /// </summary>
        /// <param name="document"></param>
        /// <param name="findAndReplaceDetails"></param>
        public void FindReplaceTextAllOccurances(WordprocessingDocument document, Dictionary<string, string> findAndReplaceDetails)
        {
            try
            {
                // Read the document text form original document
                string sDocumentInnerXML = document.MainDocumentPart.Document.InnerXml.ToString();

                // Find and Replace the content in document text
                foreach (var item in findAndReplaceDetails)
                {
                    Regex regexText = new Regex(item.Key);

                    var matches = regexText.Matches(sDocumentInnerXML);

                    if (matches.Count != 0)
                    {
                        sDocumentInnerXML = regexText.Replace(sDocumentInnerXML, Helper.XmlEncode(item.Value));
                    }

                }

                document.MainDocumentPart.Document.InnerXml = sDocumentInnerXML;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetTable

        /// <summary>
        /// GetTable
        /// </summary>
        /// <param name="wordprocessingDocument"></param>
        /// <param name="tableCaption"></param>
        /// <param name="IsCloneRequired"></param>
        /// <returns></returns>
        public Table GetTable(WordprocessingDocument wordprocessingDocument, string? tableCaption, bool IsCloneRequired = false)
        {
            try
            {
                if (wordprocessingDocument.MainDocumentPart?.Document.Body != null)
                {
                    var tables = wordprocessingDocument.MainDocumentPart.Document.Body.Descendants<Table>();

                    var table = GetTableBasedOnOccurance(tables, tableCaption);

                    if (table != null && IsCloneRequired)
                        return table.CloneNode(true) as Table;

                    return table;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetTableBasedOnOccurance
        /// <summary>
        /// GetTableBasedOnOccurance
        /// </summary>
        /// <param name="oTables"></param>
        /// <param name="tableCaption"></param>
        /// <param name="occuranceLevel"></param>
        /// <returns></returns>
        private Table GetTableBasedOnOccurance(IEnumerable<Table> oTables, string? tableCaption, int occuranceLevel = 1)
        {

            int tableFoundAt = 0;
            int tableOcceranceLevel = occuranceLevel;
            try
            {
                foreach (var table in oTables)
                {
                    var actualTablecaption = table.Descendants<TableCaption>().FirstOrDefault();
                    if (actualTablecaption != null && string.Compare(actualTablecaption.Val?.ToString()?.Trim().ToLower(), tableCaption != null ? tableCaption.Trim().ToLower() : string.Empty, true) == 0)
                    {
                        tableFoundAt++;
                        if (tableFoundAt == tableOcceranceLevel)
                            return table;
                    }

                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region UpdateArtcicles
        /// <summary>
        /// UpdateArtcicles
        /// </summary>
        /// <param name="mainDocument"></param>
        /// <param name="articles"></param>
        /// <param name="devices"></param>
        /// <param name="articleComments"></param>
        /// <param name="articleAttachments"></param>
        /// <param name="runProperties"></param>
        /// <param name="language"></param>
        public void UpdateArtcicles(WordprocessingDocument mainDocument, List<BillingPaddy>? details, RunProperties runProperties )
        {
            string billingTableTableName = "BillingTable";
            string billingTableTableName1 = "BillingTable1";
            string billingTableTableName2 = "BillingTable2";
            string billingTableTableName3 = "BillingTable3";


            try
            {
                Table BillingTableTable = GetTable(mainDocument, billingTableTableName);
                Table BillingTableTable1 = GetTable(mainDocument, billingTableTableName1);
                Table BillingTableTable2 = GetTable(mainDocument, billingTableTableName2);
                Table BillingTableTable3 = GetTable(mainDocument, billingTableTableName3);

                // Table articlesTable = GetTable(mainDocument, articlesTableName);

                int occuranceLevel = 1;
                for (int i = 0; i < details.Count(); i++)
                {
                    
                    
                    var report = details[i].Report ;
                    if (report != null && report.Count() > 0)
                    {
                        for (int j = 0; j < report.Count(); j++)
                        {
                            UpdateCellValue(BillingTableTable2, (j+1).ToString() , runProperties, false, j , 0);
                            UpdateCellValue(BillingTableTable2, report[j].Date.ToString("dd-MM-yy"), runProperties, false, j , 1);
                            UpdateCellValue(BillingTableTable2, report[j].IssueMemoNo, runProperties, false, j, 2);
                            UpdateCellValue(BillingTableTable2, report[j].PaddyWeight.ToString("F3"), runProperties, false, j , 3);
                            UpdateCellValue(BillingTableTable2, string.IsNullOrEmpty(report[j].ADNumber) ? " " : report[j].ADNumber, runProperties, false, j , 6);
                            UpdateCellValue(BillingTableTable2, (report[j].ADDate.ToString("dd-MM-yy") == "01-01-01") ? " " : report[j].ADDate.ToString("dd-MM-yy"), runProperties, false, j , 7);
                            UpdateCellValue(BillingTableTable2, (report[j].RiceWeight.ToString("F3") == "0.000") ? " " : report[j].RiceWeight.ToString("F3"), runProperties, false, j , 8);
                            UpdateCellValue(BillingTableTable2, (((int)Math.Round(report[j].TotalWeight)).ToString() == "0") ? " " : ((int)Math.Round(report[j].TotalWeight)).ToString(), runProperties, false, j, 9);
                            if (j < report.Count() - 1)
                            {
                                AppendRow(BillingTableTable2, 0);

                            }

                        }
                        UpdateCellValue(BillingTableTable3, ((int)Math.Round(details[i].TotalPaddyWeight)).ToString(), runProperties, false, 0, 2);
                        UpdateCellValue(BillingTableTable3, ((int)Math.Round(details[i].OutTurn)).ToString(), runProperties, false, 0, 3);
                        UpdateCellValue(BillingTableTable3, details[i].DueDate.ToString("dd-MM-yy"), runProperties, false, 0, 4);
                       // UpdateCellValue(BillingTableTable3, details[i].TotalRiceWeight.ToString("F3"), runProperties, false, 0, 4);
                    }
                   
                    if (i < details.Count() - 1)
                    {
                        AppendRow(BillingTableTable, 2);
                         

                        occuranceLevel = occuranceLevel + 1;
                        var BillingTableTables = BillingTableTable.Descendants<Table>();
                        var BillingTableTables1 = BillingTableTable1.Descendants<Table>();

                        //    atricleDetailsTable = GetTableBasedOnOccurance(artileTables, atricleDetailsTableName, occuranceLevel);

                        BillingTableTable2 = GetTableBasedOnOccurance(BillingTableTables, billingTableTableName2, occuranceLevel);
                        BillingTableTable2 = ClearTableRowsAndCellValues(BillingTableTable2, 1);

                        BillingTableTable3 = GetTableBasedOnOccurance(BillingTableTables, billingTableTableName3, occuranceLevel);
                       // BillingTableTable3 = ClearTableRowsAndCellValues(BillingTableTable3, 1);

                        

                    }

                }


            }
            catch (Exception)
            {
                return;

            }
        }
        #endregion


        public void UpdateCellValue(Table? table, string value, RunProperties runProperties, bool isAppend = false, int rowIndex = 0, int cellIndex = 0, bool isPageBreak = false)
        {
            try
            {
                if (table == null)
                    return;

                if (!string.IsNullOrEmpty(value))
                {
                    //string formattedText = RemoveInvalidCharacters(value);

                    var tableRow = table.Descendants<TableRow>().ElementAt(rowIndex);

                    TableRowProperties rowProperties = new TableRowProperties();
                    TableRowHeight rowHeight = new TableRowHeight() { Val = 200, HeightType = HeightRuleValues.AtLeast };
                    rowProperties.Append(rowHeight);
                    if (tableRow.GetFirstChild<TableRowProperties>() == null)
                    {
                        tableRow.PrependChild(rowProperties);
                    }

                    var tableCell = tableRow.Descendants<TableCell>().ElementAt(cellIndex);
                    TableCellProperties tcp = new TableCellProperties();
                    TableCellVerticalAlignment tcVA = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                    TableCellMargin cellMargins = new TableCellMargin();
                    
                    tcp.Append(tcVA);
                    tableCell.Append(tcp);

                    Paragraph? paragraph = tableCell.Descendants<Paragraph>().FirstOrDefault();

                    string[] newLineArray = { "\n" };
                    string[] textArray = value.Split(newLineArray, StringSplitOptions.None);

                    bool first = true;


                    if (paragraph != null)
                    {


                        if (isAppend)
                        {


                            if (isPageBreak)
                            {
                                paragraph.AppendChild(new Break() { Type = BreakValues.Page });
                            }

                            if (textArray.Length > 1)
                            {
                                foreach (string line in textArray)
                                {
                                    if (!first)
                                    {
                                        paragraph.Append(new Break());
                                    }

                                    first = false;

                                    paragraph.Append(new Run(runProperties.CloneNode(true), new Text(Helper.XmlEncode(line))));
                                }
                            }
                            else
                            {
                                paragraph.Append(new Run(runProperties.CloneNode(true), new Text(Helper.XmlEncode(value))));
                            }


                        }
                        else
                        {
                            paragraph.RemoveAllChildren();

                            if (textArray.Length > 1)
                            {
                                foreach (string line in textArray)
                                {
                                    if (!first)
                                    {
                                        paragraph.Append(new Break());
                                    }

                                    first = false;

                                    paragraph.Append(new Run(runProperties.CloneNode(true), new Text(Helper.XmlEncode(line))));
                                }
                            }
                            else
                            {
                                paragraph.Append(new Run(runProperties.CloneNode(true), new Text(Helper.XmlEncode(value))));
                            }

                        }
                    }
                }
                else
                {
                    return;
                }
            }

            catch (Exception)
            {
                throw;
            }

        }


        #region AppendRow
        /// <summary>
        /// AppendRow
        /// </summary>
        /// <param name="table"></param>
        /// <param name="cloneRowIndex"></param>
        /// <returns></returns>
        public TableRow? AppendRow(Table? table, int cloneRowIndex = 0)
        {
            try
            {
                if (table == null)
                    return null;

                var tableRow = table.Descendants<TableRow>().ElementAt(cloneRowIndex);

                var newTableRow = tableRow.CloneNode(true) as TableRow;

                table.AppendChild(newTableRow);

                return newTableRow;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region AddRow
        /// <summary>
        /// AddRow
        /// </summary>
        /// <param name="table"></param>
        /// <param name="cloneRowIndex"></param>
        /// <param name="insertAtLast"></param>
        /// <param name="insertAtIndex"></param>
        /// <returns></returns>
        public TableRow? AddRow(Table? table, int cloneRowIndex = 0, bool insertAtLast = true, int insertAtIndex = 0)
        {

            try
            {

                if (table == null)
                    return null;

                var tableRow = table.Descendants<TableRow>().ElementAt(cloneRowIndex);

                var newTableRow = tableRow.CloneNode(true) as TableRow;

                if (insertAtLast == true)
                {
                    table.Append(newTableRow);

                }
                else
                {
                    table.InsertAt(newTableRow, insertAtIndex);
                }

                return newTableRow;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region UpdateFooter
        /// <summary>
        /// UpdateFooter
        /// </summary>
        /// <param name="mainDocument"></param>
        /// <param name="fuportalID"></param>
        /// <param name="date"></param>
        public void UpdateFooter(WordprocessingDocument mainDocument, string fuportalID, string date)
        {
            try
            {

                UpdateFooterDetail(mainDocument, "##fuportalid##", fuportalID);
                UpdateFooterDetail(mainDocument, "##date##", date);

            }
            catch (Exception)
            {
                throw;

            }
        }
        #endregion

        #region UpdateFooterDetail
        /// <summary>
        /// UpdateFooterDetail
        /// </summary>
        /// <param name="document"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateFooterDetail(WordprocessingDocument document, string key, string value)
        {
            try
            {
                List<Table> tables = new List<Table>();

                foreach (FooterPart footer in document.MainDocumentPart.FooterParts)
                {
                    tables = footer.Footer.Elements<Table>().ToList();
                    //if (tables.Count > 0)
                    //    break;
                }
                if (tables.Count > 0)
                {
                    if (tables[1] != null)
                    {
                        string sTableInnerXML = tables[1].InnerXml.ToString();

                        Regex regexText = new Regex(key);

                        var matches = regexText.Matches(sTableInnerXML);

                        if (matches.Count != 0)
                        {
                            // matched content
                            sTableInnerXML = regexText.Replace(sTableInnerXML, value);
                        }

                        tables[1].InnerXml = sTableInnerXML;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region DeleteTable
        /// <summary>
        /// DeleteTable
        /// </summary>
        /// <param name="wordprocessingDocument"></param>
        /// <param name="tableName"></param>

        public void DeleteTable(WordprocessingDocument wordprocessingDocument, string tableName)
        {
            try
            {
                var table = GetTable(wordprocessingDocument, tableName);
                if (table != null)
                    table.Remove();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region DeleteTables
        /// <summary>
        /// DeleteTables
        /// </summary>
        /// <param name="wordprocessingDocument"></param>
        /// <param name="tableNames"></param>
        public void DeleteTables(WordprocessingDocument wordprocessingDocument, List<string> tableNames)
        {
            if (tableNames != null && tableNames.Count() > 0)
            {
                foreach (var item in tableNames)
                {
                    DeleteTable(wordprocessingDocument, item);
                }
            }
        }
        #endregion

        #region DeleteTableRows
        /// <summary>
        /// DeleteTableRows
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table? DeleteTableRows(Table table, int skipCount)
        {
            try
            {
                if (table != null)
                {
                    IEnumerable<TableRow> rowsToRemove = table.Descendants<TableRow>().Skip(skipCount);
                    foreach (TableRow row in rowsToRemove.ToList())
                    {
                        row.Remove();
                    }

                    return table;
                }
                return table;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region ClearTableRowsAndCellValues
        /// <summary>
        /// ClearTableRowsAndCellValues
        /// </summary>
        /// <param name="table"></param>
        /// <param name="skipCount"></param>
        /// <returns></returns>
        public Table ClearTableRowsAndCellValues(Table table, int skipCount)
        {
            try
            {
                if (table != null)
                {
                    var newTable = DeleteTableRows(table, skipCount);

                    if (newTable != null)
                    {
                        var row = table.Elements<TableRow>().LastOrDefault();
                        var cells = row.Elements<TableCell>();
                        foreach (var cell in cells)
                        {
                            cell.Elements<Paragraph>().FirstOrDefault()?.RemoveAllChildren();
                        }

                        return newTable;
                    }
                    return table;
                }
                return table;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    }
}
