using HandyControl.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using WpfApp1.ViewModels;

namespace WpfApp1.Validation
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

}

