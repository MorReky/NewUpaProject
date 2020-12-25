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
        public WindowNewSubj()
        {
            InitializeComponent();
            var source = DBConnectHelper.DbObj.Shifts.Where(x => x.IdShift == DBConnectHelper.DbObj.Shifts.OrderByDescending(y => y.IdShift).FirstOrDefault().IdShift);
            GridDutyPers.ItemsSource = source.ToList();
            GridDutyPers.SelectedIndex = 0;

            CmbDepartment.DisplayMemberPath = "DepartmentName";
            CmbDepartment.SelectedValuePath = "IdDepartment";
            CmbDepartment.ItemsSource = DBConnectHelper.DbObj.DepartmentEq.ToList();

            CmbSystemASU.DisplayMemberPath = "TechObjName";
            CmbSystemASU.SelectedValuePath = "IdItemsOfEq";
            CmbSystemASU.ItemsSource = DBConnectHelper.DbObj.ItemsOfEq.ToList();

            CmbTagMainEq.DisplayMemberPath = "TagAsuMainEq";
            CmbTagMainEq.SelectedValuePath = "IdItemsOfEq";
            CmbTagMainEq.ItemsSource = DBConnectHelper.DbObj.AsuMainEq.ToList();

            CmbTag.DisplayMemberPath = "TagAsuEq";
            CmbTag.SelectedValuePath = "IdAsuEq";
            CmbTag.ItemsSource = DBConnectHelper.DbObj.AsuEq.ToList();

        }

        private void BtnAddNewSubj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(String.IsNullOrEmpty(CmbTag.Text) && String.IsNullOrEmpty(TxbEvent.Text) && String.IsNullOrEmpty(TxbElimination.Text)))
                {
                    //Создавать эту переменную по щелчку на Checkboxs. Так избавимся от "Черточек"
                    string perf="";
                    if (ChbEngineer.IsChecked == true)
                        perf +=" " + ChbEngineer.Content.ToString() + " ";
                    if (ChbEngineer1.IsChecked == true)
                        perf += " " + ChbEngineer1.Content.ToString() + " ";
                    if (ChbRepairmen.IsChecked == true)
                        perf += " " + ChbRepairmen.Content.ToString() + " ";
                    if (ChbRepairmen1.IsChecked == true)
                        perf += " " + ChbRepairmen1.Content.ToString() + " ";
                    if (ChbRepairmen2.IsChecked == true)
                        perf += " " + ChbRepairmen2.Content.ToString() + " ";
                    OpLogJournal opLogJournalObj = new OpLogJournal()
                    {
                        IdShift = DBConnectHelper.DbObj.Shifts.OrderByDescending(y => y.IdShift).FirstOrDefault().IdShift,
                        RecordingDate = DateTime.Now.Date.ToShortDateString(),
                        TimeOccurrence= TxbTimeStart.Text,
                        TimeSolution=TxbTimeEnd.Text,
                        Emergency=CmbSeriousness.SelectedIndex,
                        AsuEq = CmbTag.SelectedItem as AsuEq,
                        Event= TxbEvent.Text,
                        WhatHappend= TxbCause.Text,
                        WhatDoing=TxbElimination.Text,
                        Comments=TxbComment.Text,
                        Writer="Programm",
                        Perfomers=perf
                    };
                    DBConnectHelper.DbObj.OpLogJournal.Add(opLogJournalObj);
                    DBConnectHelper.DbObj.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена",
                                        "Уведомление",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Information);
            }
                else
                    MessageBox.Show("Поля 'Теговый номер','Событие','Решение' обязательны для заполнения",
                                        "ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
        }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая работа с приложением. " + ex.Message.ToString(),
                                "Уведомление",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Warning);
            }
}
    }
}
