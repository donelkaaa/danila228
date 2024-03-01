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

namespace bratva
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void registerbutton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            var context = new AppDbContext();
            User user = context.Users.Where(x => x.Login == logintext.Text && x.Password == passwordtext.Text).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Неверные данные!");
                return;
            }
            Menu menu = new Menu(user);
            menu.Show();
            this.Close();
        }
    }
}