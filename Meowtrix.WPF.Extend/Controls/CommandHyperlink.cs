using System.Windows;
using System.Windows.Documents;

namespace Meowtrix.WPF.Extend.Controls
{
    public class CommandHyperlink : Hyperlink
    {
        public string CommandLineString
        {
            get { return (string)GetValue(CommandLineStringProperty); }
            set { SetValue(CommandLineStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandLineString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandLineStringProperty =
            DependencyProperty.Register(nameof(CommandLineString), typeof(string), typeof(CommandHyperlink), new PropertyMetadata(null));

        protected override void OnClick()
        {
            if (CommandLineString != null)
                System.Diagnostics.Process.Start(CommandLineString);
        }
    }
}
