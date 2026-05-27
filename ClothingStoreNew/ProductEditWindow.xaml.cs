using System.Linq;
using System.Windows;

namespace ClothingStoreNew
{
    public partial class ProductEditWindow : Window
    {
        private Products _product;

        public ProductEditWindow(Products product)
        {
            InitializeComponent();

            _product = product;

            if (_product != null)
            {
                NameBox.Text = _product.Name;

                PriceBox.Text =
                    _product.Price.ToString();
            }
        }

        private void Save_Click(
            object sender,
            RoutedEventArgs e)
        {
            using (var db = new Store123Entities())
            {
                if (_product == null)
                {
                    Products product =
                        new Products();

                    product.Name =
                        NameBox.Text;

                    product.Price =
                        decimal.Parse(
                            PriceBox.Text);

                    db.Products.Add(product);
                }
                else
                {
                    Products product =
                        db.Products.First(
                            x => x.Id ==
                            _product.Id);

                    product.Name =
                        NameBox.Text;

                    product.Price =
                        decimal.Parse(
                            PriceBox.Text);
                }

                db.SaveChanges();
            }

            DialogResult = true;

            Close();
        }

        private void Delete_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (_product == null)
                return;

            using (var db = new Store123Entities())
            {
                Products product =
                    db.Products.First(
                        x => x.Id ==
                        _product.Id);

                db.Products.Remove(product);

                db.SaveChanges();
            }

            DialogResult = true;

            Close();
        }
    }
}