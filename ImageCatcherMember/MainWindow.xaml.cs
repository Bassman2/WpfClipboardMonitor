using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfClipboardMonitor;

namespace ImageCatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;
        private ClipboardMonitor clipboardMonitor;

        public MainWindow()
        {
            InitializeComponent();

            this.path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Clipboard");
            Directory.CreateDirectory(this.path);

            this.clipboardMonitor = new ClipboardMonitor(this);
            this.clipboardMonitor.ClipboardUpdate += OnClipboardUpdate;
        }

        private void OnClipboardUpdate(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    string filePath = Path.Combine(this.path, $"Image_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg");
                    using (var file = File.Create(filePath))
                    {
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));
                        encoder.Save(file);
                    }
                }
            }
            catch
            { }
        }
    }
}
