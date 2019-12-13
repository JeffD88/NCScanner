using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCScanner.Services.Interfaces
{
    public interface IFileService
    {
        string BrowseForFile(string title, string filter, bool multiselect);
    }
}
