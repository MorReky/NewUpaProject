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
            CmbDepartment.SelectedIndex = -1;
            CmbSystemASU.ItemsSource = DBConnectHelper.DbObj.SystemsAsu.ToList();
            CmbSystemASU.SelectedIndex = 1;
            CmbTag.ItemsSource = DBConnectHelper.DbObj.Place.ToList();
            CmbSystemASU.SelectedIndex = -1;

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
        }

        private void CmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDepartment.SelectedValue != null)
            {
                CmbSystemASU.ItemsSource = DBConnectHelper.DbObj.SystemsAsu.Where(X => X.IDDepartment.ToString() == CmbDepartment.SelectedValue.ToString()).ToList();
                CmbSystemASU.SelectedIndex = -1;
                CmbTag.SelectedIndex = -1;

            }
        }
        private void CmbSystemASU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSystemASU.SelectedValue != null)
            {
                CmbTag.ItemsSource = DBConnectHelper.DbObj.SystemsAsu_Tags.Where(x => x.IDSystemAsu.ToString() == CmbSystemASU.SelectedValue.ToString()).Select(y => y.Place).ToList();
               // CmbDepartment.SelectedValue = DBConnectHelper.DbObj.SystemsAsu_Tags.Where(x=>x.IDSystemAsu.ToString()==CmbSystemASU.SelectedValue.ToString()).Select(y=>y.SystemsAsu.IDDepartment);
                CmbTag.SelectedIndex = -1;

            }
            else
                CmbTag.ItemsSource = null;
        }
        private void CmbTag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // CmbSystemASU.SelectedValue = DBConnectHelper.DbObj.SystemsAsu_Tags.Where(x => x.IDSystemAsu.ToString() == CmbSystemASU.SelectedValue.ToString()).Select(y => y.IDSystemAsu);
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
