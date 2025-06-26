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
    /// Логика взаимодействия для MixWaterFlowmeter.xaml
    /// </summary>
    public partial class MixWaterFlowmeter : Window
    {
        private readonly MixWaterFlowmeterViewModel _viewModel;
        public MixWaterFlowmeter()
        {
            InitializeComponent();

            _viewModel = new MixWaterFlowmeterViewModel();

            Loaded += MixWaterFlowmeter_Loaded;
            Closed += MixWaterFlowmeter_Closed;

            DataContext = _viewModel;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += MixWaterFlowmeter_KeyDown;
        }
        private void MixWaterFlowmeter_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    var minFreqTextBox = GridFlwFreqmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minFreqTextBox != null)
                    {
                        minFreqTextBox.Focus();
                        minFreqTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F2:
                    // Получаем TextBox внутри Grid
                    var minflowTextBox = GridFlwFlowmin.Children.OfType<TextBox>().FirstOrDefault();
                    if (minflowTextBox != null)
                    {
                        minflowTextBox.Focus();
                        minflowTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F3:
                    // Получаем TextBox внутри Grid
                    var maxFreqTextBox = GridFlwFreqmax.Children.OfType<TextBox>().FirstOrDefault();
                    if (maxFreqTextBox != null)
                    {
                        maxFreqTextBox.Focus();
                        maxFreqTextBox.SelectAll();
                    }
                    e.Handled = true;
                    break;

                case Key.F4:
                    // Получаем TextBox внутри Grid
                    var maxflowTextBox = GridFlwFlowmax.Children.OfType<TextBox>().FirstOrDefault();
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
        private async void MixWaterFlowmeter_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }
        private void MixWaterFlowmeter_Closed(object sender, EventArgs e)
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
