using System.Collections.Generic;
using System.Windows.Forms;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.GetFilePathsService
{
    public class GetFilePathsService1 : IGetFilePaths
    {
        public string GetFilePath()
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return default;
        }

        public IEnumerable<string> GetFilePaths()
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileNames;
            }

            return new List<string>();
        }
    }
}
