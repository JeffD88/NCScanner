using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NCScanner.DataTypes;

namespace NCScanner.Services.Interfaces
{
    public interface IExcelService
    {
        bool CreateReport(NCData ncData, string filePath);
    }
}
