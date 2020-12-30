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
           
        }
        

        ///Убрать выбранные элементы        
    }
}
