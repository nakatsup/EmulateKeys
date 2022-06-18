using System;
using System.Windows;
using System.Windows.Interop;
using static Project.Windows.WindowStyleAPI;

namespace EmulateKeys
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            var main = new MainWindow();
            ToSlipThrough(main);
            main.Topmost = true;
            main.Show();
        }

        private static void ToSlipThrough(Window window)
        {
            // 初期化イベントでウインドウスタイルを設定するように関数を設定
            window.SourceInitialized += (sender, e) =>
            {
                IntPtr handle = new WindowInteropHelper(window).Handle;
                var style = NativeMethod.GetLong(handle, GWL.EXSTYLE);
                _ = NativeMethod.SetLong(handle, GWL.EXSTYLE, style | WS_EX.NOACTIVATE);
            };
        }
    }
}
