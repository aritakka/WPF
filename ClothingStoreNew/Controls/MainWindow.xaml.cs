using ClothingStoreNew.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClothingStoreNew
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Products> ProductsList { get; set; }

        private List<Products> AllProducts;

        public MainWindow()
        {
            InitializeComponent();

            ProductsList = new ObservableCollection<Products>();

            DataContext = this;

            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var db = new Store123Entities())
            {
                AllProducts = db.Products.ToList();

                ProductsList.Clear();

                foreach (var item in AllProducts)
                {
                    ProductsList.Add(item);
                }
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
                return;

            Products product = button.Tag as Products;

            if (product == null)
                return;

            CartManager.Add(product);

            MessageBox.Show("Товар добавлен в корзину");
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow window = new CartWindow();

            window.ShowDialog();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow window = new AdminWindow();

            window.ShowDialog();
        }
    }
}