﻿using HandyControl.Controls;
using HandyControl.Data;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;
using Application = System.Windows.Application;
using Color = System.Windows.Media.Color;
using ListBox = System.Windows.Controls.ListBox;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {


        }
        protected override void OnClosed(EventArgs e)
        {                
                base.OnClosed(e);
                Environment.Exit(0);   
        
        }



    }
}