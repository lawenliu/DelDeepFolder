using DelDeepFolder.Properties;

namespace DelDeepFolder
{
    class Configure
    {
        #region folder path
        public static string GetFolderPath()
        {
            string value = Settings.Default[Constants.KeyFolderPath].ToString();
            return value;
        }

        public static void SaveFolderPath(string value)
        {
            Settings.Default[Constants.KeyFolderPath] = value;
            Settings.Default.Save();
        }
        #endregion
    }
}
