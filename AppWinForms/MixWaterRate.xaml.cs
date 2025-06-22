using CCD.Models.ModelsForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для MixWaterRate.xaml
    /// </summary>
    public partial class MixWaterRate : Window
    {
        private readonly MixWaterRateViewModel _viewModel;
        public MixWaterRate()
        {
            InitializeComponent();

            _viewModel = new MixWaterRateViewModel();

            Loaded += MixWaterRate_Loaded;
            Closed += MixWaterRate_Closed;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            DataContext = _viewModel;

            this.KeyDown += MixWaterRate_KeyDown;
        }

        private void MixWaterRate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F2:
                    _viewModel.ZeroJobTotalsCommand.Execute(null);
                    break;

                case Key.F3:
                    _viewModel.ZeroStageTotalsCommand.Execute(null);
                    break;
            }
        }

        private async void MixWaterRate_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }
        private void MixWaterRate_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
        }

        private void SetTotals_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private void ZeroJob_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
        //private void ZeroStage_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
    }
}
