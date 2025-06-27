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
    /// Логика взаимодействия для SystemCalibration.xaml
    /// </summary>
    public partial class SystemCalibration : Window
    {
        public SystemCalibration()
        {
            InitializeComponent();

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += SystemCalibration_KeyDown;
        }

        private void SystemCalibration_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.F1:
                    new CCD.SystemCallibration.PsPressure().Show();
                    break;

                case Key.F2:
                    new CCD.SystemCallibration.DsPressure().Show();
                    break;

                case Key.F3:
                    new CCD.SystemCallibration.MixWaterFlowmeter().Show();
                    break;

                case Key.F4:
                    new CCD.SystemCallibration.PSPump().Show();
                    break;

                case Key.F5:
                    new CCD.SystemCallibration.DSPump().Show();
                    break;
            }
        }

        private void ExitCalibration_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.Next next = new CCD.SystemCallibration.Next();
            next.Show();
            Close();
        }

        private void PSPressure_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.PsPressure psPressure = new SystemCallibration.PsPressure();
            psPressure.Show();
        }

        private void DSPressure_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.DsPressure dsPressure = new SystemCallibration.DsPressure();
            dsPressure.Show();
        }

        private void MixWaterFlowmeter_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.MixWaterFlowmeter mixWaterFlowmeter = new SystemCallibration.MixWaterFlowmeter();
            mixWaterFlowmeter.Show();
        }

        private void PSPumpRate_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.PSPump pSPump = new SystemCallibration.PSPump();
            pSPump.Show();
        }

        private void DSPumpRate_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.DSPump dSPump = new SystemCallibration.DSPump();
            dSPump.Show();
        }

        private void RecircDensity_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.RecircDensity recircDensity = new RecircDensity();
            recircDensity.Show();
        }

        private void DounholeDensity_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.RecircDensity recircDensity = new RecircDensity();
            recircDensity.Show();
        }

        private void CementValve_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.CementWaterValve cementWaterValve = new SystemCallibration.CementWaterValve();
            cementWaterValve.Show();
        }

        private void WaterValve_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.CementWaterValve cementWaterValve = new SystemCallibration.CementWaterValve();
            cementWaterValve.Show();
        }

        private void IOConfig_Click(object sender, RoutedEventArgs e)
        {
            CCD.IOConfig.IoConfig14 ioConfig14 = new CCD.IOConfig.IoConfig14();
            ioConfig14.Show();
        }

        private void TubLevel_Click(object sender, RoutedEventArgs e)
        {
            CCD.SystemCallibration.TubLevel tubLevel = new SystemCallibration.TubLevel();
            tubLevel.Show();
        }
    }
}
