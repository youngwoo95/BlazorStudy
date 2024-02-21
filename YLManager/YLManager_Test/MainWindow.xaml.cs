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
using YLManager.Logger;

namespace YLManager_Test
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

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            YLManager.Logger.LogControl.CreateScheduler("C:\\Users\\user\\Documents\\BlazorStudy\\YLManager\\YLManager_Test\\bin\\Debug\\net8.0-windows\\YLManager_Test.exe", "스케줄테스트", "테스트입니다");
        }
    }
}