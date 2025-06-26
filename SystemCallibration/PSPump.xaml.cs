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

namespace CCD.SystemCallibration
{
    /// <summary>
    /// Логика взаимодействия для PSPump.xaml
    /// </summary>
    public partial class PSPump : Window
    {
        private readonly PSPumpViewModel _viewModel;
        public PSPump()
        {
            InitializeComponent();

            _viewModel = new PSPumpViewModel();

            Loaded += PSPump_Loaded;
            Closed += PSPump_Closed;

            DataContext = _viewModel;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += PSPump_KeyDown;
        }

        private void PSPump_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    var minFreqTextBox = GridPsFreqmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minFreqTextBox != null)
                    {
                        minFreqTextBox.Focus();
                        minFreqTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F2:
                    // Получаем TextBox внутри Grid
                    var minflowTextBox = GridPsFlowmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minflowTextBox != null)
                    {
                        minflowTextBox.Focus();
                        minflowTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F3:
                    // Получаем TextBox внутри Grid
                    var maxFreqTextBox = GridPsFreqmax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxFreqTextBox != null)
                    {
                        maxFreqTextBox.Focus();
                        maxFreqTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F4:
                    // Получаем TextBox внутри Grid
                    var maxflowTextBox = GridPsFlowmax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxflowTextBox != null)
                    {
                        maxflowTextBox.Focus();
                        maxflowTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F9:
                    _viewModel.SaveCommand?.Execute(null);
                    e.Handled = true;
                    break;
            }
        }

        private async void PSPump_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }
        private void PSPump_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
        }      

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveCommand?.Execute(null);
        }
    }
}
