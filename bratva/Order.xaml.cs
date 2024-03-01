using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace bratva
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        public User user { get; set; }
        public AppDbContext context { get; set; }
        public List<ListViewCartModel> items { get; set; } = new List<ListViewCartModel>();
        public int cart_id { get; set; }
        public Order(User u)
        {
            InitializeComponent();
            context = new AppDbContext();
            user = context.Users.Include(x => x.Carts).Where(x => x.Id == u.Id).FirstOrDefault();
            List<Cart> i = context.Carts.Include(x => x.Item).Where(x => user.Carts.Contains(x)).ToList();
            foreach (Cart c in i)
            {
                items.Add(new ListViewCartModel() { Id = c.Id, Item = c.Item, Amount = c.Amount, Path = Path.Combine(Environment.CurrentDirectory, "pics", c.Item.Image) } );
            }
            itemlist.ItemsSource = items;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as ListViewCartModel;
            if (item != null)
            {
                cart_id = item.Id;
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if(cart_id != null && cart_id != 0)
            {
                context.Carts.Where(x => x.Id == cart_id).FirstOrDefault().Amount--;
                if (context.Carts.Where(x => x.Id == cart_id).FirstOrDefault().Amount == 0)
                    context.Carts.Remove(context.Carts.Where(x => x.Id == cart_id).FirstOrDefault());
                context.SaveChanges();
                items.Clear();
                itemlist.ItemsSource = new List<ListViewCartModel>();
                List<Cart> i = context.Carts.Include(x => x.Item).Where(x => user.Carts.Contains(x)).ToList();
                foreach (Cart c in i)
                {
                    items.Add(new ListViewCartModel() { Id = c.Id, Item = c.Item, Amount = c.Amount, Path = Path.Combine(Environment.CurrentDirectory, "pics", c.Item.Image) });
                }
                itemlist.ItemsSource = items;
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (cart_id != null && cart_id != 0)
            {
                context.Carts.Where(x => x.Id == cart_id).FirstOrDefault().Amount++;
                context.SaveChanges();
                items.Clear();
                itemlist.ItemsSource = new List<ListViewCartModel>();
                List<Cart> i = context.Carts.Include(x => x.Item).Where(x => user.Carts.Contains(x)).ToList();
                foreach (Cart c in i)
                {
                    items.Add(new ListViewCartModel() { Id = c.Id, Item = c.Item, Amount = c.Amount, Path = Path.Combine(Environment.CurrentDirectory, "pics", c.Item.Image) });
                }
                itemlist.ItemsSource = items;
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (cart_id != null && cart_id != 0)
            {
                context.Carts.Remove(context.Carts.Where(x => x.Id == cart_id).FirstOrDefault());
                context.SaveChanges();
                items.Clear();
                itemlist.ItemsSource = new List<ListViewCartModel>();
                List<Cart> i = context.Carts.Include(x => x.Item).Where(x => user.Carts.Contains(x)).ToList();
                foreach (Cart c in i)
                {
                    items.Add(new ListViewCartModel() { Id = c.Id, Item = c.Item, Amount = c.Amount, Path = Path.Combine(Environment.CurrentDirectory, "pics", c.Item.Image) });
                }
                itemlist.ItemsSource = items;
            }
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(user);
            menu.Show();
            this.Close();
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            if(context.Carts.Where(x => user.Carts.Contains(x)).FirstOrDefault() == null)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }
            int sum = 0;
            List<Cart> i = context.Carts.Include(x => x.Item).Where(x => user.Carts.Contains(x)).ToList();
            foreach (Cart c in i)
            {
                sum += c.Amount * c.Item.Cost;
            }
            MessageBox.Show($"Заказ оформлен! Оплата составит {sum} руб.");
            items.Clear();
            if(context.Carts.Where(x => user.Carts.Contains(x)).FirstOrDefault() != null) context.Carts.Remove(context.Carts.Where(x => user.Carts.Contains(x)).FirstOrDefault());
            context.SaveChanges();
            itemlist.ItemsSource = new List<ListViewCartModel>();
        }
    }
}
