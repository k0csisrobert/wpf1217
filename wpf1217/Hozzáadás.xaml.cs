using System.Windows;
using static wpf1217.MainWindow;

namespace wpf1217
{
    
    public partial class Hozzáadás : Window
    {

        public ItemModel ujtermek;
        public Hozzáadás()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int mennyiseg_ =0;
            int ar_ = 0;
            if (nev.Text=="" 
                || !int.TryParse(mennyiseg.Text,out mennyiseg_) 
                || !int.TryParse(ar.Text, out ar_)
                || kategoria.SelectedItem==null)
            {
                MessageBox.Show("Hiba",
                    "Nem megfelelő adatok a beviteli mezőkben!", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            ujtermek = new ItemModel(nev.Text, mennyiseg_, ar_, kategoria.Text);
            DialogResult = true;
        }
    }
}
