using System;
using System.Windows;
using System.Windows.Controls;

namespace Meowtrix.WPF.Extend.Controls
{
    public class FilePicker : Control
    {
        static FilePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilePicker), new FrameworkPropertyMetadata(typeof(FilePicker)));
        }

        public string Filename
        {
            get { return (string)GetValue(FilenameProperty); }
            set { SetValue(FilenameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Filename.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilenameProperty =
            DependencyProperty.Register("Filename", typeof(string), typeof(FilePicker), new PropertyMetadata(string.Empty));

        public event EventHandler<PropertyChangedEventArgs<string>> FilenameChanged;
    }
}
