using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClothingStoreNew
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            using (var db = new Store123Entities())
            {
                ProductsGrid.ItemsSource =
                    db.Products.ToList();

                UsersGrid.ItemsSource =
                    db.Users.ToList();

                OrdersGrid.ItemsSource =
                    db.Orders.ToList();

                CategoryBox.ItemsSource =
                    db.Categories.ToList();
            }
        }

        private void AddProduct(
            object sender,
            RoutedEventArgs e)
        {
            using (var db = new Store123Entities())
            {
                Products product = new Products();

                product.Name = NameBox.Text;

                product.Price =
                    decimal.Parse(PriceBox.Text);

                product.Description =
                    DescriptionBox.Text;

                product.Quantity = 1;

                Categories category =
                    CategoryBox.SelectedItem
                    as Categories;

                if (category != null)
                {
                    product.CategoryId =
                        category.Id;
                }

                db.Products.Add(product);

                db.SaveChanges();
            }

            LoadData();
        }

        private void UpdateProduct(
            object sender,
            RoutedEventArgs e)
        {
            Products selected =
                ProductsGrid.SelectedItem
                as Products;

            if (selected == null)
                return;

            using (var db = new Store123Entities())
            {
                Products product =
                    db.Products.First(
                        x => x.Id == selected.Id);

                product.Name =
                    NameBox.Text;

                product.Price =
                    decimal.Parse(PriceBox.Text);

                product.Description =
                    DescriptionBox.Text;

                db.SaveChanges();
            }

            LoadData();
        }

        private void DeleteProduct(
            object sender,
            RoutedEventArgs e)
        {
            Products selected =
                ProductsGrid.SelectedItem
                as Products;

            if (selected == null)
                return;

            using (var db = new Store123Entities())
            {
                Products product =
                    db.Products.First(
                        x => x.Id == selected.Id);

                db.Products.Remove(product);

                db.SaveChanges();
            }

            LoadData();
        }

        private void ProductsGrid_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            Products product =
                ProductsGrid.SelectedItem
                as Products;

            if (product == null)
                return;

            NameBox.Text =
                product.Name;

            PriceBox.Text =
                product.Price.ToString();

            DescriptionBox.Text =
                product.Description;
        }
    }
}