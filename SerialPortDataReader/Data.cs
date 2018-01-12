using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortDataReader
{
    class Data
    {
        //karsidan alinan veriler
        #region
        private double temperatureData1;
        private double temperatureData2;
        private double temperatureData3;
        private double temperatureData4;
        #endregion

        //get ve set metodlari
        #region
        public double TemperatureData1
        {
            get
            {
                return temperatureData1;
            }

            set
            {
                temperatureData1 = value;
            }
        }
        public double TemperatureData2
        {
            get
            {
                return temperatureData2;
            }

            set
            {
                temperatureData2 = value;
            }
        }
        public double TemperatureData3
        {
            get
            {
                return temperatureData3;
            }

            set
            {
                temperatureData3 = value;
            }
        }
        public double TemperatureData4
        {
            get
            {
                return temperatureData4;
            }

            set
            {
                temperatureData4 = value;
            }
        }
        #endregion

        public static double[] splitToArray(int size, string message)
        {
            double[] datas = new double[size];
            int j = 0;
            string s = message.Substring(1, message.Length - 3);
            string data = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s.ElementAt(i) != '-')
                {
                    data = data + s.ElementAt(i);                    
                }
                else
                {
                    datas[j] = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                    j++;
                    data = "";
                }
                if (i == s.Length - 1)
                {
                    datas[j] = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return datas;
        }
    }
}
