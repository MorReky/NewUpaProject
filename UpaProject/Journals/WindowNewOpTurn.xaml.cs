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
using UpaProject.DataFilesApp;

namespace UpaProject.Journals
{
    /// <summary>
    /// Логика взаимодействия для WindowNewOpTurn.xaml
    /// </summary>
    public partial class WindowNewOpTurn : Window
    {
        // Есть что автоматизировать и тут

        public WindowNewOpTurn()
        {
            InitializeComponent();

            CmbDutyEngKIP.SelectedValuePath = "Id";
            CmbDutyEngKIP.DisplayMemberPath = "SNP";
            CmbDutyEngKIP.ItemsSource = DBConnectHelper.DbObj.Engineers.OrderBy(x => x.SNP).ToList();

            CmbDutyEngASU.SelectedValuePath = "Id";
            CmbDutyEngASU.DisplayMemberPath = "SNP";
            CmbDutyEngASU.ItemsSource = DBConnectHelper.DbObj.Engineers.OrderBy(x => x.SNP).ToList();

            CmbDutyRep1.SelectedValuePath = "Id";
            CmbDutyRep1.DisplayMemberPath = "SNP";
            CmbDutyRep1.ItemsSource = DBConnectHelper.DbObj.Repairmens.OrderBy(x => x.SNP).ToList();

            CmbDutyRep2.SelectedValuePath = "Id";
            CmbDutyRep2.DisplayMemberPath = "SNP";
            CmbDutyRep2.ItemsSource = DBConnectHelper.DbObj.Repairmens.OrderBy(x => x.SNP).ToList();

            CmbDutyRep3.SelectedValuePath = "Id";
            CmbDutyRep3.DisplayMemberPath = "SNP";
            CmbDutyRep3.ItemsSource = DBConnectHelper.DbObj.Repairmens.OrderBy(x => x.SNP).ToList();

            CmbDutyEngKIP.SelectedIndex = 0;
            CmbDutyEngASU.SelectedIndex = 0;
            CmbDutyRep1.SelectedIndex = 0;
            CmbDutyRep2.SelectedIndex = 0;
            CmbDutyRep3.SelectedIndex = 0;

            DtOccur.SelectedDate = DateTime.Today;
        }

        private void BtnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddNewTurn_Click(object sender, RoutedEventArgs e)
        {
            if (!(RdbDayTurn.IsEnabled || RdbNightTurn.IsEnabled))
            {
                MessageBox.Show("Для создания новой смены необходимо указать ее тип",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    DateTime DateEnd = DtOccur.SelectedDate.Value;

                    if (RdbNightTurn.IsChecked==true)
                        DateEnd = DateEnd.AddDays(1);
                    if (DBConnectHelper.DbObj.Shifts.Where(x => x.DateStartShift == DtOccur.SelectedDate.Value && x.DateEndShift == DateEnd).Any())
                    {
                        MessageBox.Show("Смена:" + DtOccur.SelectedDate + " - " + DateEnd + " уже объявлена",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                    }
                    else
                    {
                        Shifts shiftObj = new Shifts()
                        {
                            DateStartShift = DtOccur.SelectedDate.Value,
                            DateEndShift = DateEnd,
                            Engineers = CmbDutyEngKIP.SelectedItem as Engineers,
                            Engineers1 = CmbDutyEngASU.SelectedItem as Engineers,
                            Repairmens = CmbDutyRep1.SelectedItem as Repairmens,
                            Repairmens1 = CmbDutyRep2.SelectedItem as Repairmens,
                            Repairmens2 = CmbDutyRep3.SelectedItem as Repairmens,
                        };                        
                        DBConnectHelper.DbObj.Shifts.Add(shiftObj);
                        DBConnectHelper.DbObj.SaveChanges();
                        //Строго после того как добавлена запись о смене-привязка к ее ID
                        //Конечно, в дальнейшем можно просто инкрементировать Id записи и разгрузить программу
                        OpLogJournal opLogJournalObj = new OpLogJournal()
                        {
                            IdShift = DBConnectHelper.DbObj.Shifts.Where(x => x.DateStartShift == DtOccur.SelectedDate.Value && x.DateEndShift == DateEnd).FirstOrDefault().IdShift,
                            RecordingDate=DateTime.Now.ToShortDateString().ToString(),
                            Event = "Создана новая смена: " + DtOccur.SelectedDate + " - " + DateEnd,
                            Comments= CmbDutyEngKIP.Text+","+ CmbDutyEngASU.Text + ","+ CmbDutyRep1.Text + ","+ CmbDutyRep2.Text + ","+ CmbDutyRep3.Text + ","
                        };
                        DBConnectHelper.DbObj.OpLogJournal.Add(opLogJournalObj);
                        DBConnectHelper.DbObj.SaveChanges();
                        MessageBox.Show("Смена:" + shiftObj.DateStartShift + " - " + shiftObj.DateEndShift + " успешно создана",
                                         "Информация",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
            {
                MessageBox.Show("Критическая работа с приложением " + ex.Message.ToString(),
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }
        }

        ///Убрать выбранные элементы        
    }
}
