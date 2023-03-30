using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace учёт_бюджета
{
    public class Record
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public RecordType RecordType { get; set; }
        public decimal Money { get; set; }
        public string TypeMoney { get; set; }


        public static List<Record> Records = new List<Record>();

        public Record(string name, DateOnly date, RecordType typename, decimal money)
        {
            Name = name;
            RecordType = typename;
            Date = date;
            
            if (money >= 0)
            {
                TypeMoney = "Прибавка";
                Money = money;
            }
            else
            {
                TypeMoney = "Вычет";
                Money = Math.Abs(money);
            }
        }
    }
}
