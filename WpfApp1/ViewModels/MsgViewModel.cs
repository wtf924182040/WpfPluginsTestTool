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
using System.Windows.Documents;
using Newtonsoft.Json.Linq;
using RichTextBox = System.Windows.Controls.RichTextBox;
using System.Windows.Media;

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
        public void rec(object recipient, LogMessage msg)
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
            Mg = msg;

        }
        [ObservableProperty]
        private string checkstatus = "自动滚动:开";

        [ObservableProperty]
        private string messages = "";
        [ObservableProperty]
        private LogMessage mg = new LogMessage();
        private bool isLogsChangedPropertyInViewModel = true;
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


        [ObservableProperty]
        private FlowDocument doc = new FlowDocument();
        [RelayCommand]
        private void OpenAuto()
        {
            IsLogsChangedPropertyInViewModel = true;
        }
        [RelayCommand]
        private void CloseAuto()
        {
            IsLogsChangedPropertyInViewModel = false;
        }
        [RelayCommand]
        private void Copy()
        {
            // System.Windows.Clipboard.SetText(Messages);

        }
        [RelayCommand]
        private void Paste()
        {
            Messages = System.Windows.Clipboard.GetText();
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




        public static LogMessage GetMyProperty(DependencyObject obj)
        {
            return (LogMessage)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, LogMessage value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached("MyProperty", typeof(LogMessage), typeof(Helper), new PropertyMetadata(null, MyPropertyChanged));

        private static void MyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LogMessage log = (LogMessage)e.NewValue;
            if (log.MsgContent!=""&&log.MsgContent.Length>1)
            {
                RichTextBox rtb = (RichTextBox)d;
               Paragraph paragraph = new Paragraph( FilMsg(log));
                rtb.Document.Blocks.Add(paragraph);
            }

        }

        public static Run FilMsg(LogMessage type) => type.MsgType switch
        {
            LogMessage.LogType.错误 =>new Run($"【错误】:"+ DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms ") + "\t" + type.MsgContent) { Foreground =new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) },
            LogMessage.LogType.警告 => new Run($"【警告】:"+ DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms ") + "\t" + type.MsgContent) { Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) },
            LogMessage.LogType.消息 => new Run($"【消息】:"+ DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms ") + "\t" + type.MsgContent) { Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0)) },
            _ => throw new Exception(),
        };

    }
}
