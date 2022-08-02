using System.IO;

namespace Tower_Defense
{
    static class FileReader
    {
        /// <summary>
        /// Returns all instances of specified name inside a text document
        /// </summary>
        public static string[] FindAllInfoOfName(string aPath, string aName, char aSeperator)
        {
            if (File.Exists(aPath))
            {
                if (new FileInfo(aPath).Length > 0)
                {
                    string[] tempFoundInfo;
                    int tempInfoSize = 0;
                    int tempAddInfo = 0;

                    string[] tempReadFile = File.ReadAllLines(aPath);
                    string[] tempFoundValues = new string[tempReadFile.Length];
                    for (int i = 0; i < tempReadFile.Length; i++)
                    {
                        string[] tempSplitText = tempReadFile[i].Split(aSeperator);
                        if (tempSplitText[0] == aName)
                        {
                            tempInfoSize++;
                            tempFoundValues[i] = tempSplitText[1];
                        }
                    }

                    tempFoundInfo = new string[tempInfoSize];
                    for (int i = 0; i < tempFoundValues.Length; i++)
                    {
                        if (tempFoundValues[i] != null)
                        {
                            tempFoundInfo[tempAddInfo] = tempFoundValues[i];
                            tempAddInfo++;
                        }
                    }
                    return tempFoundInfo;
                }
            }
            return new string[0];
        }

        /// <summary>
        /// Returns first instance of specified name inside a text document
        /// </summary>
        public static string FindInfoOfName(string aPath, string aName, char aSeperator)
        {
            if (File.Exists(aPath))
            {
                if (new FileInfo(aPath).Length > 0)
                {
                    string[] tempReadFile = File.ReadAllLines(aPath);
                    for (int i = 0; i < tempReadFile.Length; i++)
                    {
                        string[] tempSplitText = tempReadFile[i].Split(aSeperator);
                        if (tempSplitText[0] == aName)
                        {
                            return tempSplitText[1];
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string[] FindAllInfo(string aPath, char aSeperator, params string[] aName)
        {
            if (File.Exists(aPath))
            {
                if (new FileInfo(aPath).Length > 0)
                {
                    string[] tempFoundInfo;
                    int tempInfoSize = 0;
                    int tempAddInfo = 0;

                    string[] tempReadFile = File.ReadAllLines(aPath);
                    string[] tempFoundValues = new string[tempReadFile.Length];
                    for (int i = 0; i < tempReadFile.Length; i++)
                    {
                        string[] tempSplitText = tempReadFile[i].Split(aSeperator);
                        for (int j = 0; j < aName.Length; j++)
                        {
                            if (tempSplitText[0] == aName[j])
                            {
                                tempInfoSize++;
                                tempFoundValues[i] = tempSplitText[1];
                            }
                        }
                    }

                    tempFoundInfo = new string[tempInfoSize];
                    for (int i = 0; i < tempFoundValues.Length; i++)
                    {
                        if (tempFoundValues[i] != null)
                        {
                            tempFoundInfo[tempAddInfo] = tempFoundValues[i];
                            tempAddInfo++;
                        }
                    }
                    return tempFoundInfo;
                }
            }
            return new string[0];
        }

        /// <summary>
        /// Returns all file names within a specified folder
        /// <returns></returns>
        public static string[] FindFileNames(string aPath)
        {
            DirectoryInfo tempInfo = new DirectoryInfo(aPath);
            FileInfo[] tempFiles = tempInfo.GetFiles("*.txt");

            if (tempFiles.Length > 0)
            {
                string[] tempFileNames = new string[tempFiles.Length];
                for (int i = 0; i < tempFiles.Length; i++)
                {
                    tempFileNames[i] = tempFiles[i].Name;
                    tempFileNames[i] = tempFileNames[i].Replace(".txt", "");
                }

                return tempFileNames;
            }
            return new string[0];
        }
    }
}
