using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.VisualBasic;
using CommunityToolkit.Mvvm.Input;
using WTF.Plugins;

namespace WpfApp1.ViewModels
{
    public partial class MsgViewModel : ObservableRecipient
    {
        public MsgViewModel()
        {           
            IsActive = true;
        }
        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<LogMessage>(this, rec);
            base.OnActivated();
        }
        public void rec(object recipient,LogMessage msg)
        {
            if (Messages.Split('\n').Count() >= 100)
            {
                Messages = "";
            }
            if (Messages == "")
            {
                Messages += $"【{msg.MsgType.ToString()}】:" + "\t" + DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms ") + "\t" + msg.MsgContent;
            }
            else
            {
                Messages += "\r\n" + $"【{msg.MsgType.ToString()}】:" + "\t" + DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms ") + "\t" + msg.MsgContent;
            }
        }
        [ObservableProperty]
        private string checkstatus = "自动滚动:开";

        [ObservableProperty]
        private string messages = "";

        private bool isLogsChangedPropertyInViewModel=true;
        public bool IsLogsChangedPropertyInViewModel
        {
            get => isLogsChangedPropertyInViewModel;

            set
            {
                if (value == true)
                {
                    Checkstatus = "自动滚动:开";
                }
                else
                {
                    Checkstatus = "自动滚动:关";
                }
                SetProperty(ref isLogsChangedPropertyInViewModel, value);
            }
        }

        [RelayCommand]
        private void OpenAuto()
        {
            IsLogsChangedPropertyInViewModel=true;
        }
        [RelayCommand]
        private void CloseAuto()
        {
            IsLogsChangedPropertyInViewModel = false;
        }
        [RelayCommand]
        private void Copy()
        {
           System.Windows.Clipboard.SetText(Messages);
        }
        [RelayCommand]
        private void Paste()
        {
            Messages= System.Windows.Clipboard.GetText();
        }
    }
    public static class Helper
    {

        public static bool GetAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollProperty, value);
        }

        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(Helper), new PropertyMetadata(false, AutoScrollPropertyChanged));

        private static void AutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;

            if (scrollViewer != null && (bool)e.NewValue)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public static bool GetDisplay(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisplayProperty);
        }

        public static void SetDisplay(DependencyObject obj, bool value)
        {
            obj.SetValue(DisplayProperty, value);
        }
        public static readonly DependencyProperty DisplayProperty = DependencyProperty.RegisterAttached("Display", typeof(bool), typeof(Helper), new PropertyMetadata(false, DisplayChanged));

        private static void DisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            dynamic sat = e;
            dynamic gc = d;
            if (sat.NewValue != sat.OldValue)
            {
                if (sat.NewValue == true)
                {
                    gc.Width = 200;
                }
                else
                {
                    gc.Width = 60;
                }
            }
        }

    }
}
