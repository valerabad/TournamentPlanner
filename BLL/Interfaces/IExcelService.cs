using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IExcelService
    {
        MemoryStream ExportToExcel(IEnumerable<PlayerDTO> playerDTOs, string workSheetName);
        void ImportFromExcel(Stream stream, int startRow = 2);
    }
}
