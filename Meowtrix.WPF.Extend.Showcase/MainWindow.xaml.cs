using System.Text;
using System.Windows;

namespace Meowtrix.WPF.Extend.Showcase
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeAnimateProgress(object sender, RoutedEventArgs e)
        {
            if (animateprogress.Value != animateprogress.Maximum)
                animateprogress.Value = animateprogress.Maximum;
            else animateprogress.Value = animateprogress.Minimum;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            SystemThemeHelper.SetTheme("aero", "normalcolor");
        }

        private void ShowBFS(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var c in this.BFS())
                sb.AppendLine(c.ToString());
            MessageBox.Show(sb.ToString());
        }

        private void ShowDFS(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var c in this.DFS())
                sb.AppendLine(c.ToString());
            MessageBox.Show(sb.ToString());
        }
    }
}
