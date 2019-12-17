using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NCScanner.Services.Interfaces;

namespace NCScanner.Services.Classes
{
    class FileService : IFileService
    {
        public string BrowseForFile(string title, string filter, bool multiselect)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                Multiselect = multiselect
            };
            return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : string.Empty;
        }

        public (bool Result, string Path) SaveFileAs(string title, string filter, bool addExtension,
                                                     string fileName = "")
        {
            var saveFileAs = new SaveFileDialog
            {
                Title = title,
                Filter = filter,
                FileName = fileName,
                AddExtension = addExtension
            };
            return saveFileAs.ShowDialog() == DialogResult.OK ? (true, saveFileAs.FileName) : 
                                                                (false, string.Empty);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath) ? true : false;
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

    }
}
