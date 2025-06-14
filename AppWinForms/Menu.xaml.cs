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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void MixControl_Click(object sender, RoutedEventArgs e)
        {
            MixControl mixControl = new CCD.AppWinForms.MixControl();
            mixControl.Show();
        }

        private void BlendData_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.BlendData blendData = new CCD.AppWinForms.BlendData();
            blendData.Show();
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SetMixMod_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.SetMixMode setMixMode = new SetMixMode();
            setMixMode.Show();
        }

        private void SetExtraPres_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.SetExtraPressures setExtraPressures = new SetExtraPressures();  
            setExtraPressures.Show();
        }

        private void Lockout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CoolTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Totals_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.Totals totals = new CCD.AppWinForms.Totals();
            totals.Show();
        }

        private void ManualControl_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.ManualControl manualControl = new CCD.AppWinForms.ManualControl();
            manualControl.Show();
        }

        private void ExtraDisplay_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.ExtraDisplay extraDisplay = new CCD.AppWinForms.ExtraDisplay();
            extraDisplay.Show();
        }

        private void SetPumpPres_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.PassSide passSide = new CCD.AppWinForms.PassSide();
            passSide.Show();
        }

        private void SystemCalibration_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.SystemCalibration systemCalibration = new CCD.AppWinForms.SystemCalibration();
            systemCalibration.Show();
        }

        private void SystemInformation_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.SystemInformation systemInformation = new CCD.AppWinForms.SystemInformation();
            systemInformation.Show();
        }

        private void SetDensities_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.RecircDensity recircDensity = new CCD.AppWinForms.RecircDensity();
            recircDensity.Show();
        }
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
