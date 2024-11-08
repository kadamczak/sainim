using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImageMagick;

namespace sainim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RenderImage("dra.psd");
        }

        private async void RenderImage(string fileName)
        {
            string samplesFolder = @".\Assets\Samples\";
            string outputFolder = @".\Assets\Samples\Outputs\";
            string inputPath = samplesFolder + fileName;

            //using var imageData = new MagickImage(inputPath);
            using var layeredImageData = new MagickImageCollection(inputPath);

            layeredImageData.RemoveAt(0);

            var imageSpace = (Image)this.FindName("ImageSpace");

            var frame1 = new MagickImageCollection(layeredImageData.Where(l => l.Label.StartsWith("1-"))).Merge().ToBitmapSource();
            var frame2 = new MagickImageCollection(layeredImageData.Where(l => l.Label.StartsWith("2-"))).Merge().ToBitmapSource();
            var frame3 = new MagickImageCollection(layeredImageData.Where(l => l.Label.StartsWith("3-"))).Merge().ToBitmapSource();

            imageSpace.Source = frame1;
        }
    }
}