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

        (bool Result, string Path) SaveFileAs(string title, string filter, bool addExtension,
                                              string fileName = "");

        bool FileExists(string filePath);

        string GetFileName(string filePath);
    }
}
