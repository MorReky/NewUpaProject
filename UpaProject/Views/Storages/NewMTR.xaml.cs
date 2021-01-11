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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Views.Storages
{
    /// <summary>
    /// Логика взаимодействия для NewMTR.xaml
    /// </summary>
    public partial class NewMTR : Page
    {
        static MTR mtr;

        public NewMTR()
        {
            InitializeComponent();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.GoBack();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mtr = new MTR()
                {
                    IdSap = Convert.ToInt32(TxbIdSap.Text),
                    Name = TxbName.Text,
                    Unit = TxbBaseEI.Text
                };
                DBConnectHelper.DbObj.MTR.Add(mtr);
                HistoryMTR historyMTRObj = new HistoryMTR()
                {
                    IdMTR = mtr.IDMTR,
                    IdUser = ClassUserHelper.ID
                };
                DBConnectHelper.DbObj.HistoryMTR.Add(historyMTRObj);
                Storage_MTR storage_MTR = new Storage_MTR()
                {
                    IdMTR = mtr.IDMTR,
                    IdStorage = "Неопределено"
                };
                DBConnectHelper.DbObj.Storage_MTR.Add(storage_MTR);
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show("Данные обновлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Исключение:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
