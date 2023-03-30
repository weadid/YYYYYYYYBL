using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace учёт_бюджета
{

    public partial class MainWindow : Window
    {

        Record selectedRecord = null;
        
        public MainWindow()
        {
            InitializeComponent();
            UpdateRecordTypesComboBox();
            RecordDatePicker.SelectedDate = DateTime.Today;

            UpdateRecordsDataGrid();
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.ShowDialog();
            UpdateRecordTypesComboBox();
            MyJsonConvert.Serialization<List<RecordType>>(RecordType.RecordTypes);
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            
        }
        


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = Namebox.Text;
            RecordType recordType = Typebox.SelectedItem as RecordType;
            string strMoney = Mbox.Text;
            if (string.IsNullOrEmpty(name) || NewRecordDatePicker.SelectedDate == null || recordType == null || string.IsNullOrEmpty(strMoney))
            {
                MessageBox.Show("Не все данные заполнены");
            }
            else if (!decimal.TryParse(strMoney, out decimal money))
            {
                MessageBox.Show("Сумма не соответствует денежному типу данных ");
            }
            else 
            { 
                Record record = new Record(name, DateOnly.FromDateTime(NewRecordDatePicker.SelectedDate.Value), recordType, money);
                Record.Records.Add(record);
                MyJsonConvert.Serialization<List<Record>>(Record.Records);
                MessageBox.Show("Запись добавлена");
                UpdateRecordsDataGrid();
                Namebox.Clear();
                NewRecordDatePicker.SelectedDate = null;
                Typebox.SelectedItem = null;
                Mbox.Clear();
            }



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("Для начала выберите запись в таблице");
            }
            else
            {
                string name = ChangeNamebox.Text;
                RecordType recordType = ChangeTypebox.SelectedItem as RecordType;
                string strMoney = ChangeMbox.Text;
                if (string.IsNullOrEmpty(name) || ChangeRecordDatePicker.SelectedDate == null || recordType == null || string.IsNullOrEmpty(strMoney))
                {
                    MessageBox.Show("Не все данные заполнены");
                }
                else if (!decimal.TryParse(strMoney, out decimal money))
                {
                    MessageBox.Show("Сумма не соответствует денежному типу данных ");
                }
                else
                {
                    selectedRecord.Name = name;
                    selectedRecord.Date = DateOnly.FromDateTime(ChangeRecordDatePicker.SelectedDate.Value);
                    selectedRecord.RecordType = recordType;
                    selectedRecord.Money = money;
                    MyJsonConvert.Serialization<List<Record>>(Record.Records);
                    MessageBox.Show("Запись изменена");
                    UpdateRecordsDataGrid();
                    ChangeNamebox.Clear();
                    ChangeRecordDatePicker.SelectedDate = null;
                    ChangeTypebox.SelectedItem = null;
                    ChangeMbox.Clear();
                }
            }

        }

        private void UpdateRecordsDataGrid()
        {
            MyJsonConvert.Deserialization<List<Record>>();
            RecordsDataGrid.ItemsSource = null;
            List<Record> records = Record.Records.Where(r => r.Date == DateOnly.FromDateTime(RecordDatePicker.SelectedDate.Value)).ToList();
            RecordsDataGrid.ItemsSource = records;
            decimal result = 0;
            foreach (Record record in records)
            {
                if (record.TypeMoney == "Прибавка")
                {
                    result += record.Money;
                }
                else
                {
                    result -= record.Money;
                }
            }
            ResultBox.Text = "Итог: " + result;

        }

        private void UpdateRecordTypesComboBox()
        {
            MyJsonConvert.Deserialization<List<RecordType>>();
            ChangeTypebox.ItemsSource = null;
            ChangeTypebox.ItemsSource = RecordType.RecordTypes.ToList();
            Typebox.ItemsSource = null;
            Typebox.ItemsSource = RecordType.RecordTypes.ToList();
        }

        private void RecordsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecord = RecordsDataGrid.SelectedItem as Record;
            if (selectedRecord == null)
            {
                ChangeNamebox.Clear();
                ChangeRecordDatePicker.SelectedDate = null;
                ChangeTypebox.SelectedItem = null;
                ChangeMbox.Clear();

            }
            else
            {
                ChangeNamebox.Text = selectedRecord.Name;
                ChangeRecordDatePicker.SelectedDate = selectedRecord.Date.ToDateTime(TimeOnly.Parse("10:00 PM"));
                ChangeTypebox.SelectedIndex = Array.IndexOf(RecordType.RecordTypes.Select(r => r.Name).ToArray(), selectedRecord.RecordType.Name);
                ChangeMbox.Text = int.Parse(selectedRecord.Money.ToString()).ToString();
            }
        }

        private void RecordDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecordsDataGrid();
        }

        private void Mbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789-".IndexOf(e.Text) < 0;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("Для начала выберите запись в таблице");
            }
            else
            {
                Record.Records.Remove(selectedRecord);
                MyJsonConvert.Serialization<List<Record>>(Record.Records);
                MessageBox.Show("Запись удалена");
                UpdateRecordsDataGrid();
            }
        }

        private void ChangeMbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        


    }
}
