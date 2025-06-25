using ClosedXML.Excel;
using RagAssistant.Share.Interfaces.Managers;
using System.Text;

namespace RagAssistant.Share.Managers
{
    public class MsExcelManagers : IMsExcelManagers
    {
        public string ExtractText(Stream stream)
        {
            using XLWorkbook workbook = new(stream);
            StringBuilder result = new();

            foreach (IXLWorksheet sheet in workbook.Worksheets)
            {
                result.AppendLine($"Sheet: {sheet.Name}");

                // get header row
                IXLRow? headerRow = sheet.FirstRowUsed();
                if (headerRow == null)
                {
                    continue;
                }

                List<string> headers = headerRow.Cells().Select(cell => cell.GetString().Trim()).ToList();

                // get content rows
                IEnumerable<IXLRow> dataRows = sheet.RowsUsed().Where(row => row.RowNumber() > headerRow.RowNumber());

                int rowIndex = 1;
                foreach (IXLRow? row in dataRows)
                {
                    List<string> rowParts = [];

                    // combind header and value
                    List<IXLCell> cells = row.Cells().ToList();
                    for (int i = 0; i < headers.Count; i++)
                    {
                        string header = headers[i];
                        string value = i < cells.Count ? cells[i].GetString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Trim() : string.Empty;
                        rowParts.Add($"{header}: {value}");
                    }

                    // prepare row content
                    result.AppendLine($"Row {rowIndex++}: {string.Join(" | ", rowParts)}");
                }

                // new line for split each row
                result.AppendLine();
            }

            return result.ToString().Trim();
        }
    }
}
