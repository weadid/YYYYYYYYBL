using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace учёт_бюджета
{
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = Texttype.Text;
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Тип записи пустой");
            }
            else if (RecordType.RecordTypes.Where(r => r.Name == type).Count() > 0)
            {
                MessageBox.Show("Такой тип записи уже есть");
            }
            else
            {
                RecordType recordType = new RecordType(type);
                RecordType.RecordTypes.Add(recordType);
                MessageBox.Show("Тип записи добавлен");
                Close();

            }

        }



    }
}
