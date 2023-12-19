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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControl4.xaml
    /// </summary>
    public partial class CustomMessageBox4 : UserControl
    {
        public CustomMessageBox4()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(CustomMessageBox4), new PropertyMetadata(string.Empty));

        public string Message4
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
