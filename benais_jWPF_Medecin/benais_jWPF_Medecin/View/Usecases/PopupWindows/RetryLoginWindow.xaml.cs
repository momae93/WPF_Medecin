﻿using benais_jWPF_Medecin.ViewModel.Usecases.Popup;
using System.Windows;

namespace benais_jWPF_Medecin.View.Usecases.PopupWindows
{
    /// <summary>
    /// Logique d'interaction pour RetryLoginWindow.xaml
    /// </summary>
    public partial class RetryLoginWindow : Window
    {
        public RetryLoginWindow()
        {
            InitializeComponent();
            this.DataContext = new RetryLoginViewModel();
        }
    }
}
