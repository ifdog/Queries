using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Common.Static
{
    public static class Excel
    {
        public static string GetInstance()
        {
            var xx = Marshal.GetActiveObject("Excel.Application") as Application;
            return xx.ActiveCell.Value;
        }
    }
}
