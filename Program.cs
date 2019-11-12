using System;
using System.Timers;
using System.Windows.Forms;

namespace PowerCableNotifier {
    static class Program {
        static NotifyIcon notifyIcon = new NotifyIcon();
        static System.Timers.Timer timer = new System.Timers.Timer();

        [STAThread]
        static void Main() {
            // TODO: Add new icon
            System.Drawing.Icon cableOffIcon = Properties.Resources.CableOff32x32;
            System.Drawing.Icon cableOnIcon = Properties.Resources.CableOn32x32;

            notifyIcon.Icon = cableOnIcon;

            notifyIcon.ContextMenuStrip = GetContext();
            notifyIcon.BalloonTipText = "Power cable is off!";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon.Visible = true;

            timer.Enabled = true;
            timer.Interval = 10000;
            timer.Start();

            timer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) => {
                if (SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Online) {
                    if (notifyIcon.Icon != cableOffIcon) {
                        notifyIcon.Icon = cableOffIcon;
                    }
                    notifyIcon.ShowBalloonTip(10000);
                } else {
                    if (notifyIcon.Icon != cableOnIcon) {
                        notifyIcon.Icon = cableOnIcon;
                    }
                }
            });

            Application.Run();
        }

        private static ContextMenuStrip GetContext() {
            ContextMenuStrip CMS = new ContextMenuStrip();
            CMS.Items.Add("About", null, new EventHandler(AboutClick));

            // TODO: Implement settings on Timer and customize notification
            // CMS.Items.Add("Settings", null, new EventHandler(SettingsClick));

            CMS.Items.Add("Exit", null, new EventHandler(ExitClick));

            return CMS;
        }

        private static void AboutClick(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/hangacs/PowerCableNotifier");
        }

        private static void SettingsClick(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private static void ExitClick (object sender, EventArgs e) {
            notifyIcon.Dispose();
            Application.Exit();
        }
    }
}

