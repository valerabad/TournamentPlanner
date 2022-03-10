using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using DAL.Interfaces;
using TournamentPlanner.DAL.Entities;

namespace BLL.Services
{
    public class ExcelService : IExcelService
    {
        IPlayerRepository playerRepository { get; set; }
        public ExcelService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }
        public MemoryStream ExportToExcel(IEnumerable<PlayerDTO> playerDTOs, string workSheetName)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Players");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = $"List of {workSheetName}";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A2"].Value = "Last name";
                worksheet.Cells["B2"].Value = "First name";
                worksheet.Cells["C2"].Value = "Birthday";
                worksheet.Cells["A2:C2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2:C2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A2:C2"].Style.Font.Bold = true;

                row = 3;
                foreach (var player in playerDTOs)
                {
                    worksheet.Cells[row, 1].Value = player.LastName;
                    worksheet.Cells[row, 2].Value = player.FirstName;
                    worksheet.Cells[row, 3].Value = player.Birthday;

                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = $"{workSheetName} List";
                xlPackage.Workbook.Properties.Author = "noname";
                xlPackage.Workbook.Properties.Subject = $"{workSheetName} List";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return stream;
        }

        public void ImportFromExcel(Stream stream, int startRow = 2)
        {
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.First();//package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (var row = startRow; row <= rowCount; row++)
                {
                    try
                    {
                        var firstName = worksheet.Cells[row, 1].Value?.ToString();
                        var lastName = worksheet.Cells[row, 2].Value?.ToString();
                        var birthDay = (DateTime?)worksheet.Cells[row, 3].Value; // ?? (DateTime)worksheet.Cells[row, 3].Value;

                        var players = new Player() // or PlayerDTO
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Birthday = birthDay
                        };
                        playerRepository.Create(players);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something went wrong");
                    }
                }
            }

        }
    }
}
