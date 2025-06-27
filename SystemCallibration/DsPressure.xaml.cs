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
    /// Логика взаимодействия для DsPressure.xaml
    /// </summary>
    public partial class DsPressure : Window
    {
        private readonly DsPressureViewModel _viewModel;
        public DsPressure()
        {
            InitializeComponent();

            _viewModel = new DsPressureViewModel();

            Loaded += DsPressure_Loaded;
            Closed += DsPressure_Closed;      
            
            DataContext = _viewModel;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += DsPressure_KeyDown;
        }

        private void DsPressure_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    var minMillTextBox = GridDsMillmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minMillTextBox != null)
                    {
                        minMillTextBox.Focus();
                        minMillTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F2:
                    // Получаем TextBox внутри Grid
                    var minPressureTextBox = GridDsPressuremin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minPressureTextBox != null)
                    {
                        minPressureTextBox.Focus();
                        minPressureTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F3:
                    // Получаем TextBox внутри Grid
                    var maxMillTextBox = GridDsMillmax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxMillTextBox != null)
                    {
                        maxMillTextBox.Focus();
                        maxMillTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F4:
                    // Получаем TextBox внутри Grid
                    var maxPressureTextBox = GridDsPressuremax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxPressureTextBox != null)
                    {
                        maxPressureTextBox.Focus();
                        maxPressureTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F9:
                    _viewModel.SaveCommand?.Execute(null);
                    e.Handled = true;
                    break;
            }
        }

        private async void DsPressure_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }

        private void DsPressure_Closed(object sender, EventArgs e)
        {
            _viewModel.StopPolling();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveCommand.Execute(null);
        }
    }
}
