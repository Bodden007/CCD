﻿using System;
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
    /// Логика взаимодействия для SetExtraPressures.xaml
    /// </summary>
    public partial class SetExtraPressures : Window
    {
        public SetExtraPressures()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void SetInput1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetInput2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
