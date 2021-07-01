using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinOCR;

namespace ExampleConsoleApp {
    class Program {
        static void Main (string[] args) {
            var path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop) + @"\キャプチャ.PNG";
            var res = string.Join (Environment.NewLine,
                new WinOCR.WinOCR ().ReadLine (path)
            );

            Console.WriteLine (res);
            Console.ReadKey ();
        }
    }
}