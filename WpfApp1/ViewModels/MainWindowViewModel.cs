using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Validation;
using WTF.Plugins;
using static HandyControl.Tools.Interop.InteropValues;
using ButtonBase = System.Windows.Controls.Primitives.ButtonBase;
using LogMessage = WTF.Plugins.LogMessage;
namespace WpfApp1.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        /// <summary>
        /// 保存当前编辑的类
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private void Save(object obj)
        {

            dynamic bs = this.SlType;
            if (bs != null)
            {
                bs.Save(bs.PluginsName);
                //WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = res });
            }
            else
            {
                WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "没有任何更改" });

            }

        }
        [RelayCommand]
        private void Update(object obj)
        {


            dynamic bs = this.SlType;
            if (bs != null)
            {
                bs.Update(bs.PluginsName);
                Load();
                // WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = res });
            }
            else
            {
                WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "没有任何更改" });

            }


        }
        /// <summary>
        /// 删除当前选择的保存类
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private void Delete(object obj)
        {

            dynamic bs = this.SlType;
            if (bs != null)
            {
                bs.Delete(bs.PluginsName);
                Load();
                // WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = $"删除{bs.PluginsName}完成"});
            }
            else
            {
                WeakReferenceMessenger.Default.Send<LogMessage>(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "没有任何更改" });

            }

        }
        /// <summary>
        /// 加载程序集类
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private void NewlyBuilt(object obj)///新建
        {

            Typelist.Clear();
            var Filelist = from i in System.IO.Directory.GetFiles(Environment.CurrentDirectory) where Path.GetExtension(i) == ".dll" select i;
            Collection<Type> types = new Collection<Type>();
            Parallel.ForEach(Filelist, a =>
            {
                Assembly assembly = Assembly.LoadFrom(a);
                var ps = assembly.ExportedTypes.Where(a => a.BaseType == typeof(PluginsBase));

                foreach (Type item in ps)
                {
                    types.Add(item);
                }
            });

            List<Type> list1 = types.GroupBy(d => new { d.Name, d.FullName, d.Assembly }).Select(d => d.FirstOrDefault()).ToList();

            foreach (var item in list1)
            {
                Typelist.Add(Activator.CreateInstance(item));
            }
            LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = "加载程序集类完成!!!" });
        }
        /// <summary>
        /// 加载数据库保存类
        /// </summary>
        [RelayCommand]
        private void Load()///数据库加载
        {
            Typelist.Clear();
            var db = new SQLiteConnection(Path.GetFullPath("reservoom.db", Environment.CurrentDirectory));
            TableQuery<TestItem> users = db.Table<TestItem>();
            var Filelist = from i in System.IO.Directory.GetFiles(Environment.CurrentDirectory) where Path.GetExtension(i) == ".dll" select i;
            Collection<Type> types = new Collection<Type>();
            Parallel.ForEach(Filelist, a =>
            {
                Assembly assembly = Assembly.LoadFrom(a);
                var ps = assembly.ExportedTypes.Where(a => a.BaseType == typeof(PluginsBase));

                foreach (Type item in ps)
                {
                    types.Add(item);
                }
            });
            foreach (var item in users)
            {
                var tep = from mm in types where mm.FullName == item.TypeName select mm;
                Type tap = tep.ToList()[0];
                dynamic obj = Activator.CreateInstance(tap);
                var bs = tap.GetProperties().Where(a => a.GetCustomAttribute(typeof(DisplayNameAttribute)) != null).ToList();
                dynamic oobj = JsonConvert.DeserializeAnonymousType(item.Value, obj);
                foreach (PropertyInfo bbs in bs)
                {
                    bbs.SetValue(obj, bbs.GetValue(oobj));

                }
                Typelist.Add(obj);
            }
            LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = "加载数据库保存类完成!!!" });
        }

        [ObservableProperty]
        private ObservableCollection<object> typelist = new ObservableCollection<object>();

        [ObservableProperty]
        private object slType = null;

        [RelayCommand]
        private void SingleRun(object obj)///运行
        {
            if (Typelist != null && SlType != null)
            {
                Task.Run(() =>
                 {
                     if (SlType != null && ((PluginsBase)SlType).HasErrors == false)
                     {
                         ((PluginsBase)SlType).Execute();
                         //LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = res });
                     }

                     else
                     {
                         LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "请填写正确的参数后重试" });
                     }
                 });
            }
            else
            {

                Task.Run(() =>
                {
                    LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "请选择要执行的命令" });
                });
            }

        }
        public System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        [RelayCommand]
        private void CycleOperation(bool obj)///W运行
        {


            // Tick 超过计时器间隔时发生。
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            // Interval 获取或设置计时器刻度之间的时间段
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            if (obj)
            {
                dispatcherTimer.Start();
            }
            else
            {
                dispatcherTimer.Stop();
            }


        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (SlType != null && ((PluginsBase)SlType).HasErrors == false)
            {
                Task.Run(() =>
                  {
                      ((PluginsBase)SlType).Execute();
                      // LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.消息, MsgContent = res });
                  });
            }



            else
            {
                LogMessage.PostMsg(new LogMessage() { MsgType = LogMessage.LogType.错误, MsgContent = "请填写正确的参数后重试" });
            }
        }
    }
}

