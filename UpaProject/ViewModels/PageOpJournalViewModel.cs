using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using System.Windows.Input;
using System.Windows.Threading;
using UpaProject.Infrastracture.Commands;
using UpaProject.Journals;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace UpaProject.ViewModels
{
    internal class PageOpJournalViewModel : ViewModel
    {
        DispatcherTimer timer;
        #region Свойства
        private IEnumerable<OpShifts_OpRecord> _DataTable;
        public IEnumerable<OpShifts_OpRecord> DataTable
        {
            get => _DataTable;
            set => Set(ref _DataTable, value);
        }
        private string _LastOpenTurn;
        public string LastOpenTurn
        {
            get => _LastOpenTurn;
            set => Set(ref _LastOpenTurn, value);
        }


        #endregion
        #region Комманды
        #region Обновление данных на странице
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p)
        {
            DataTable = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
            LastOpenTurn= DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First().DateStartShift.ToShortDateString().ToString();
        }
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion
        #region Объявление нового события
        public ICommand NewSubject { get; }
        public void OnNewSubjectExecuted(object p)
        {
            WindowNewSubj windowNewSubj = new WindowNewSubj();
            windowNewSubj.ShowDialog();
        }
        #endregion
        #region Объявление новой смены
        public ICommand NewTurn { get; }
        public void OnNewTurnExecuted(object p)
        {
            WindowNewOpTurn newOpTurn = new WindowNewOpTurn();
            newOpTurn.ShowDialog();
        }
        public bool CanOnNewTurnExecute(object p) => true;
        #endregion
        #region Выгрузка в excel
        public ICommand ToExcel { get; set; }
        public void OnToExcelExecuted(object p) => GeneredNewExcel();
        public bool CanOnToExcelExecute(object p) => true;
        #endregion
        public bool CanOnNewSubjectExecute(object p) => true;

        #endregion
        public PageOpJournalViewModel()
        {
            #region Комманды
            ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            NewTurn = new LambdaCommand(OnNewTurnExecuted, CanOnNewTurnExecute);
            NewSubject = new LambdaCommand(OnNewSubjectExecuted, CanOnNewSubjectExecute);
            ToExcel = new LambdaCommand(OnToExcelExecuted, CanOnToExcelExecute);
            #endregion
            // StartTimer();
            LoadData();

        }
        private void LoadData()
        {
            DataTable = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
            LastOpenTurn = DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First().DateStartShift.ToShortDateString().ToString();
        }
        private void StartTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromSeconds(10);
            //timer.Tick += new EventHandler(Tick);        
            timer.Tick += (object sender, EventArgs e) => ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            timer.Start();
        }
        private void GeneredNewExcel()
        {


            #region Запись при помощи Interloop
            Excel.Range xlSheetRange = null;
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
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

                int collInd = 0;
                int rowInd = 0;
                string data = "";

                xlSheet.Cells[1, 1] = "Смена";
                IEnumerable<OpRececord> rececords = DBConnectHelper.DbObj.OpRececord.ToList();

                //Проставляем тип смены колонки
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpShifts.Type).Last().ToString();
                    xlSheet.Cells[i + 2, 1] = data;

                    //выделяем первую строку
                    xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

                    //делаем полужирный текст и перенос слов
                    xlSheetRange.WrapText = true;
                    xlSheetRange.Font.Bold = true;
                }

                xlSheet.Cells[1, 2] = "Год";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DateOccurence).Last().Year.ToString();
                    xlSheet.Cells[i + 2, 2] = data;
                }
                xlSheet.Cells[1, 3] = "Месяц";
                for (int i = 0; i < rececords.Count(); i++)
                {                 
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DateOccurence).Last().Month.ToString();
                    xlSheet.Cells[i + 2, 3] = data;
                }
                xlSheet.Cells[1, 4] = "Число";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DateOccurence).Last().Day.ToString();
                    xlSheet.Cells[i + 2, 4] = data;
                }
                xlSheet.Cells[1,5] = "Время возникновения";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.TimeOccurence).Last().ToString();
                    xlSheet.Cells[i + 2, 5] = data;
                }
                xlSheet.Cells[1, 6] = "Время решения";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.TimeSolution).Last().ToString();
                    xlSheet.Cells[i + 2, 6] = data;
                }
                xlSheet.Cells[1, 7] = "Время простоя";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.DownTime).Last().ToString();
                    xlSheet.Cells[i + 2, 7] = data;
                }
                xlSheet.Cells[1, 8] = "Отделение";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Departments.Name).Last().ToString();
                    xlSheet.Cells[i + 2, 8] = data;
                }
                xlSheet.Cells[1, 9] = "Критичность";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Criticality).Last().ToString();
                    xlSheet.Cells[i + 2, 9] = data;
                }
                xlSheet.Cells[1, 10] = "Тег";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Place.Tag).Last().ToString();
                    xlSheet.Cells[i + 2, 10] = data;
                }
                xlSheet.Cells[1, 11] = "Событие";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Occurence).Last().ToString();
                    xlSheet.Cells[i + 2, 11] = data;
                }
                xlSheet.Cells[1, 12] = "Причина";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Account).Last().ToString();
                    xlSheet.Cells[i + 2, 12] = data;
                }
                xlSheet.Cells[1, 13] = "Устранение";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Solution).Last().ToString();
                    xlSheet.Cells[i + 2, 13] = data;
                }
                xlSheet.Cells[1, 14] = "Комментарии";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    data = rececords.ElementAt(i).OpShifts_OpRecord.Select(x => x.OpRececord.Comments).Last().ToString();
                    xlSheet.Cells[i + 2, 14] = data;
                }
                xlSheet.Cells[1, 15] = "Исполнитель";
                for (int i = 0; i < rececords.Count(); i++)
                {
                    int recId = rececords.ElementAt(i).IDOpRecord;
                    IEnumerable<Realizers> realizers = DBConnectHelper.DbObj.Realizers.Where(x => recId == x.IdOpRecord).ToList();
                    for (int x=0;x<realizers.Count();x++)
                    {
                        if (x == 0)
                            data = realizers.ElementAt(x).Persons.Name;
                        else
                        {
                            data +=", ";
                            data+= realizers.ElementAt(x).Persons.Name;
                        }
                    }
                    xlSheet.Cells[i + 2, 15] = data;
                }
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
        #endregion

    }
    //private void Tick(object sender, EventArgs e)
    //{
    //    //Здесь пишем что должен делать таймер
    //    ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
    //}
}

