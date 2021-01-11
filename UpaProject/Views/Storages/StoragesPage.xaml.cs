using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Models.DataFilesApp;
using Excel = Microsoft.Office.Interop.Excel;

namespace UpaProject.Views.Storages
{
    /// <summary>
    /// Логика взаимодействия для StoragesPage.xaml
    /// </summary>
    public partial class StoragesPage : System.Windows.Controls.Page
    {
        private Storage_MTR storage_MTR = new Storage_MTR();
        private IEnumerable Source;
        private static IEnumerable StoragesCollection;


        public StoragesPage()
        {
            InitializeComponent();

            if (ClassUserHelper.Role == 1)
            {
                MTRGrid.IsReadOnly = false;
            }

            StoragesCollection = DBConnectHelper.DbObj.Storages.ToList();

            MTRGrid.ItemsSource = DBConnectHelper.DbObj.MTR.ToList();
            CmbGrid.ItemsSource = StoragesCollection;

            CmbStorage.ItemsSource = StoragesCollection;
            CmbStorage.Text = "Общий склад";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += FrameLoad;
            timer.Start();
        }

        private void CmbStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbStorage.SelectedValue.ToString() == "Общий склад")
                Source = DBConnectHelper.DbObj.MTR.ToList();
            else
                // MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x=>x.Storage==CmbStorage.SelectedValue.ToString()).ToList();
                //Source = DBConnectHelper.DbObj.Storage_MTR.Where(x => x.IdStorage == CmbStorage.SelectedValue.ToString()).ToList();
                Source = DBConnectHelper.DbObj.MTR.Select(x => x.Storage_MTR.FirstOrDefault(y => y.IdStorage == CmbStorage.SelectedValue.ToString())).ToList();
            MTRGrid.ItemsSource = Source;
        }

        private void FrameLoad(object sender, object e)
        {
            MTRGrid.ItemsSource = MTRGrid.ItemsSource;
        }

        private void CmbId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbId.SelectedValue != null)
                MTRGrid.ItemsSource = (Source as IEnumerable<Storage_MTR>).Where(x => x.MTR.IdSap.ToString() == CmbId.SelectedValue.ToString()).ToList();
            else
                MTRGrid.ItemsSource = Source;
        }

        private void TxbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MTRGrid.ItemsSource = (Source as IEnumerable<MTR>).Where(x => x.Name.ToString().Contains(TxbName.Text));
        }

        private void TxbComments_TextChanged(object sender, TextChangedEventArgs e)
        {
            MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x => x.Comment.ToString().Contains(TxbName.Text));
        }

        private void MTRGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //try
            //{
            int IdStorage_mtr = ((sender as DataGrid).CurrentItem as MTR).Storage_MTR.First().IDStorage_MTR;
                HistoryStorages historyObj = new HistoryStorages()
                {
                    IdStorage_MTR = IdStorage_mtr,
                    IdUser = ClassUserHelper.ID
                };
                DBConnectHelper.DbObj.HistoryStorages.Add(historyObj);
                DBConnectHelper.DbObj.SaveChanges();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Исключение:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void BtnToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Application ex = new Excel.Application();

                ex.SheetsInNewWorkbook = 1;
                ex.DisplayAlerts = false;
                ex.Visible = true;

                Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet = (Worksheet)workbook.Sheets[1];

                sheet.Name = "Данные по складским помещениям";

                for (int j = 0; j < MTRGrid.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet.Cells[1, j + 1];
                    sheet.Cells[1, j + 1].Font.Bold = true;
                    myRange.Value2 = MTRGrid.Columns[j].Header;
                }
                for (int j = 1; j < MTRGrid.Columns.Count; j++)
                {
                    for (int i = 0; i < MTRGrid.Items.Count; i++)
                    {
                        TextBlock b = MTRGrid.Columns[i].GetCellContent(MTRGrid.Items[j]) as TextBlock;
                        Range myRange = (Range)sheet.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNewRow_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new NewMTR());
        }
    }
}
