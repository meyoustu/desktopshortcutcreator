using System;

namespace DesktopShortcutCreator
{
    class Program
    {
        private Program() { /* Do nothing. */ }
        public string ShortcutName { get; set; }
        public string TargetPath { get; set; }
        public string HotKey { get; set; }
        public string Description { get; set; }
        public string WorkingDirectory { get; set; }
        public string Arguments { get; set; }
        public string WindowStyle { get; set; }
        public string ShortcutIconLocation { get; set; }

        static void Main(string[] args)
        {
            Instance.WorkingDirectory = "C:\\Users\\LiangchengJ\\Documents\\Chrome_Windows";
            Instance.TargetPath = "C:\\Users\\LiangchengJ\\Documents\\Chrome_Windows\\chrome.exe";
            Instance.Description = "Google Chrome";
            Instance.ShortcutName = "Chrome";
            Instance.Create();
        }

        private static volatile Program program = null;
        private static readonly object syncObj = new object();

        public static Program Instance
        {
            get
            {
                Program tmp = program;
                if (null == tmp)
                {
                    lock (syncObj)
                    {
                        tmp = program;
                        if (null == tmp)
                        {
                            tmp = new Program();
                            program = tmp;
                        }
                    }
                }
                return program;
            }
        }


        public bool Create()
        {
            try
            {
                string desktopPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}{Path.DirectorySeparatorChar}";
                string shortcutPath = $"{desktopPath}{Path.DirectorySeparatorChar}{ShortcutName}.lnk";

                if (File.Exists(shortcutPath))
                {
                    File.Delete(shortcutPath);
                }

                Type obj = Type.GetTypeFromCLSID(new Guid("00021401-0000-0000-C000-000000000046"), true);
                IShellLinkW lnk = Activator.CreateInstance(obj) as IShellLinkW;
                if (WorkingDirectory != string.Empty)
                {
                    lnk.SetWorkingDirectory(WorkingDirectory);
                }
                if (TargetPath != string.Empty)
                {
                    lnk.SetPath(TargetPath);
                }
                if (Description != string.Empty)
                {
                    lnk.SetDescription(Description);
                }

                ((IPersistFile)lnk).Save(shortcutPath, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
