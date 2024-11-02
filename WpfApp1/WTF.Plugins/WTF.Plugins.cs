using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static WTF.Plugins.Validation;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Text;
using System.Drawing;
using System;
using System.Net.Mime;
namespace WTF
{
    namespace Plugins
    {

        /// <summary>
        /// PluginsBase 运行框架的基类，必须继承此类才能加载和识别
        /// Category特性可以修饰属性并给属性修改显示分组必须要加默认为参数组
        /// DisplayName特性可以修改属性框显示名，参数属性必须要加否则不会保存该参数值
        /// Browsable特性可以设置是否在属性框显示该属性
		///PluginsName,PluginsID,Description三个默认属性再编写插件时尽量重新补充完整
        /// </summary>
        public abstract partial class PluginsBase : ObservableValidator, IPluginsBase
        {

            /// <summary>
            /// 该字段为数据库文件存储地址
            /// </summary>
            [Browsable(false)]
            private string SqlAddrContent { get; set; } = Path.GetFullPath("reservoom.db", Environment.CurrentDirectory);// = """C:\Users\92418\Desktop\reservoom.db""";
            private double _pluginsID = 1.0;
            /// <summary>
            /// 插件版本号ID,ID范围在1，10000之间"
            /// </summary>
            [Category("必填项目")]
            [Range(1, 100000000000000000, ErrorMessage = "ID范围在1，100000000000000000之间")]
            [Description("插件ID或版本号,ID范围在1，10000之间")]
            [DisplayName("插件ID")]
            [JsonProperty]
            public double PluginsID
            {
                get { return _pluginsID; }
                set { SetProperty(ref _pluginsID, value, true); Check(); }
            }

            private string _pluginsname = "";
            /// <summary>
            /// 插件测试项目名称不能小于8个字符
            /// </summary>
            [MinLength(8, ErrorMessage = "测试项目名称不能小于8个字符")]
            [Category("必填项目")]
            [Description("插件名字,测试项名字不能重复不能小于8个字符")]
            [DisplayName("插件名字")]
            [JsonProperty]
            public string PluginsName
            {
                get { return _pluginsname; }
                set { SetProperty(ref _pluginsname, value, true); Check(); }
            }
            private string _description = "";
            /// <summary>
            /// 类描述文字描述不能小于10个字符
            /// </summary>
            [Category("必填项目")]
            [Description("类描述，文字描述不能小于10个字符")]
            [MinLength(10, ErrorMessage = "文字描述不能小于10个字符")]
            [DisplayName("插件描述")]
            [JsonProperty]
            public string Description
            {
                get { return _description; }
                set { SetProperty(ref _description, value, true); Check(); }
            }

            public abstract string FriendName { get; }

            public abstract string Execute();
            public abstract void BeforeExecution();

            public abstract void AfterExecution();

            /// <summary>
            /// 保存实例化的参数
            /// </summary>
            /// <param name="str"></param>
            public string Save(string str)
            {
                if (SaveNameIsValid(str))
                {
                    TestItem dict = new TestItem();
                    ValidateAllProperties();
                    if (HasErrors == true)
                    {
                        LogMessage tep = new LogMessage("请检查参数设置后重试");
                        tep.PostError();
                        return "请检查参数设置后重试";

                    }
                    else
                    {
                        var db = new SQLiteConnection(SqlAddrContent);
                        string bytes = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                        dict.Name = str;
                        dict.TypeName = this.GetType().FullName;
                        dict.Value = bytes;
                        db.Insert(dict);
                        LogMessage tep = new LogMessage("保存成功");
                        tep.PostMsg();
                        return "保存成功";

                    }

                }
                else
                {
                    LogMessage tep = new LogMessage("插件名称已经存在了，请更新数据或者增加描述性后缀后保存");
                    tep.PostError();
                    return "插件名称已经存在了，请更新数据或者增加描述性后缀后保存";

                }


            }
            public string Update(string str)
            {
                if (!SaveNameIsValid(str))
                {
                    TestItem dict = new TestItem();
                    ValidateAllProperties();
                    if (HasErrors == true)
                    {

                        LogMessage tep = new LogMessage("请检查参数设置后重试");
                        tep.PostError();
                        return "请检查参数设置后重试";
                    }
                    else
                    {

                        var db = new SQLiteConnection(SqlAddrContent);
                        string bytes = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                        dict.Name = str;
                        dict.TypeName = this.GetType().FullName;
                        dict.Value = bytes;
                        db.Execute($"UPDATE TestItem SET Value = '{bytes}' WHERE Name = '{str}';");
                        LogMessage tep = new LogMessage("更新成功");
                        tep.PostMsg();
                        return "更新成功";

                    }

                }
                else
                {
                    LogMessage tep = new LogMessage("插件名称不存在，请使用保存按钮");
                    tep.PostError();
                    return "插件名称不存在，请使用保存按钮";

                }


            }
            public string Delete(string str)
            {
                if (!SaveNameIsValid(str))
                {
                    TestItem dict = new TestItem();
                    ValidateAllProperties();
                    if (HasErrors == true)
                    {
                        LogMessage tep = new LogMessage("请检查参数设置后重试");
                        tep.PostError();
                        return "请检查参数设置后重试";
                    }
                    else
                    {

                        var db = new SQLiteConnection(SqlAddrContent);
                        string bytes = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                        dict.Name = str;
                        dict.TypeName = this.GetType().FullName;
                        dict.Value = bytes;
                        db.Execute($"DELETE FROM TestItem WHERE Name = '{str}';");
                        LogMessage tep = new LogMessage("删除成功");
                        tep.PostMsg();
                        return "删除成功";

                    }

                }
                else
                {
                    LogMessage tep = new LogMessage("插件名称不存在，删除失败");
                    tep.PostError();
                    return "插件名称不存在，删除失败";

                }


            }
            public void Check()
            {
                this.ValidateAllProperties();

            }
            public bool SaveNameIsValid(object? value)
            {
                var db = new SQLiteConnection(SqlAddrContent);
                TableQuery<TestItem> users = db.Table<TestItem>().Where(a => a.Name == value);

                if (users.Count() >= 1)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }

        }
        public interface IPluginsBase
        {
            public double PluginsID { get; set; }
            public string PluginsName { get; set; }
            public string FriendName { get; }

            /// <summary>
            /// 执行程序必须要实现主要包含插件的功能
            /// </summary>
            /// <returns>执行结果</returns>
            public string Execute();
            /// <summary>
            /// 执行程序前运行必须要实现，可以不写内容
            /// </summary>
            public void BeforeExecution();
            /// <summary>
            /// 执行程序后必须要实现，可以不写内容
            /// </summary>
            public void AfterExecution();
        }
        public class TestItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string TypeName { get; set; }

        }
        public class LogMessage
        {
            public LogMessage()
            { }
            public LogMessage(string str)
            {
                this.MsgContent = str;
            }
            public static void PostMsg(LogMessage msg)
            {
                WeakReferenceMessenger.Default.Send<LogMessage>(msg);
            }
            public enum LogType
            {
                错误,
                警告,
                消息,
            }
            public string MsgContent { get; set; } = "";
            public LogType MsgType { get; set; } = LogType.消息;
        }
        public static class ExtendedHelper
        {
            public static void PostError(this LogMessage msg)
            {
                msg.MsgType = LogMessage.LogType.错误;
                WeakReferenceMessenger.Default.Send<LogMessage>(msg);
            }
            public static void PostMsg(this LogMessage msg)
            {
                msg.MsgType = LogMessage.LogType.消息;
                WeakReferenceMessenger.Default.Send<LogMessage>(msg);
            }
            public static void PostWring(this LogMessage msg)
            {
                msg.MsgType = LogMessage.LogType.警告;
                WeakReferenceMessenger.Default.Send<LogMessage>(msg);
            }

        }
        public class Validation
        {
            public class Email地址Attribute : ValidationAttribute
            {
                public Email地址Attribute()
                {
                    ErrorMessage = "必须是一个合法的邮箱地址";
                }
                public override bool IsValid(object? value)
                {

                    System.Text.RegularExpressions.Regex regex = new Regex("""^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$""");
                    return regex.Match(value.ToString()).Success;
                }

            }
            public class IP地址Attribute : ValidationAttribute
            {
                public IP地址Attribute()
                {
                    ErrorMessage = "必须是一个合法的IP地址";
                }
                public override bool IsValid(object? value)
                {

                    System.Text.RegularExpressions.Regex regex = new Regex("""((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}""");
                    return regex.Match(value.ToString()).Success;
                }

            }



        }
    }
    namespace Plugins.Test
    {
        [JsonObject]
        public partial class Demo : PluginsBase
        {

            [JsonProperty]
            public override string FriendName => "测试样本类V1.0";
            public Demo()
            {
                this.PluginsName = FriendName;
            }

            private string _demo = "样例参数";
            [Category("参数")]
            [DisplayName("样例参数")]
            [JsonProperty]
            public string Demos
            {
                get { return _demo; }
                set { SetProperty(ref _demo, value, true); Check(); }
            }

            private string _Phone = "18500000000";
            [RegularExpression("""^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$""", ErrorMessage = "请输入正确的手机号码")]
            [Category("参数")]
            [DisplayName("手机号码")]
            [JsonProperty]
            public string PhoneNumber
            {
                get { return _Phone; }
                set { SetProperty(ref _Phone, value, true); Check(); }
            }

            private string _email = "xxxx@qq.com";
            [RegularExpression("""^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$""", ErrorMessage = "邮箱地址")]
            [Category("参数")]
            [DisplayName("邮箱地址")]
            [JsonProperty]
            public string Email
            {
                get { return _email; }
                set { SetProperty(ref _email, value, true); Check(); }
            }

            [Browsable(false)]
            public bool IsActive { get; set; } = true;
            public override void AfterExecution()
            {

            }

            public override void BeforeExecution()
            {

            }

            public override string Execute()
            {

                this.Check();
                if (this.HasErrors == false)
                {
                    AfterExecution();
                    BeforeExecution();
                    LogMessage tep = new LogMessage(this.Demos);
                    tep.PostMsg();
                    return this.Demos;
                }
                else
                {
                    LogMessage tep = new LogMessage("参数配置不正确，请检查");
                    tep.PostError();
                    return "参数配置不正确，请检查";
                }

            }


        }

        public partial class TcpClientHelp : PluginsBase
        {
            public override string FriendName => "TcpClientHelper";
            public TcpClientHelp()
            {
                this.PluginsName = FriendName;
                string OIP = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork).AddressList[0].ToString();
                IP地址Attribute ip = new IP地址Attribute();
                if (ip.IsValid(OIP))
                {
                    RIP = OIP;

                }
                else
                {

                    RIP = "127.0.0.1";
                }

            }
            public override void AfterExecution()
            {
                //throw new NotImplementedException();
            }

            public override void BeforeExecution()
            {
                //throw new NotImplementedException();
            }

            public override string Execute()
            {
                try
                {
                    if (MyTcpClient == null)
                    {
                        MyTcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    if (!MyTcpClient.Connected)
                    {
                        MyTcpClient.Connect(RIP, RPort);
                    }
                    Byte[] data;
                    if (Usencoding == Usencod.ASCII)
                    {
                        data = Encoding.ASCII.GetBytes(Sedtxt);
                    }
                    else
                    {
                        data = Encoding.Unicode.GetBytes(Sedtxt);
                    }
                    MyTcpClient.Send(data);
                    LogMessage tep = new LogMessage($"发送{Sedtxt}完成");
                    tep.PostMsg();
                    data = new byte[1024];
                    int temp = MyTcpClient.Receive(data);

                    if (Usencoding == Usencod.ASCII)
                    {
                        Rectxt = Encoding.ASCII.GetString(data, 0, data.Length);
                    }
                    else
                    {
                        Rectxt = Encoding.Unicode.GetString(data, 0, data.Length);
                    }
                    if (SedHex)
                    {
                        Rectxt = BitConverter.ToString(data, 0, temp).Replace("-", " ").ToUpper();
                    }
                    return $"发送{Sedtxt}完成";
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    LogMessage tep = new LogMessage("服务器断开连接,请重试");
                    tep.PostError();               
                    //MyTcpClient.Dispose();
                    MyTcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) ;
                    return ex.Message;
                }
                catch (Exception ex)
                {

                    LogMessage tep = new LogMessage(ex.Message);
                    tep.PostError();        
                    //MyTcpClient.Dispose();
                    MyTcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    return ex.Message;

                }



            }
            #region IP地址属性
            private string _rip = "127.0.0.1";
            [Category("参数")]
            [DisplayName("远程IP地址")]
            [IP地址]
            [Description("格式形如192.168.1.1")]
            public string RIP
            {
                get { return _rip; }
                set { SetProperty(ref _rip, value, true); Check(); }
            }
            #endregion

            private Socket myVar = null;
            [Browsable(false)]
            // [JsonIgnore]
            public Socket MyTcpClient
            {
                get { return myVar; }
                set { SetProperty(ref myVar, value); }
            }

            private int _rport = 200;
            [Category("参数")]
            [DisplayName("目标端口号")]
            [Description("200")]
            [Range(0, 65535)]
            public int RPort
            {
                get { return _rport; }
                set { SetProperty(ref _rport, value, true); Check(); }
            }

            private string _sed = "hello word !!!";
            [Category("参数")]
            [DisplayName("发送文本：")]
            [Description("发送文本内容")]
            public string Sedtxt
            {
                get { return _sed; }
                set { SetProperty(ref _sed, value, true); Check(); }
            }

            private bool _sedhex = false;
            [Category("参数")]
            [DisplayName("是否16进制显示接收文本：")]
            [Description("是否16进制显示接收文本")]
            public bool SedHex
            {
                get { return _sedhex; }
                set { SetProperty(ref _sedhex, value, true); Check(); }
            }


            private Usencod usencoding;
            [Category("参数")]
            [DisplayName("接收和发送使用的编码")]
            [Description("接收和发送使用的编码")]
            public Usencod Usencoding
            {
                get { return usencoding; }
                set { usencoding = value; }
            }
            public enum Usencod
            {
                ASCII,
                Unicode
            }


            private string _rex = "";
            [Category("参数")]
            [DisplayName("接收文本：")]
            [Description("接收文本内容")]
            public string Rectxt
            {
                get { return _rex; }
                set { SetProperty(ref _rex, value, true); Check(); }
            }
        }
    }
}
