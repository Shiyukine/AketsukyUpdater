using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AketsukyUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                string fold = AppDomain.CurrentDomain.BaseDirectory;
                string temp = fold + "DownloadTemp";
                if (e.Args.Length > 0 && e.Args[0] == "--move-files")
                {
                    string title = e.Args[1].Split('=')[1];
                    AketsukyUpdater.MainWindow mw = new AketsukyUpdater.MainWindow();
                    mw.Title = title;
                    mw.img.Source = new BitmapImage(new Uri("/AketsukyUpdater;component/Resources/" + title + ".ico", UriKind.RelativeOrAbsolute));
                    //mw.Icon = new BitmapImage(new Uri("/AketsukyUpdater;component/Resources/" + title + ".ico", UriKind.RelativeOrAbsolute));
                    mw.Show();
                    Thread.Sleep(1000);
                    foreach (string f in Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories))
                    {
                        string s = fold + Path.GetFullPath(f).Replace(temp, "");
                        if (File.Exists(s)) File.Delete(s);
                        //
                        if (!Directory.Exists(Path.GetDirectoryName(s))) Directory.CreateDirectory(Path.GetDirectoryName(s));
                        File.Move(f, s);
                    }
                    Directory.Delete(temp, true);
                    Process.Start(fold + title + ".exe");
                    App.Current.Shutdown();
                }
                else App.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Stacktrace :\n" + ex.StackTrace);
                App.Current.Shutdown();
            }
        }
    }
}
