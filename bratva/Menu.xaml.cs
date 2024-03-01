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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace bratva
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public User user {  get; set; }
        public AppDbContext context { get; set; }
        public List<ListViewItemModel> items { get; set; } = new List<ListViewItemModel>();
        public Menu(User u)
        {
            InitializeComponent();
            context = new AppDbContext();
            user = context.Users.Include(x => x.Carts).Where(x => x.Id == u.Id).FirstOrDefault();
            List<Item> i = context.Items.ToList();
            foreach(Item item in i)
            {
                items.Add(new ListViewItemModel() { Id = item.Id, Name = item.Name, Cost = item.Cost, Path = Path.Combine(Environment.CurrentDirectory, "pics", item.Image) });
            }
            itemlist.ItemsSource = items;
        }

        private void logoff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as ListViewItemModel;
            if (item != null)
            {
                if (context.Carts.Where(x => x.Item.Id == item.Id && user.Carts.Contains(x)).FirstOrDefault() == null)
                {
                    MessageBox.Show("Вы добавили товар в корзину!");
                    context.Users.Include(x => x.Carts).Where(x => x.Id == user.Id).FirstOrDefault().Carts.Add(
                        new Cart() { Item = context.Items.Where(x => x.Id == item.Id).FirstOrDefault(), Amount = 1 });
                }
                context.SaveChanges();
            }

        }

        private void cart_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order(user);
            order.Show();
            this.Close();
        }
    }
}
