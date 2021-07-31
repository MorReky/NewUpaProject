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
using UpaProject.Models.DataFilesApp;

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

            DtOccur.SelectedDate = DateTime.Today;

            CmbDutyEngKIP.ItemsSource = DBConnectHelper.DbObj.Persons.Where(x => x.IdWorkGroup == 3 && x.Change == Properties.Settings.Default.DeafaultChange).ToList();
            CmbDutyEngKIP.Text = "-";
            CmbDutyRep1.ItemsSource = DBConnectHelper.DbObj.Persons.Where(x => x.IdWorkGroup == 2&&x.Change==Properties.Settings.Default.DeafaultChange).ToList();
            CmbDutyRep1.Text = "-";
            CmbDutyRep2.ItemsSource = DBConnectHelper.DbObj.Persons.Where(x => x.IdWorkGroup == 2 && x.Change == Properties.Settings.Default.DeafaultChange).ToList();
            CmbDutyRep2.Text = "-";
            CmbDutyRep3.ItemsSource = DBConnectHelper.DbObj.Persons.Where(x => x.IdWorkGroup == 2 && x.Change == Properties.Settings.Default.DeafaultChange).ToList();
            CmbDutyRep3.Text = "-";
        }

        private void BtnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddNewTurn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DtOccur.SelectedDate != null)
                {
                    int typeSheme = 0;
                    if (RdbDayTurn.IsChecked == true)
                        typeSheme = 1;
                    if (RdbNightTurn.IsChecked == true)
                        typeSheme = 2;
                    if (typeSheme != 0)
                    {
                        OpShifts Shift = new OpShifts()
                        {
                            DateStartShift = DtOccur.SelectedDate.Value,
                            Type = typeSheme
                        };
                        DBConnectHelper.DbObj.OpShifts.Add(Shift);
                        if (CmbDutyEngKIP.Text != "-")
                            AddPerson(Shift.IDShift, Convert.ToInt32(CmbDutyEngKIP.SelectedValue));
                        if (CmbDutyRep1.Text != "-")
                            AddPerson(Shift.IDShift, Convert.ToInt32(CmbDutyRep1.SelectedValue));
                        if (CmbDutyRep2.Text != "-")
                            AddPerson(Shift.IDShift, Convert.ToInt32(CmbDutyRep2.SelectedValue));
                        if (CmbDutyRep3.Text != "-")
                            AddPerson(Shift.IDShift, Convert.ToInt32(CmbDutyRep3.SelectedValue));
                        DBConnectHelper.DbObj.SaveChanges();
                        MessageBox.Show("Смена успешно объявлена");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Необходимо указать тип смены");
                }
                else
                    MessageBox.Show("Необходимо указать дату начала смены");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сбой программы. Пожалуйста,обратитесь к администратору:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPerson(int idShift,int idPerson)
        {
            Shifts_Persons shifts_Persons = new Shifts_Persons()
            {
                IdShift = idShift,
                IdPerson = idPerson,
            };
            DBConnectHelper.DbObj.Shifts_Persons.Add(shifts_Persons);
        }       
    }
}
