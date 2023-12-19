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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page1());
     
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            string enteredLogin = LoginTextBox.Text;
            string enteredPassword = PasswordBox.Password;

            if (IsUserCredentialsValid(enteredLogin, enteredPassword))
            {
                NavigationService?.Navigate(new Page3());
            }
            else
            {
                CustomMessageBox4 customMessageBox = new CustomMessageBox4();
                customMessageBox.Message4 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
            }
            
        }

        private bool IsUserCredentialsValid(string enteredLogin, string enteredPassword)
        {
            // Загрузка XML-файла
            XDocument xmlDoc = XDocument.Load("C:\\Users\\Анастасия\\Desktop\\Users.xml");

            // Проверка введенных логина и пароля
            return xmlDoc.Descendants("User").Any(user =>
                (string)user.Element("Login") == enteredLogin &&
                (string)user.Element("Password") == enteredPassword);
        }
    }
}


