using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Markup;

namespace учёт_бюджета
{
    public static class MyJsonConvert
    {
        static string pathRecord = "Records.json";
        static string pathRecordTypes = "RecordTypes.json";


        public static void Serialization<T>(T data)
        {

            string json = JsonConvert.SerializeObject(data);
            string path;
            if (typeof(T) == typeof(List<Record>))
            {
                path = pathRecord;
            }
            else
            {
                path = pathRecordTypes;
            }
            File.WriteAllText(path, json);

        }

        public static void Deserialization<T>()
        {
            if (typeof(T) == typeof(List<Record>))
            {
                if (File.Exists(pathRecord))
                {
                Record.Records = JsonConvert.DeserializeObject<List<Record>>(File.ReadAllText(pathRecord));
                }
            }
            else
            {
                if (File.Exists(pathRecordTypes))
                {
                    RecordType.RecordTypes = JsonConvert.DeserializeObject<List<RecordType>>(File.ReadAllText(pathRecordTypes));
                }
            }
        }

    }
}
