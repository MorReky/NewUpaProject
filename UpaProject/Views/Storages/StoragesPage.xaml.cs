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
        private IEnumerable Source= DBConnectHelper.DbObj.Storage_MTR.ToList();
        private static IEnumerable StoragesCollection;


        public StoragesPage()
        {
            InitializeComponent();

            if (ClassUserHelper.Role == 1)
            {
                MTRGrid.IsReadOnly = false;
            }

            StoragesCollection = DBConnectHelper.DbObj.Storages.ToList();

            MTRGrid.ItemsSource = Source;
            CmbGrid.ItemsSource = StoragesCollection;

            CmbStorage.ItemsSource = StoragesCollection;
            CmbStorage.SelectedValue = 10;

            CmbId.ItemsSource = DBConnectHelper.DbObj.MTR.OrderBy(x => x.IdSap).ToList();
            CmbId.SelectedIndex = -1;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += FrameLoad;
            timer.Start();
        }

        private void CmbStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(CmbStorage.SelectedValue) == 10)
                Source = DBConnectHelper.DbObj.Storage_MTR.ToList();
            else
                // MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x=>x.Storage==CmbStorage.SelectedValue.ToString()).ToList();
                //null System.NotSupportedException: "Не удалось создать константу с типом "System.Object" и значением NULL. В этом контексте поддерживаются только типы сущностей, типы перечисления и типы-примитивы
                Source = DBConnectHelper.DbObj.Storage_MTR.Where(x => x.IdStorage.ToString() == CmbStorage.SelectedValue.ToString()).ToList();
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
            MTRGrid.ItemsSource = (Source as IEnumerable<Storage_MTR>).Where(x => x.MTR.Name.ToString().Contains(TxbName.Text));
        }

        private void TxbComments_TextChanged(object sender, TextChangedEventArgs e)
        {
            MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x => x.Comment.ToString().Contains(TxbName.Text));
        }

        private void MTRGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            int IdStorage_mtr;
            if (((sender as DataGrid).CurrentItem as Storage_MTR) != null)
            {
                IdStorage_mtr = ((sender as DataGrid).CurrentItem as Storage_MTR).IDStorage_MTR;
                HistoryStorages historyObj = new HistoryStorages()
                {
                    IdStorage_MTR = ((sender as DataGrid).CurrentItem as Storage_MTR).IDStorage_MTR,
                    IdUser = ClassUserHelper.ID,
                    DateEdit = DateTime.Now,
                    Activity = "Редактирование записи"
                };
                DBConnectHelper.DbObj.HistoryStorages.Add(historyObj);
            }
            DBConnectHelper.DbObj.SaveChanges();
        }

        private async void BtnToExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Range xlSheetRange = null;
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
            MessageBox.Show("Загрузка данных началась. Пожалуйста подождите", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            try
            {
               

                     //добавляем книгу
                     xlApp.Workbooks.Add(Type.Missing);

                     //делаем временно неактивным документ
                     xlApp.Interactive = false;
                     xlApp.EnableEvents = false;

                     //выбираем лист на котором будем работать (Лист 1)
                     xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                     //Название листа
                     xlSheet.Name = "Данные";

                     //Выгрузка данных
                     //DataTable dt = IEnumerableToDataTable.ToDataTable(DataTable);

                     string data = "";

                     xlSheet.Cells[1, 1] = "#п/п";
                     xlSheet.Cells[1, 2] = "Гид";
                     xlSheet.Cells[1, 3] = "Наименование";
                     xlSheet.Cells[1, 4] = "Ед.Изм.";
                     xlSheet.Cells[1, 5] = "Итого";
                     xlSheet.Cells[1, 6] = "Контейнер 1";
                     xlSheet.Cells[1, 7] = "Контейнер 2";
                     xlSheet.Cells[1, 8] = "Контейнер 3";
                     xlSheet.Cells[1, 9] = "Контейнер 4";
                     xlSheet.Cells[1, 10] = "Пом. 185";
                     xlSheet.Cells[1, 11] = "Пом. 303";
                     xlSheet.Cells[1, 12] = "Пом. 328";
                     xlSheet.Cells[1, 13] = "Открытый склад";
                     xlSheet.Cells[1, 14] = "ХАЛ";
                     xlSheet.Cells[1, 15] = "Безвозмездная поставка";
                     xlSheet.Cells[1, 16] = "PLC119";
                     xlSheet.Cells[1, 17] = "Оперативный";
                     xlSheet.Cells[1, 18] = "Несортированное";


                     IEnumerable<Storage_MTR> rececords = DBConnectHelper.DbObj.Storage_MTR.ToList();

                     //Проставляем тип смены колонки
                     for (int i = 0; i < rececords.Count(); i++)
                     {
                         data = rececords.ElementAt(i).MTR.IDMTR.ToString();
                         xlSheet.Cells[i + 2, 1] = i;
                         xlSheet.Cells[i + 2, 2] = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).IdSap;
                         xlSheet.Cells[i + 2, 3] = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).Name;
                         xlSheet.Cells[i + 2, 4] = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).Unit;

                         xlSheet.Cells[i + 2, 5] = DBConnectHelper.DbObj.Storage_MTR.Where(x => x.IdMTR.ToString() == data).Sum(x => x.Quantity);

                         xlSheet.Cells[i + 2, 6] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 1) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 1).Quantity : 0;
                         xlSheet.Cells[i + 2, 7] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 3) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 3).Quantity : 0;
                         xlSheet.Cells[i + 2, 8] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 4) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 4).Quantity : 0;
                         xlSheet.Cells[i + 2, 9] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 7) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 7).Quantity : 0;
                         xlSheet.Cells[i + 2, 10] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 2) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 2).Quantity : 0;
                         xlSheet.Cells[i + 2, 11] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 5) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 5).Quantity : 0;
                         xlSheet.Cells[i + 2, 12] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 6) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 6).Quantity : 0;
                         xlSheet.Cells[i + 2, 13] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 11) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 11).Quantity : 0;
                         xlSheet.Cells[i + 2, 14] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 12) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 12).Quantity : 0;
                         xlSheet.Cells[i + 2, 15] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 8) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 8).Quantity : 0;
                         xlSheet.Cells[i + 2, 16] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 13) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 13).Quantity : 0;
                         xlSheet.Cells[i + 2, 17] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 15) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 15).Quantity : 0;
                         xlSheet.Cells[i + 2, 18] = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 9) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 9).Quantity : 0;


                         //выделяем первую строку
                         xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

                         //делаем полужирный текст и перенос слов
                         xlSheetRange.WrapText = true;
                         xlSheetRange.Font.Bold = true;

                         //xlSheetRange = xlSheet.get_Range("C1:C2000", Type.Missing);

                         //xlSheetRange.WrapText = true;
                         //xlSheetRange.ColumnWidth = 80;
                     }

                     //xlSheet.Cells[1, 3] = "Наименование";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).MTR.Name.ToString();
                     //    xlSheet.Cells[i + 2, 3] = data;
                     //}
                     //xlSheet.Cells[1, 4] = "Ед.изм.";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).MTR.Unit.ToString();
                     //    xlSheet.Cells[i + 2, 4] = data;
                     //}
                     //xlSheet.Cells[1, 6] = "Контейнер 1";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DateOccurence).Last().Day.ToString();
                     //    xlSheet.Cells[i + 2, ] = data;
                     //}
                     //xlSheet.Cells[1, 5] = "Время возникновения";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.TimeOccurence).Last().ToString();
                     //    xlSheet.Cells[i + 2, 5] = data;
                     //}
                     //xlSheet.Cells[1, 6] = "Время решения";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.TimeSolution).Last().ToString();
                     //    xlSheet.Cells[i + 2, 6] = data;
                     //}
                     //xlSheet.Cells[1, 7] = "Время простоя";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DownTime).Last().ToString();
                     //    xlSheet.Cells[i + 2, 7] = data;
                     //}
                     //xlSheet.Cells[1, 8] = "Отделение";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Departments.Name).Last().ToString();
                     //    xlSheet.Cells[i + 2, 8] = data;
                     //}
                     //xlSheet.Cells[1, 9] = "Критичность";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Criticality).Last().ToString();
                     //    xlSheet.Cells[i + 2, 9] = data;
                     //}
                     //xlSheet.Cells[1, 10] = "Тег";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Place.Tag).Last().ToString();
                     //    xlSheet.Cells[i + 2, 10] = data;
                     //}
                     //xlSheet.Cells[1, 11] = "Событие";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Occurence).Last().ToString();
                     //    xlSheet.Cells[i + 2, 11] = data;
                     //}
                     //xlSheet.Cells[1, 12] = "Причина";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Account).Last().ToString();
                     //    xlSheet.Cells[i + 2, 12] = data;
                     //}
                     //xlSheet.Cells[1, 13] = "Устранение";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Solution).Last().ToString();
                     //    xlSheet.Cells[i + 2, 13] = data;
                     //}
                     //xlSheet.Cells[1, 14] = "Комментарии";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Comments).Last().ToString();
                     //    xlSheet.Cells[i + 2, 14] = data;
                     //}
                     //xlSheet.Cells[1, 15] = "Исполнитель";
                     //for (int i = 0; i < rececords.Count(); i++)
                     //{
                     //    int recId = rececords.ElementAt(i).IDOpRecord;
                     //    IEnumerable<Realizers> realizers = DBConnectHelper.DbObj.Realizers.Where(x => recId == x.IdOpRecord).ToList();
                     //    for (int x = 0; x < realizers.Count(); x++)
                     //    {
                     //        if (x == 0)
                     //            data = realizers.ElementAt(x).Persons.Name;
                     //        else
                     //        {
                     //            data += ", ";
                     //            data += realizers.ElementAt(x).Persons.Name;
                     //        }
                     //    }
                     //    xlSheet.Cells[i + 2, 15] = data;
                     //}

                     ////заполняем строки
                     //for (rowInd = 0; rowInd < dt.Rows.Count; rowInd++)
                     //{
                     //    for (collInd = 0; collInd < dt.Columns.Count; collInd++)
                     //    {
                     //        data = dt.Rows[rowInd].ItemArray[collInd].ToString();
                     //        xlSheet.Cells[rowInd + 2, collInd + 1] = data;
                     //    }
                     //}

                     //выбираем всю область данных
                     xlSheetRange = xlSheet.UsedRange;

                     //выравниваем строки и колонки по их содержимому
                     xlSheetRange.Columns.AutoFit();
                     xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                //Показываем ексель
                xlApp.Visible = true;

                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;

                //Отсоединяемся от Excel
                releaseObject(xlSheetRange);
                releaseObject(xlSheet);
                releaseObject(xlApp);
            }
        }
        //Освобождаем ресуры (закрываем Excel)
        void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.ToString(), "Ошибка!");
            }
            finally
            {
                GC.Collect();
            }
        }

        private void BtnNewPosition_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new NewMTRPage());
        }
    }
}
