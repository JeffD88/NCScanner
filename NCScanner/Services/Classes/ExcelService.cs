using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ClosedXML.Excel;

using NCScanner.DataTypes;
using NCScanner.Services.Interfaces;

namespace NCScanner.Services.Classes
{
    class ExcelService : IExcelService
    {
        public bool CreateReport(NCData ncData, string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("NCScanner Report");

                    var toolColumn = worksheet.Column(1);                  
                    toolColumn.Cell(1).Value = "TOOLS";

                    var index = 2;
                    foreach (var tool in ncData.Tools)
                    {
                        toolColumn.Cell(index).Value = tool;

                        index++;
                    }

                    var workOffsetColumn = worksheet.Column(2);
                    workOffsetColumn.Cell(1).Value = "WORK OFFSETS";

                    index = 2;
                    foreach (var workOffset in ncData.WorkOffsets)
                    {
                        workOffsetColumn.Cell(index).Value = workOffset;

                        index++;
                    }

                    var minColumn = worksheet.Column(3);
                    minColumn.Cell(1).Value = "MIN";

                    minColumn.Cell(2).Value = "X:";
                    minColumn.Cell(3).Value = "Y:";
                    minColumn.Cell(4).Value = "Z:";

                    var minValuesColumn = worksheet.Column(4);

                    minValuesColumn.Cell(2).Value = ncData.XMin;
                    minValuesColumn.Cell(3).Value = ncData.YMin;
                    minValuesColumn.Cell(4).Value = ncData.ZMin;

                    var maxColumn = worksheet.Column(5);
                    maxColumn.Cell(1).Value = "MAX";

                    maxColumn.Cell(2).Value = "X:";
                    maxColumn.Cell(3).Value = "Y:";
                    maxColumn.Cell(4).Value = "Z:";

                    var maxValuesColumn = worksheet.Column(6);

                    maxValuesColumn.Cell(2).Value = ncData.XMax;
                    maxValuesColumn.Cell(3).Value = ncData.YMax;
                    maxValuesColumn.Cell(4).Value = ncData.ZMax;

                    workbook.SaveAs(filePath);
                }
                return true;
            }
            catch (Exception e)
            {
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
