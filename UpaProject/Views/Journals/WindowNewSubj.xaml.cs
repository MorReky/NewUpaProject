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
