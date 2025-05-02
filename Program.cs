 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeBreak
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApp());
        }
    }

    public class TrayApp : Form
    {
        private Icon iconGotCoffee;
        private Icon iconGettingCoffee;

        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);

        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_DISPLAY_REQUIRED = 0x00000002;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private bool atDesk = false;

        public TrayApp()
        {
            iconGotCoffee = new Icon("atdesk.ico");
            iconGettingCoffee = new Icon("away.ico");

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Toggle Mode", ToggleMode);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon = new NotifyIcon()
            {
                Text = "Mode: Getting Coffee",
                Icon = iconGettingCoffee,
                ContextMenu = trayMenu,
                Visible = true
            };

            trayIcon.DoubleClick += ToggleMode;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void ToggleMode(object sender, EventArgs e)
        {
            if (atDesk)
            {
                SetThreadExecutionState(ES_CONTINUOUS);
                trayIcon.Text = "Mode: Getting Coffee";
                trayIcon.Icon = iconGettingCoffee;
            }
            else
            {
                SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
                trayIcon.Text = "Mode: Got Coffee";
                trayIcon.Icon = iconGotCoffee;
            }
            atDesk = !atDesk;
        }

        private void OnExit(object sender, EventArgs e)
        {
            SetThreadExecutionState(ES_CONTINUOUS); // Reset to normal
            trayIcon.Visible = false;
            Application.Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Hide();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && trayIcon != null)
            {
                iconGettingCoffee?.Dispose();
                iconGotCoffee?.Dispose();
                trayIcon?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
