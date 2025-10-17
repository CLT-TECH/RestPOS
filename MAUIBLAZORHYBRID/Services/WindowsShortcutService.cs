using MAUIBLAZORHYBRID.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
#if WINDOWS
    using Windows.ApplicationModel;
    public class WindowsShortcutService : IShortcutService
    {
        public void CreateDesktopShortcut()
        {
            try
            {
                var appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var shortcutPath = Path.Combine(desktopPath, "POS Application.lnk");

                // Create using VBScript
                string vbsScript = $@"
Set oWS = WScript.CreateObject(""WScript.Shell"")
sLinkFile = ""{shortcutPath}""
Set oLink = oWS.CreateShortcut(sLinkFile)
oLink.TargetPath = ""{appPath}""
oLink.WorkingDirectory = ""{Path.GetDirectoryName(appPath)}""
oLink.Description = ""POS Application""
oLink.Save
";

                string vbsFilePath = Path.GetTempFileName() + ".vbs";
                File.WriteAllText(vbsFilePath, vbsScript);

                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cscript";
                process.StartInfo.Arguments = $"//Nologo \"{vbsFilePath}\"";
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();

                File.Delete(vbsFilePath);

                Console.WriteLine("✅ Desktop shortcut created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error creating shortcut: {ex.Message}");
            }
        }

        public bool ShortcutExists()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var shortcutPath = Path.Combine(desktopPath, "POS Application.lnk");
            return File.Exists(shortcutPath);
        }
    }

#endif
}
