using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Shapes;
using Project.API.User;

namespace EmulateKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static void ButtonDown(Int16 key)
        {
            var inputs = new INPUT[] {
                KeyInput(key, (Int32)(KEYEVENTF.EXTENDEDKEY | KEYEVENTF.KEYDOWN)),
            };
            InputAPI.SendKey(inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
        }
        private static void ButtonUp(Int16 key)
        {
            var inputs = new INPUT[] {
                KeyInput(key, (Int32)(KEYEVENTF.EXTENDEDKEY | KEYEVENTF.KEYUP)),
            };
            InputAPI.SendKey(inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
        }

        private static INPUT KeyInput(Int16 key, Int32 keyflg)
        {
            return new INPUT {
                type = (Int32)TYPE_INPUT.KEYBOARD,
                ui = new INPUT_UNION {
                    keyboard = new KEYBDINPUT {
                        wVk = key,
                        wScan = (Int16)InputAPI.FindMapViratualKey(key, 0),
                        dwFlags = keyflg,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };
        }

        private void Panel_PreviewTouchDown(Object sender, System.Windows.Input.TouchEventArgs e)
        {
            var key = GetKeyCode(sender);
            ButtonDown(key);
        }

        private void Panel_PreviewTouchUp(Object sender, System.Windows.Input.TouchEventArgs e)
        {
            var key = GetKeyCode(sender);
            ButtonUp(key);
        }

        private static Int16 GetKeyCode(Object sender)
        {
            return sender is not Shape button
                ? (Int16)0
                : Convert.ToInt16(button.Tag);
        }
    }
}
