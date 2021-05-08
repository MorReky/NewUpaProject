using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
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
        private IEnumerable Source = DBConnectHelper.DbObj.Storage_MTR.ToList();
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

        private void BtnToExcel_Click(object sender, RoutedEventArgs e)
        {
            //Creating a new workbook (Создание новой книги)
            var Wb = new XLWorkbook();
            //Adding a woksheet (добавление рабочего листа)
            var ws = Wb.Worksheets.Add("МТР");

            MessageBox.Show("Загрузка данных началась. Пожалуйста, подождите", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            //try
            //{
            string data = "";
            ws.Cell(1, 1).Value = "#п/п";
            ws.Cell(1, 2).Value = "Гид";
            ws.Cell(1, 3).Value = "Наименование";
            ws.Cell(1, 4).Value = "Ед.Изм.";
            ws.Cell(1, 5).Value = "Итого";
            ws.Cell(1, 6).Value = "Контейнер 1";
            ws.Cell(1, 7).Value = "Контейнер 2";
            ws.Cell(1, 8).Value = "Контейнер 3";
            ws.Cell(1, 9).Value = "Контейнер 4";
            ws.Cell(1, 10).Value = "Пом. 185";
            ws.Cell(1, 11).Value = "Пом. 303";
            ws.Cell(1, 12).Value = "Пом. 328";
            ws.Cell(1, 13).Value = "Открытый склад";
            ws.Cell(1, 14).Value = "ХАЛ";
            ws.Cell(1, 15).Value = "Безвозмездная поставка";
            ws.Cell(1, 16).Value = "PLC119";
            ws.Cell(1, 17).Value = "Оперативный";
            ws.Cell(1, 18).Value = "Несортированное";
            ws.Cell(1, 19).Value = "Дата последнего обновления";

            IEnumerable<Storage_MTR> rececords = DBConnectHelper.DbObj.Storage_MTR.ToList();
            //Проставляем тип смены колонки
            for (int i = 0; i < rececords.Count(); i++)
            {
                data = rececords.ElementAt(i).MTR.IDMTR.ToString();
                ws.Cell(i + 2, 1).Value = i;
                ws.Cell(i + 2, 2).Value = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).IdSap;
                ws.Cell(i + 2, 3).Value = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).Name;
                ws.Cell(i + 2, 4).Value = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IDMTR.ToString() == data).Unit;

                ws.Cell(i + 2, 5).Value = DBConnectHelper.DbObj.Storage_MTR.Where(x => x.IdMTR.ToString() == data).Sum(x => x.Quantity);

                ws.Cell(i + 2, 6).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 1) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 1).Quantity : 0;
                ws.Cell(i + 2, 7).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 3) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 3).Quantity : 0;
                ws.Cell(i + 2, 8).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 4) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 4).Quantity : 0;
                ws.Cell(i + 2, 9).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 7) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 7).Quantity : 0;
                ws.Cell(i + 2, 10).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 2) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 2).Quantity : 0;
                ws.Cell(i + 2, 11).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 5) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 5).Quantity : 0;
                ws.Cell(i + 2, 12).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 6) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 6).Quantity : 0;
                ws.Cell(i + 2, 13).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 11) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 11).Quantity : 0;
                ws.Cell(i + 2, 14).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 12) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 12).Quantity : 0;
                ws.Cell(i + 2, 15).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 8) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 8).Quantity : 0;
                ws.Cell(i + 2, 16).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 13) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 13).Quantity : 0;
                ws.Cell(i + 2, 17).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 15) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 15).Quantity : 0;
                ws.Cell(i + 2, 18).Value = (DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 9) != null) ? DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.IdMTR.ToString() == data && x.IdStorage == 9).Quantity : 0;

                ws.Cell(i + 2, 19).Value = (DBConnectHelper.DbObj.HistoryStorages.FirstOrDefault(x => x.Storage_MTR.MTR.IDMTR.ToString() == data)!=null) ? DBConnectHelper.DbObj.HistoryStorages.FirstOrDefault(x => x.Storage_MTR.MTR.IDMTR.ToString() == data).DateEdit.ToString() : "";
            }
            MessageBox.Show("Готово! StorageTable.xlsx находится на вашем рабочем столе");

            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            Wb.SaveAs($@"C:\Users\{wi.Name.Substring(4)}\Desktop\StorageTable.xlsx");
            
        }

        private void BtnNewPosition_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new NewMTRPage());
        }
    }
}
