using System;
using System.ComponentModel;
using System.IO;

namespace DelDeepFolder
{
    class Helpers
    {
        public static bool DeleteDeepFolder(string folderPath, BackgroundWorker backgroundWorker)
        {
            // Get the subdirectories for the specified directory.
            try
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                if (!dir.Exists)
                {
                    backgroundWorker.ReportProgress(100, "Path is not exist: " + folderPath);
                    return false;
                }

                // Get the files in the directory and delete them.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        backgroundWorker.ReportProgress(100, "Delete file failed: " + ex.Message);
                        return false;
                    }
                }

                // Delete subfolders and files
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                    DeleteDeepFolder(subdir.FullName, backgroundWorker);
                }

                // Delete itself
                try
                {
                    dir.Delete();
                    return true;
                }
                catch (Exception ex)
                {
                    backgroundWorker.ReportProgress(100, "Delete folder failed: " + ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                backgroundWorker.ReportProgress(100, "Exception: " + ex.Message);
                return false;
            }
        }
    }
}
