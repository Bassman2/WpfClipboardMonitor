using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WpfClipboardMonitor
{
    /// <summary>
    /// Clipboard Monitor window class
    /// </summary>
    public class ClipboardMonitorWindow : Window
    {
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        private IntPtr windowHandle;

        /// <summary>
        /// Event for clipboard update notification.
        /// </summary>
        public event EventHandler ClipboardUpdate;

        /// <summary>
        /// Raises the System.Windows.FrameworkElement.Initialized event. This method is
        /// invoked whenever System.Windows.FrameworkElement.IsInitialized is set to true internally.
        /// </summary>
        /// <param name="e">The System.Windows.RoutedEventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            windowHandle = new WindowInteropHelper(this).EnsureHandle();
            HwndSource.FromHwnd(windowHandle)?.AddHook(HwndHandler);

            if (this.ClipboardNotification)
            {
                Start();
            }
        }

        /// <summary>
        /// Clipboard Update dependency property
        /// </summary>
        public static readonly DependencyProperty ClipboardUpdateCommandProperty =
            DependencyProperty.Register("ClipboardUpdateCommand", typeof(ICommand), typeof(ClipboardMonitorWindow), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// ICommand to be called on clipboard update.
        /// </summary>
        public ICommand ClipboardUpdateCommand
        {
            get { return (ICommand)GetValue(ClipboardUpdateCommandProperty); }
            set { SetValue(ClipboardUpdateCommandProperty, value); }
        }

        /// <summary>
        /// Clipboard Notification dependency property
        /// </summary>
        public static readonly DependencyProperty ClipboardNotificationProperty =
            DependencyProperty.Register("ClipboardNotification", typeof(bool), typeof(ClipboardMonitorWindow), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnClipboardNotificationChanged)));

        private static void OnClipboardNotificationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ClipboardMonitorWindow clipboardMonitorWindow = (ClipboardMonitorWindow)o;
            bool value = (bool)e.NewValue;
            if (value)
            {
                clipboardMonitorWindow.Start();
            }
            else
            {
                clipboardMonitorWindow.Stop();
            }
        }

        /// <summary>
        /// Enable clipboard notification.
        /// </summary>
        public bool ClipboardNotification
        {
            get { return (bool)GetValue(ClipboardNotificationProperty); }
            set { SetValue(ClipboardNotificationProperty, value); }
        }

        /// <summary>
        /// Override to handle clipboard notification.
        /// </summary>
        protected virtual void OnClipboardUpdate()
        { }

        /// <summary>
        /// Enable clipboard notification.
        /// </summary>
        public void Start()
        {
            if (this.windowHandle != null)
            {
                NativeMethods.AddClipboardFormatListener(this.windowHandle);
            }
        }

        /// <summary>
        /// Disable clipboard notification.
        /// </summary>
        public void Stop()
        {
            if (this.windowHandle != null)
            {
                NativeMethods.RemoveClipboardFormatListener(this.windowHandle);
            }
        }

        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                // fire event
                this.ClipboardUpdate?.Invoke(this, new EventArgs());
                // execute command
                if (this.ClipboardUpdateCommand?.CanExecute(null) ?? false)
                {
                    this.ClipboardUpdateCommand?.Execute(null);
                }
                // call virtual method
                OnClipboardUpdate();
            }
            handled = false;
            return IntPtr.Zero;
        }


        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AddClipboardFormatListener(IntPtr hwnd);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
        }
    }
}
