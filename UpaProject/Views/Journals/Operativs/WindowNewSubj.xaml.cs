using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Journals
{
    /// <summary>
    /// Логика взаимодействия для WindowNewSubj.xaml
    /// </summary>
    public partial class WindowNewSubj : Window
    {
        private static OpShifts opShiftsObj;

        public WindowNewSubj()
        {
            InitializeComponent();
            opShiftsObj = DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First();
            TxtDate.Text = opShiftsObj.DateStartShift.ToShortDateString().ToString();
            TxtType.Text = opShiftsObj.Type == 1 ? "Дневная" : "Ночная";

            CmbDepartment.ItemsSource = DBConnectHelper.DbObj.Departments.ToList();
            CmbSystemASU.ItemsSource = DBConnectHelper.DbObj.SystemsAsu.ToList();
            CmbTag.ItemsSource = DBConnectHelper.DbObj.Place.ToList();

            LBPersonal.ItemsSource = DBConnectHelper.DbObj.Shifts_Persons.Where(y=>y.IdShift== opShiftsObj.IDShift).Select(x => x.Persons).ToList();
        }

        private void BtnAddNewSubj_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error=new StringBuilder();
            

            if (TxbTimeStart.Text==null||TxbTimeEnd.Text==null)
                error.Append("Поля 'Время начала:' и 'Время окончания:' обязательны для заполнения\n");
            if (CmbDepartment.SelectedValue == null)
                error.Append("Поле 'Отделение' обязательно для заполнения\n");
            if (CmbTag.Text=="")
                error.Append("Поле 'Теговый номер' обязательно для заполнения\n");
            if (TxbEvent.Text=="")
                error.Append("Поле 'Событие' обязательно для заполнения\n");
            if(LBRealizers.Items.Count== 0)
                error.Append("Необходимо выбрать исполнителей работы\n");
            if (error.Length > 0)
                MessageBox.Show(error.ToString());
            else
            {
                //Создали запись в таблице записей
                OpRececord opRececordObj = new OpRececord
                {
                    DateOccurence = Convert.ToDateTime(TxtDate.Text),
                    TimeOccurence = TimeSpan.Parse(TxbTimeStart.Text),
                    TimeSolution=TimeSpan.Parse(TxbTimeEnd.Text),
                    DownTime= TimeSpan.Parse(TxbDownTime.Text),
                    IdDepartment =Convert.ToInt32(CmbDepartment.SelectedValue),
                    Criticality=CmbSeriousness.SelectedIndex,
                    IdPlace= Convert.ToInt32(CmbTag.SelectedValue),
                    Occurence=TxbEvent.Text,
                    Account=TxbCause.Text,
                    Solution=TxbElimination.Text,
                    Comments=TxbComment.Text
                };
                DBConnectHelper.DbObj.OpRececord.Add(opRececordObj);
                //Создали запись в сводной таблице запись-смена
                OpShifts_OpRecord opShifts_opRecordObj = new OpShifts_OpRecord()
                {
                    IdOpRecord = opRececordObj.IDOpRecord,
                    IdShift = opShiftsObj.IDShift
                };
                DBConnectHelper.DbObj.OpShifts_OpRecord.Add(opShifts_opRecordObj);
                //Добавляем исполнителей работу в сводную таблицу запись-исполнители
                foreach (object objRelizer in LBRealizers.Items)
                {
                    Realizers realizers = new Realizers()
                    {
                        IdOpRecord = opRececordObj.IDOpRecord,
                        IdPerson = (objRelizer as Persons).IDPerson
                    };
                    DBConnectHelper.DbObj.Realizers.Add(realizers);
                }
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }

            //    try
            //    {
            //        if (!(String.IsNullOrEmpty(CmbTag.Text) && String.IsNullOrEmpty(TxbEvent.Text) && String.IsNullOrEmpty(TxbElimination.Text)))
            //        {


            //            MessageBox.Show("Запись успешно добавлена",
            //                                "Уведомление",
            //                                 MessageBoxButton.OK,
            //                                 MessageBoxImage.Information);
            //    }
            //        else
            //            MessageBox.Show("Поля 'Теговый номер','Событие','Решение' обязательны для заполнения",
            //                                "ошибка",
            //                                 MessageBoxButton.OK,
            //                                 MessageBoxImage.Error);
            //}
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Критическая работа с приложением. " + ex.Message.ToString(),
            //                        "Уведомление",
            //                         MessageBoxButton.OK,
            //                         MessageBoxImage.Warning);
            //    }
        }

        private void CmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxbTimeStart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890:".IndexOf(e.Text)<0;
        }

        private void BtnPersonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] list = new object[LBPersonal.SelectedItems.Count];
            LBPersonal.SelectedItems.CopyTo(list, 0);
            foreach (object i in list)
            {
                LBRealizers.Items.Add(i);
                
            }
        }
    }
}
