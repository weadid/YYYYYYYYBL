using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace учёт_бюджета
{
    public class RecordType
    {
        public string Name { get; set; }
        public RecordType(string name)
        {
            Name = name;
        }
        public static List<RecordType> RecordTypes = new List<RecordType>();

    }
}
