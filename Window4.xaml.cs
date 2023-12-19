using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;

namespace WpfApp1
{
    public partial class Window4 : Window
    {
        private ObservableCollection<Model2> dataCollection;
        private string xmlFilePath = "C:\\Users\\Анастасия\\Desktop\\3.xml";

        public Window4()
        {
            InitializeComponent();
            dataCollection = new ObservableCollection<Model2>();
            dataGrid.ItemsSource = dataCollection;
            LoadDataFromXml();
        }

        private void addButton(object sender, RoutedEventArgs e)
        {
            string text1 = comboBox1.Text;
            string text2 = comboBox2.Text;
            string text3 = textBox1.Text;
            string text4 = textBox2.Text;

            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2) ||
                string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text4))
            {
                UserControl6 customMessageBox = new UserControl6();
                customMessageBox.Message6 = "Ваше сообщение";

                mainGrid.Children.Add(customMessageBox);

                customMessageBox.Visibility = Visibility.Visible;
                return;
            }

            if ((!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2) &&
                 !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4)))
            {
                Model2 newData = new Model2
                {
                    Property1 = text1,
                    Property2 = text2,
                    Property3 = text3,
                    Property4 = text4
                };

                dataCollection.Add(newData);

                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }

            textBox5.Clear();
            SearchTextBox_TextChanged(sender, null);
            SaveDataToXml();
        }

        private void SaveDataToXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model2>));
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, dataCollection);
                }

                UserControl7 customMessageBox = new UserControl7();
                customMessageBox.Message7 = "Ваше сообщение";

                mainGrid.Children.Add(customMessageBox);
                customMessageBox.Visibility = Visibility.Visible;
            }
            catch
            {
                UserControl8 customMessageBox = new UserControl8();
                customMessageBox.Message8 = "Ваше сообщение";

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
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model2>));
                    using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                    {
                        dataCollection = (ObservableCollection<Model2>)serializer.Deserialize(stream);
                    }

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

                mainGrid.Children.Add(customMessageBox);
                customMessageBox.Visibility = Visibility.Visible;
            }
        }

        private ObservableCollection<string> LoadComboBoxData(string xmlFilePath, string propertyToLoad, bool isModel1 = false, string filter = "")
        {
            ObservableCollection<string> comboBoxData = new ObservableCollection<string>();

            try
            {
                if (File.Exists(xmlFilePath))
                {
                    XmlSerializer serializer = isModel1
                        ? new XmlSerializer(typeof(ArrayOfModel1))
                        : new XmlSerializer(typeof(ArrayOfModel));

                    using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                    {
                        try
                        {
                            if (isModel1)
                            {
                                var array = (ArrayOfModel1)serializer.Deserialize(stream);

                                // Очистите ComboBox перед добавлением новых данных
                                comboBoxData.Clear();

                                // Добавление данных из XML в ComboBox
                                foreach (var item in array.Models1)
                                {
                                    // Учтите фильтр перед добавлением данных в ComboBox
                                    var propertyValue = item.GetType().GetProperty(propertyToLoad)?.GetValue(item, null);
                                    if (propertyValue != null && propertyValue.ToString().ToLower().Contains(filter))
                                    {
                                        comboBoxData.Add(propertyValue.ToString());
                                    }
                                }
                            }
                            else
                            {
                                var array = (ArrayOfModel)serializer.Deserialize(stream);

                                // Очистите ComboBox перед добавлением новых данных
                                comboBoxData.Clear();

                                // Добавление данных из XML в ComboBox
                                foreach (var item in array.Models)
                                {
                                    // Учтите фильтр перед добавлением данных в ComboBox
                                    var propertyValue = item.GetType().GetProperty(propertyToLoad)?.GetValue(item, null);
                                    if (propertyValue != null && propertyValue.ToString().ToLower().Contains(filter))
                                    {
                                        comboBoxData.Add(propertyValue.ToString());
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при десериализации: " + ex.Message);
                            throw; // Передача исключения выше для обработки
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Файл не существует: " + xmlFilePath);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если не удается загрузить данные из XML
                Console.WriteLine("Ошибка при загрузке данных из XML: " + ex.Message);
            }

            return comboBoxData;
        }


        private void Window4_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных для первого ComboBox
            var dataForComboBox1 = LoadComboBoxData("C:\\Users\\Анастасия\\Desktop\\1.xml", "Property1");
            comboBox1.ItemsSource = dataForComboBox1;

            if (dataForComboBox1.Count == 0)
            {
                Console.WriteLine("Данные для первого ComboBox не загружены.");
            }

            // Загрузка данных для второго ComboBox
            var dataForComboBox2 = LoadComboBoxData("C:\\Users\\Анастасия\\Desktop\\2.xml", "Property1", true);
            comboBox2.ItemsSource = dataForComboBox2;

            if (dataForComboBox2.Count == 0)
            {
                Console.WriteLine("Данные для второго ComboBox не загружены.");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterValue = textBox5.Text.Trim().ToLower();

            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);

            if (view != null)
            {
                view.Filter = item =>
                {
                    Model2 model = item as Model2;
                    return model.Property1.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                           
                };
            }
        }



        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ваш код обработки события
            // Этот метод нужно добавить, так как в XAML у вас есть обработчик "SelectionChanged"
            // для элемента управления DataGrid: SelectionChanged="dataGrid_SelectionChanged"
        }
    }
}
