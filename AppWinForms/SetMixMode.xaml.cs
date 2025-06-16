using CCD.Models.ModelsForms;
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
using System.Windows.Shapes;

namespace CCD.AppWinForms
{
    /// <summary>
    /// Логика взаимодействия для SetMixMode.xaml
    /// </summary>
    public partial class SetMixMode : Window
    {
        private readonly SetMixModeViewModel _viewModel;
        public SetMixMode()
        {
            InitializeComponent();

            _viewModel = new SetMixModeViewModel();

            DataContext = _viewModel;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            Loaded += PassSideWindow_Loaded;
            Closed += PassSideWindow_Closed;

            this.KeyDown += SetMixMode_KeyDown;
        }      

        private async void PassSideWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }
        private void SetMixMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        private async void PassSideWindow_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
