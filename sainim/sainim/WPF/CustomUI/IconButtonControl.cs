using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace sainim.WPF.CustomUI
{
    /// <summary>
    /// The <c>IconButton</c> class defines property fields of the IconButton control.
    /// </summary>
    public class IconButtonControl : ButtonBase
    {
        static IconButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButtonControl), new FrameworkPropertyMetadata(typeof(IconButtonControl)));
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(IconButtonControl), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
}