using CCD.Models;
using CCD.Models.ModelsForms;
using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CCD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            //FIXME верхний бар
            WindowStyle = WindowStyle.None;

            //FIXMI В максимальный размер
            WindowState = WindowState.Maximized;


            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;

            this.KeyDown += MainWindow_KeyDown;

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _viewModel.StopPolling();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                // Создаем и показываем новое окно
                CCD.AppWinForms.PassSide passSide = new CCD.AppWinForms.PassSide();
                passSide.Show();

                // Если нужно открыть модально (как диалог):
                // newWindow.ShowDialog();
            }
        }


        //FIXMI ВАЖНО!!! не забудь расскоментировать

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await _viewModel.StartPollingAsync();
        }

        //private void Window_Closed(object sender, System.EventArgs e)
        //{
        //    _viewModel.StopPolling();
        //}

        private void control_EventFromLoop(object sender, int e)
        {
            //textBlock_0.Text = e.ToString();
        }

        //FIXMI uncomment
        private void Timer_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("HH:mm");
            //StepChange();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.Menu menu = new CCD.AppWinForms.Menu();
            menu.Show();
        }

        private void PS_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.PassSide passSide = new CCD.AppWinForms.PassSide();
            passSide.Show();
        }

        private void DS_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.PassSide passSide = new CCD.AppWinForms.PassSide();
            passSide.Show();
        }

        private void CombiRate_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.Combined combined = new CCD.AppWinForms.Combined();
            //combined.Show();
        }

        private void CombiTotal_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.Combined combined = new CCD.AppWinForms.Combined();
            //combined.Show();
        }

        private void RecircDensity_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.RecircDensity recircDensity = new AppWinForms.RecircDensity();
            //recircDensity.Show();
        }

        private void DownholeDensity_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.RecircDensity recircDensity = new AppWinForms.RecircDensity();
            //recircDensity.Show();
        }

        private void MixControl_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.MixControl mixControl = new CCD.AppWinForms.MixControl();
            //mixControl.Show();
        }

        private void MixWaterRate_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.MixWaterRate mixWaterRate = new AppWinForms.MixWaterRate();
            //mixWaterRate.Show();
        }

        private void Event_Click(object sender, RoutedEventArgs e)
        {
            //CCD.AppWinForms.Event @event = new CCD.AppWinForms.Event();
            //@event.Owner = this;
            //@event.Show();
        }

        private void ShowData(int data)
        {
            //textBlock_0.Text = data.ToString();
            
        }

    }
}
