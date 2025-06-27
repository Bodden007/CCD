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
    /// Логика взаимодействия для PassSide.xaml
    /// </summary>
    public partial class PassSide : Window
    {
        private readonly PassSideViewModel _viewModel;
        public PassSide()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += MainWindow_KeyDown;

            _viewModel = new PassSideViewModel();

            DataContext = _viewModel;

            Loaded += PassSideWindow_Loaded;
            Closed += PassSideWindow_Closed;
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    var pskoTextBox = GridPskoText.Children.OfType<TextBox>().FirstOrDefault();
                    if (pskoTextBox != null)
                    {
                        pskoTextBox.Focus();
                        pskoTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F2:
                    _viewModel.SetPSKickoutCommand.Execute(null);
                    e.Handled = true;
                    break;

                case Key.F5:
                    var dskoTextBox = GridDskoText.Children.OfType<TextBox>().FirstOrDefault();
                    if (dskoTextBox != null)
                    {
                        dskoTextBox.Focus();
                        dskoTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F6:
                    _viewModel.SetDSKickoutCommand.Execute(null);
                    e.Handled = true;
                    break;
            }
        }
        private async void PassSideWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }

        private void PassSideWindow_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
        }
    }
}
