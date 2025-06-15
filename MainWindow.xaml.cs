using CCD.AppWinForms;
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

        private bool _isTextChangedCombiRate = false; // Флаг для отслеживания состояния текста
        private bool _isTextChangedCombiRateTotal = false; // Флаг для отслеживания состояния текста

        public MainWindow()
        {
            InitializeComponent();

            //FIXME верхний бар
            //WindowStyle = WindowStyle.None;

            //FIXMI В максимальный размер
            //WindowState = WindowState.Maximized;


            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;

            // Делаем кнопку фокусируемой
            CombiRate.Focusable = true;

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

            // Если нажаты Ctrl + 2
            if (e.Key == Key.D2 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CombinedHT400RateHandler();

                _isTextChangedCombiRate = !_isTextChangedCombiRate; // Инвертируем флаг

                e.Handled = true; // Отменяем дальнейшую обработку
            }

            // Если нажаты Ctrl + 3
            if (e.Key == Key.D3 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CombinedHT400RateTotalHandler();

                _isTextChangedCombiRateTotal = !_isTextChangedCombiRateTotal; // Инвертируем флаг

                e.Handled = true; // Отменяем дальнейшую обработку
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

        private void CombinedHT400RateHandler()
        {
            if (CombinedRateValue.Visibility == Visibility.Visible)
            {
                CombinedRateValue.Visibility = Visibility.Collapsed;
                PSRateValue.Visibility = Visibility.Visible;
                RowR_1.Height = new GridLength(2, GridUnitType.Star);
                RowR_2.Height = new GridLength(5, GridUnitType.Star);
                RowR_3.Height = new GridLength(0.5, GridUnitType.Star);
                RowR_4.Height = new GridLength(2, GridUnitType.Star);
                RowR_5.Height = new GridLength(5, GridUnitType.Star);
            }
            else
            {
                CombinedRateValue.Visibility = Visibility.Visible;
                PSRateValue.Visibility = Visibility.Collapsed;
                RowR_1.Height = new GridLength(2, GridUnitType.Star);
                RowR_2.Height = new GridLength(1, GridUnitType.Star);
                RowR_3.Height = new GridLength(5, GridUnitType.Star);
                RowR_4.Height = new GridLength(1, GridUnitType.Star);
                RowR_5.Height = new GridLength(2, GridUnitType.Star);
            }

            // Переключаем видимость TextBlock4 и TextBlock5
            DSRate.Visibility = DSRateValue.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            DSRateValue.Visibility = DSRateValue.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            // Меняем текст в TextBlock1
            //PSRate_CombinedRate.Text = _isTextChanged
            //    ? "Combined HT400 Rate"
            //    : "DS HT400 Rate";

            if (_isTextChangedCombiRate)
            {
                PSRate_CombinedRate.Text = "Combined HT400 Rate";
                PSRate_CombinedRate.FontSize = 16;
            }
            else
            {
                PSRate_CombinedRate.Text = "PS HT400 Rate";
                PSRate_CombinedRate.FontSize = 12;                
            }
        }

        private void CombinedHT400RateTotalHandler()
        {
            if (CombinedRateTotalValue.Visibility == Visibility.Visible)
            {
                CombinedRateTotalValue.Visibility = Visibility.Collapsed;
                PSRateTotalValue.Visibility = Visibility.Visible;
                RowS_1.Height = new GridLength(2, GridUnitType.Star);
                RowS_2.Height = new GridLength(5, GridUnitType.Star);
                RowS_3.Height = new GridLength(0.5, GridUnitType.Star);
                RowS_4.Height = new GridLength(2, GridUnitType.Star);
                RowS_5.Height = new GridLength(5, GridUnitType.Star);
            }
            else
            {
                CombinedRateTotalValue.Visibility = Visibility.Visible;
                PSRateTotalValue.Visibility = Visibility.Collapsed;
                RowS_1.Height = new GridLength(2, GridUnitType.Star);
                RowS_2.Height = new GridLength(1, GridUnitType.Star);
                RowS_3.Height = new GridLength(5, GridUnitType.Star);
                RowS_4.Height = new GridLength(1, GridUnitType.Star);
                RowS_5.Height = new GridLength(2, GridUnitType.Star);
            }

            // Переключаем видимость TextBlock4 и TextBlock5
            DSRateTotal.Visibility = DSRateTotalValue.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            DSRateTotalValue.Visibility = DSRateTotalValue.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            // Меняем текст в TextBlock1
            //PSRate_CombinedRate.Text = _isTextChanged
            //    ? "Combined HT400 Rate"
            //    : "DS HT400 Rate";

            if (_isTextChangedCombiRateTotal)
            {
                PSRateTotal_CombinedRate.Text = "Combined Stage Total";
                PSRateTotal_CombinedRate.FontSize = 16;
            }
            else
            {
                PSRateTotal_CombinedRate.Text = "PS Stage Total";
                PSRateTotal_CombinedRate.FontSize = 12;
            }
        }
        

    }
}
