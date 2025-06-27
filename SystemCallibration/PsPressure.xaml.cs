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
    /// Логика взаимодействия для PsPressure.xaml
    /// </summary>
    public partial class PsPressure : Window
    {
        private readonly PsPressureViewModel _viewModel;
        public PsPressure()
        {
            InitializeComponent();

            _viewModel = new PsPressureViewModel();

            Loaded += PsPressure_Loaded;
            Closed += PsPressure_Closed;
            
            DataContext = _viewModel;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += PsPressure_KeyDown;
        }

        private void PsPressure_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    var minMillTextBox = GridPsMillmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minMillTextBox != null)
                    {
                        minMillTextBox.Focus();
                        minMillTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F2:
                    // Получаем TextBox внутри Grid
                    var minPressureTextBox = GridPsPressuremin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minPressureTextBox != null)
                    {
                        minPressureTextBox.Focus();
                        minPressureTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F3:
                    // Получаем TextBox внутри Grid
                    var maxMillTextBox = GridPsMillmax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxMillTextBox != null)
                    {
                        maxMillTextBox.Focus();
                        maxMillTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F4:
                    // Получаем TextBox внутри Grid
                    var maxPressureTextBox = GridPsPressuremax.Children.OfType<TextBox>().FirstOrDefault();
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

        private async void PsPressure_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }

        private void PsPressure_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
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
