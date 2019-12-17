using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NCScanner.DataTypes;

namespace NCScanner.Services.Interfaces
{
    public interface INCFileScannerService
    {
        NCData ScanNCFile(string filePath);
    }
}
