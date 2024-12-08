using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace sainim.WPF.CustomUI
{
    public class ToggleIconButtonControl : ButtonBase
    {
        static ToggleIconButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleIconButtonControl), new FrameworkPropertyMetadata(typeof(ToggleIconButtonControl)));
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ToggleIconButtonControl), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        //public static readonly DependencyProperty CheckedImageSourceProperty =
        //   DependencyProperty.Register("CheckedImageSource", typeof(ImageSource), typeof(ToggleIconButtonControl), new PropertyMetadata(null));

        //public ImageSource CheckedImageSource
        //{
        //    get { return (ImageSource)GetValue(CheckedImageSourceProperty); }
        //    set { SetValue(CheckedImageSourceProperty, value); }
        //}

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleIconButtonControl), new PropertyMetadata(false));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
    }
}