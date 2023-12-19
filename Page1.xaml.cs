using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            if (!File.Exists("C:\\Users\\Анастасия\\Desktop\\Users.xml"))
            {
                XDocument xmlDoc = new XDocument(new XElement("Base", new XElement("Users")));
                xmlDoc.Save("C:\\Users\\Анастасия\\Desktop\\Users.xml");
            }
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page2());
        }

        public class User
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = RepeatPasswordBox.Password;

            // Проверка наличия данных в полях
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                UserControl6 customMessageBox = new UserControl6();
                customMessageBox.Message6 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
                return; // Прерываем выполнение метода, так как не все поля заполнены
            }

            if (IsLoginUnique(login))
            {
                if (password == confirmPassword)
                {
                    SaveUserToXml(login, password);

                    CustomMessageBox customMessageBox = new CustomMessageBox();
                    customMessageBox.Message = "Ваше сообщение";

                    // Добавляем кастомный MessageBox в Grid
                    mainGrid.Children.Add(customMessageBox);

                    customMessageBox.Visibility = Visibility.Visible;
                }
                else
                {
                    AnotherCustomMessageBox customMessageBox = new AnotherCustomMessageBox();
                    customMessageBox.Message1 = "Ваше сообщение";

                    // Добавляем кастомный MessageBox в Grid
                    mainGrid.Children.Add(customMessageBox);

                    customMessageBox.Visibility = Visibility.Visible;
                }
            }
            else
            {
                CustomMessageBox2 customMessageBox = new CustomMessageBox2();
                customMessageBox.Message3 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
            }
        }
        private bool IsLoginUnique(string login)
        {
            // Загрузка XML-файла
            XDocument xmlDoc = XDocument.Load("C:\\Users\\Анастасия\\Desktop\\Users.xml");

            // Проверка уникальности логина
            bool isUnique = xmlDoc.Descendants("User").Any(user => (string)user.Element("Login") == login);

            return !isUnique;
        }



        private void SaveUserToXml(string login, string password)
        {
            try
            {
                // Загружаем существующий XML-файл
                XDocument xmlDoc = XDocument.Load("C:\\Users\\Анастасия\\Desktop\\Users.xml");

                // Создаем нового пользователя с уникальным id (максимальный id + 1)
                int newUserId = xmlDoc.Descendants("User").Attributes("id").Select(attr => (int)attr).DefaultIfEmpty(0).Max() + 1;

                // Создаем новый элемент <User> с атрибутом id
                XElement newUser = new XElement("User",
                    new XAttribute("id", newUserId),
                    new XElement("Login", login),
                    new XElement("Password", password)
                );

                // Добавляем нового пользователя в корневой элемент <Users>
                xmlDoc.Element("Base").Element("Users").Add(newUser);

                // Сохраняем изменения в XML-файле
                xmlDoc.Save("C:\\Users\\Анастасия\\Desktop\\Users.xml");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении в XML: {ex.Message}");
            }
        }

       
    }
}