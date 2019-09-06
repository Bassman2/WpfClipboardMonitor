using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageCatcher
{
    public class MainViewModel
    {
        private string path;

        public DelegateCommand ClipboardUpdateCommand { get; private set; }

        public MainViewModel()
        {
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);

            this.path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Clipboard");
            Directory.CreateDirectory(this.path);
        }

        public void OnClipboardUpdate()
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

        public bool OnCanClipboardUpdate()
        {
            return true;
        }
    }
}
