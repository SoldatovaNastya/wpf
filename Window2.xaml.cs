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
        public partial class Window2 : Window
        {
            private ObservableCollection<Model> dataCollection;
            private string xmlFilePath = "C:\\Users\\Анастасия\\Desktop\\1.xml";
            private ComboBox comboBoxFilterColumn3;
            private ComboBox comboBoxFilterColumn4;
            private TextBox searchTextBox;

            public Window2()
            {
                InitializeComponent();
                dataCollection = new ObservableCollection<Model>();
                dataGrid.ItemsSource = dataCollection;
                LoadDataFromXml();
            }

            private void Window2_Loaded(object sender, RoutedEventArgs e)
            {
                comboBoxFilterColumn3 = FindName("comboBoxFilterColumn3") as ComboBox;
                comboBoxFilterColumn4 = FindName("comboBoxFilterColumn4") as ComboBox;
                if (comboBoxFilterColumn3 != null)
                {
                    comboBoxFilterColumn3.SelectedIndex = 0; // "Все"
                }

                if (comboBoxFilterColumn4 != null)
                {
                    comboBoxFilterColumn4.SelectedIndex = 0; // "Все"
                }

                searchTextBox = FindName("searchTextBox") as TextBox;
                if (searchTextBox != null)
                    searchTextBox.TextChanged += SearchTextBox_TextChanged;
            }

            private void addButton(object sender, RoutedEventArgs e)
            {
                string combo1 = comboBox1.Text;
                string combo2 = comboBox2.Text;
                string text1 = textBox1.Text;
                string text2 = textBox2.Text;
               

            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2) ||
       string.IsNullOrEmpty(combo1) || string.IsNullOrEmpty(combo2))

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
                    !string.IsNullOrEmpty(combo1) && !string.IsNullOrEmpty(combo2)))
                {
                    Model newData = new Model
                    {
                        Id = text1, // Используйте text1 в качестве Id
                        Property1 = text1,
                        Property2 = text2,
                        Property3 = combo1,
                        Property4 = combo2
                    };

                    dataCollection.Add(newData);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                }
                else
                {
              
                }
                SaveDataToXml();
            }


            private void SaveDataToXml()
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model>));
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
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Model>));
                        using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                        {
                            dataCollection = (ObservableCollection<Model>)serializer.Deserialize(stream);
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


            private void ComboBoxFilterColumn3_Loaded(object sender, RoutedEventArgs e)
            {
                comboBoxFilterColumn3 = sender as ComboBox;
            }

            private void ComboBoxFilterColumn4_Loaded(object sender, RoutedEventArgs e)
            {
                comboBoxFilterColumn4 = sender as ComboBox;
            }

            private void ComboBoxFilterColumn3_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (comboBoxFilterColumn3 != null)
                {
                    ApplyFilter();
                }
            }

            private void ComboBoxFilterColumn4_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (comboBoxFilterColumn4 != null)
                {
                    ApplyFilter();
                }
            }

            private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                ApplyFilter();
            }

        private void ApplyFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);

            if (view != null)
            {
                view.Filter = item =>
                {
                    Model model = item as Model;

                    string filterValueId = textBox5?.Text.ToLower();
                    bool filterId = string.IsNullOrEmpty(filterValueId) || model.Property1.ToLower().StartsWith(filterValueId);

                    string filterValueProperty3 = (comboBoxFilterColumn3?.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string filterValueProperty4 = (comboBoxFilterColumn4?.SelectedItem as ComboBoxItem)?.Content.ToString();

                    bool filterProperty3 = string.IsNullOrEmpty(filterValueProperty3) || model.Property3 == filterValueProperty3 || filterValueProperty3 == "Все";
                    bool filterProperty4 = string.IsNullOrEmpty(filterValueProperty4) || model.Property4 == filterValueProperty4 || filterValueProperty4 == "Все";

                    return filterId && filterProperty3 && filterProperty4;
                };
            }
        }



    }
}
