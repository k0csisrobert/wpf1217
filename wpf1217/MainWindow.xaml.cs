using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf1217
{
    public class ItemModel
    {
        public String Nev { get; set; }
        public int Mennyiség { get; set; }
        public int Ár { get; set; }
        public String Kategória { get; set; }

        public int Összesen { get; set; }
        public ItemModel(string nev, int mennyiség, int ár, string kategória)
        {
            Nev = nev;
            Mennyiség = mennyiség;
            Ár = ár;
            Kategória = kategória;
            Összesen = Mennyiség * Ár;
        }
    }
    public partial class MainWindow : Window
    {
        List<ItemModel> termekek = new List<ItemModel>();
        public MainWindow()
        {
            InitializeComponent();
            termekek.Add(new ItemModel("Tej", 5, 450, "A"));
            termekek.Add(new ItemModel("Kenyer", 10, 350, "B"));
            termekek.Add(new ItemModel("Kenyer", 12, 400, "B"));
            termekek.Add(new ItemModel("Sajt", 2, 1200, "A"));
            termekek.Add(new ItemModel("Alma", 20, 200, "C"));
            termekek.Add(new ItemModel("Alma", 22, 250, "C"));
            termekek.Add(new ItemModel("Narancs", 15, 300, "C"));
            termekek.Add(new ItemModel("Hús", 3, 2500, "D"));
            termekek.Add(new ItemModel("Csokoládé", 7, 900, "B"));
            termekek.Add(new ItemModel("Kenyer", 1, 450, "B"));
            termekek.Add(new ItemModel("Tej", 12, 400, "A"));
            termekek.Add(new ItemModel("Sajt", 5, 1500, "D"));
            dataGrid.ItemsSource = termekek;
            priceProgressBar.Minimum = termekek.Min(x => x.Ár);
            priceProgressBar.Maximum = termekek.Max(x => x.Ár);
        }

        private void hozza_adas(object sender, RoutedEventArgs e)
        {
            var ujtermek = new Hozzaadas();

            if (ujtermek.ShowDialog() == true)
            {
                termekek.Add(ujtermek.ujtermek);
                dataGrid.ItemsSource = termekek;
                dataGrid.Items.Refresh();
                priceProgressBar.Minimum = termekek.Min(x => x.Ár);
                priceProgressBar.Maximum = termekek.Max(x => x.Ár);
            }

        }

        private void torles(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is ItemModel)
            {
                termekek.Remove((ItemModel)dataGrid.SelectedItem);
                dataGrid.ItemsSource = termekek;
                dataGrid.Items.Refresh();
                priceProgressBar.Minimum = termekek.Min(x => x.Ár);
                priceProgressBar.Maximum = termekek.Max(x => x.Ár);
            }
        }

        private void aTipus3Draga(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategória == "A").OrderByDescending(x => x.Összesen).Take(3);
        }

        private void Top5Osszerint(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(t => t.Összesen).Take(5);
        }
        private void tobbMint1(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Mennyiség > 1).Select(k => new { Nev = k.Nev, Összeg = k.Összesen });
        }
        private void reset(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek;
        }

        private void csokken(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(x => x.Ár);
        }

        private void dTipus500felett(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategória == "D" && t.Ár > 500);
        }

        private void nevOsszABC(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev)
                .Select(g => new { g.Nev, g.Összesen });
        }

        private void darabEsOsszertek(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev)
                .GroupBy(g => g.Kategória)
                .Select(g => new { Típus = g.Key, Darab = g.Sum(t => t.Mennyiség), Összesen = g.Sum(t => t.Összesen) });
        }

        private void tipusAtlagar(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => t.Kategória)
                .Select(g => new
                {
                    Kategória = g.Key,
                    Átlagár = Math.Round(g.Average(t => t.Ár), 2)
                });
        }

        private void highestTotalByCategoryBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => t.Kategória).Select(
                g => new
                {
                    Kategória = g.Key,
                    Összérték = g.Max(g => g.Összesen)
                });
        }

        private void bcEzerKisebb(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => (t.Kategória == "B" || t.Kategória == "C") && t.Ár < 1000);
        }
        private void felett500csoport(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek
                .Where(c => c.Ár > 500)
                .GroupBy(x => x.Kategória)
                .Select(v => new { Kategória = v.Key, TermékekSzáma = v.Count() });

        }

        private void kevesebbMint10esKisebb1000(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek
                .Where(t => t.Mennyiség > 10 && t.Ár < 1000)
                .OrderBy(t => t.Ár);
        }

        private void OsszN2000ABC(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Összesen > 2000).OrderBy(x => x.Nev);
        }

        private void termeknevTipusCsoportositas(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => new { Nev = t.Nev, Kategoria = t.Kategória })
                .Select(x => new { TermekNev = x.Key.Nev, Kategoria = x.Key.Kategoria, Darab = x.Count() });
        }


        private void LegertekesebbTipusonkent(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(item => item.Kategória).Select(x => new { Kategoria = x.Key, Ar = x.Max(s => s.Ár) });
        }

        private void OsszesitettDbTipusonkent(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(f => f.Kategória).Select(g => new { Kategoria = g.Key, Darab = g.Sum(q => q.Mennyiség) });
        }

        private void nullaFt(object sender, RoutedEventArgs e)
        {
            var nullaFtosTermek = termekek.Any(x => x.Ár == 0);

            if (nullaFtosTermek == false)
            {
                MessageBox.Show("Nincs nulla Ft-os termék.");
            }
            else
            {
                MessageBox.Show("Van nulla Ft-os termék.");
            }
        }

        private void kenyerek(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Nev.Contains("Kenyer")).OrderByDescending(c => c.Ár);

        }

        private void egyezoAr(object sender, RoutedEventArgs e)
        {
            var egyformak = termekek.GroupBy(x => x.Ár)
                .Select(g => new { darab = g.Count() })
                .Any(z => z.darab > 1);

            if (egyformak == true)
            {
                MessageBox.Show($"Van olyan adataink, amelyeknek megeggyezik az ára!");
            }
            else
            {
                MessageBox.Show($"Nincs olyan adat amely egy másik adat árával megeggyezne, mind egyedi");
            }
        }

        private void Valtozas(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Nev.ToLower().Contains(textBox.Text.ToLower())).ToList();
        }

        private void egyezo(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek
                .GroupBy(t => t.Nev)
                .Where(t => t.Count() > 1)
                .SelectMany(t => t);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is ItemModel selectedItem)
            {

                priceProgressBar.Value = selectedItem.Ár;
            }
        }

        private void nemc(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategória != "C");
        }

        private void nevHosszSzerint(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev.Length);
        }

        private void aTipusOsszAr(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek
                .Where(t => t.Kategória == "A")
                .OrderBy(t => t.Nev)
                .Select(t => new
                {
                    Kategória = t.Kategória,
                    Név = t.Nev,
                    Összes = t.Összesen
                });
        }
    }
}