using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Static;
using Common.Structure;


namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Enumerable.Range(0, 100).Select(i=>new
            {
                x = i,
                y = i*i,
                z = string.Join("",Enumerable.Repeat("Do",i))
            });

            
        }


    }


}
