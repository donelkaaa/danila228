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
using System.Windows.Shapes;

namespace bratva
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void registerbutton_Click(object sender, RoutedEventArgs e)
        {
            if(logintext.Text == "" || passwordtext.Text == "" || mailtext.Text == "")
            {
                MessageBox.Show("Некоторые поля пусты!");
                return;
            }
            var context = new AppDbContext();
            context.Users.Add(new User() { Login = logintext.Text, Password = passwordtext.Text, Mail = mailtext.Text });
            context.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрировались!");
        }

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
