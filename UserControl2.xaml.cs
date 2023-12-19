using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    public partial class AnotherCustomMessageBox : UserControl
    {
        public AnotherCustomMessageBox()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(AnotherCustomMessageBox), new PropertyMetadata(string.Empty));

        public string Message1
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
