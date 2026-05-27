using ClothingStoreNew.Services;
using ClothingStoreNew.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace ClothingStoreNew
{
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();

            LoadCart();
        }

        private void LoadCart()
        {
            var cart = CartManager.GetFullCart()
                .Select(x => new CartItem
                {
                    ProductId = x.product.Id,
                    Name = x.product.Name,
                    Price = x.product.Price,
                    Quantity = x.qty
                })
                .ToList();

            CartGrid.ItemsSource = cart;

            TotalText.Text = cart.Sum(x => x.Total).ToString("0.00");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            CartItem item = ((FrameworkElement)sender).Tag as CartItem;

            if (item == null)
                return;

            CartItems existing =
                CartManager.Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existing != null)
            {
                if (existing.Quantity > 1)
                    existing.Quantity--;
                else
                    CartManager.Items.Remove(existing);
            }

            LoadCart();
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (!CartManager.Items.Any())
                return;

            using (var db = new Store123Entities())
            {
                Orders order = new Orders();

                order.CustomerId = 1;
                order.OrderDate = DateTime.Now;
                order.TotalAmount = 0;
                order.Status = "Оплачен";

                db.Orders.Add(order);

                db.SaveChanges();

                foreach (var item in CartManager.Items)
                {
                    Products product =
                        db.Products.First(p => p.Id == item.ProductId);

                    OrderItems orderItem = new OrderItems();

                    orderItem.OrderId = order.Id;
                    orderItem.ProductId = product.Id;
                    orderItem.Quantity = item.Quantity;
                    orderItem.Price = product.Price;

                    db.OrderItems.Add(orderItem);
                }

                db.SaveChanges();
            }

            CartManager.Items.Clear();

            MessageBox.Show("Заказ успешно оформлен");

            Close();
        }
    }
}