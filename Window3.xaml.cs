using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private ObservableCollection<Model1> dataCollection;
        private string xmlFilePath = "C:\\Users\\Анастасия\\Desktop\\2.xml";
        public Window3()
        {
            InitializeComponent();
            dataCollection = new ObservableCollection<Model1>();
            dataGrid.ItemsSource = dataCollection;
            LoadDataFromXml();
        }
        private void addButton(object sender, RoutedEventArgs e)
        {
            string text1 = textBox1.Text;
            string text2 = textBox2.Text;
            string text3 = textBox3.Text;
            string text4 = textBox4.Text;
            string text5 = textBox5.Text;

            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2) ||
       string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text4) || string.IsNullOrEmpty(text5))

            {
                UserControl6 customMessageBox = new UserControl6();
                customMessageBox.Message6 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
                return; // Прерываем выполнение метода, так как не все поля заполнены
            }

            bool isDuplicate = dataCollection.Any(item => item.Property1 == text1);

            if (isDuplicate)
            {
                UserControl5 customMessageBox = new UserControl5();
                customMessageBox.Message5 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
                return; // Прерываем выполнение метода, чтобы не добавлять дубликат
            }

            if ((!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2) &&
                    !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4) && !string.IsNullOrEmpty(text5)))
            {
                Model1 newData = new Model1
                {
                    Id = text1, // Используйте text1 в качестве Id
                    Property1 = text1,
                    Property2 = text2,
                    Property3 = text3,
                    Property4 = text4,
                    Property5 = text5

                };

                dataCollection.Add(newData);


                textBox1.Clear();
                textBox2.Clear(); 
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

            }
            else
            {

            }
            textBox6.Clear();
            SearchTextBox_TextChanged(sender, null);
            SaveDataToXml();
        }
        private void SaveDataToXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model1>));
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, dataCollection);
                }
                UserControl7 customMessageBox = new UserControl7();
                customMessageBox.Message7 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;

            }
            catch
            {
                UserControl8 customMessageBox = new UserControl8();
                customMessageBox.Message8 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
            }
        }
        private void LoadDataFromXml()
        {
            try
            {
                if (File.Exists(xmlFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model1>));
                    using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                    {
                        dataCollection = (ObservableCollection<Model1>)serializer.Deserialize(stream);
                    }

                    // Присвойте Id на основе индекса элемента, если необходимо
                    for (int i = 0; i < dataCollection.Count; i++)
                    {
                        dataCollection[i].Id = i.ToString();
                    }

                    dataGrid.ItemsSource = dataCollection;
                }
            }
            catch
            {
                UserControl8 customMessageBox = new UserControl8();
                customMessageBox.Message8 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid.CommitEdit(DataGridEditingUnit.Cell, true);
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                SaveDataToXml();
            }
            catch
            {
                UserControl8 customMessageBox = new UserControl8();
                customMessageBox.Message8 = "Ваше сообщение";

                // Добавляем кастомный MessageBox в Grid
                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ваш код обработки события
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterValue = textBox6.Text.Trim().ToLower();

            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);

            if (view != null)
            {
                view.Filter = item =>
                {
                    Model1 model = item as Model1;
                    return model.Property1.ToLower().Contains(filterValue);
                };
            }
        }
        private void Window3_Loaded(object sender, RoutedEventArgs e)
        {
           
            TextBox textBox6 = FindName("textBox6") as TextBox;
            if (textBox6 != null)
                textBox6.TextChanged += SearchTextBox_TextChanged;

        }
    }
}
