using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerialPortDataReader
{
    class Writer
    {

        static private string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        static public void writeToFile(double tmp,string path)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(tmp + "");
            sw.Close();
        }

        static public void writeBlockToFile(string s,string path)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(s);
            sw.Close();
        }



    }
}
