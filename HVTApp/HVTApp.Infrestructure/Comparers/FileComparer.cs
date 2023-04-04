using System.IO;

namespace HVTApp.Infrastructure.Comparers
{
    public static class FileComparer
    {
        /// <summary>
        /// Метод сравнивания файлов - одинаковые ли они
        /// </summary>
        /// <param name="file1Path"></param>
        /// <param name="file2Path"></param>
        /// <returns></returns>
        public static bool CheckFilesEquality(string file1Path, string file2Path)
        {
            int file1Byte;
            int file2Byte;

            // Determine if the same file was referenced two times.
            if (file1Path == file2Path)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            var fs1 = new FileStream(file1Path, FileMode.Open);
            var fs2 = new FileStream(file2Path, FileMode.Open);

            // Check the file sizes. If they are not the same, the files
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1Byte = fs1.ReadByte();
                file2Byte = fs2.ReadByte();
            }
            while (file1Byte == file2Byte && 
                   file1Byte != -1);

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is
            // equal to "file2byte" at this point only if the files are
            // the same.
            return file1Byte - file2Byte == 0;
        }
    }
}