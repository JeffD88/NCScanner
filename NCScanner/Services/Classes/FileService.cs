using System;
using System.Collections.Generic;
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
    }
}
