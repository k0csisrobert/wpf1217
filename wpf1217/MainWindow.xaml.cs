using System.Windows;

namespace wpf1217
{
    public partial class MainWindow : Window
    {

        public class Termekek()
        {
            public string Nev { get; set; }

            public int Db { get; set; }

        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void hozzaadas(object sender, RoutedEventArgs e)
        {

        }
    }
}