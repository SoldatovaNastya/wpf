using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControl3.xaml
    /// </summary>
    public partial class CustomMessageBox2 : UserControl
    {
        public CustomMessageBox2()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(CustomMessageBox2), new PropertyMetadata(string.Empty));

        public string Message3
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Обработчик события для кнопки OK
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть UserControl, например, установив Visibility в Hidden или Collapsed
            Visibility = Visibility.Collapsed;
        }
    }
}
