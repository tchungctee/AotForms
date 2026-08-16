using System;
using System.Drawing;
using System.Windows.Forms;

namespace AotForms
{
    internal static class UIUtils
    {
        // 48; 48; 47
        internal static void ElipseControl(Control control, int elipse)
        {
            var region = WinAPI.CreateRoundRectRgn(0, 0, control.Width, control.Height, elipse, elipse);

            if (region == IntPtr.Zero)
            {
                return;
            }

            try
            {
                var previousRegion = control.Region;
                control.Region = Region.FromHrgn(region);
                previousRegion?.Dispose();
            }
            finally
            {
                WinAPI.DeleteObject(region);
            }
        }

        internal static void MovableForm(Form form)
        {
            MovableControl(form, form);
        }

        internal static void MovableControl(Control control, Control target)
        {
            control.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    WinAPI.ReleaseCapture();
                    WinAPI.SendMessage(target.Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
                }
            };
        }
    }
}
