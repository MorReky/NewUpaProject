﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using Excel = Microsoft.Office.Interop.Excel;

namespace UpaProject.ViewModels
{
    internal class MTRCatalogsPageViewModel:ViewModel
    {
        #region Свойства
        public IEnumerable<MTR> TableSource { get => DBConnectHelper.DbObj.MTR.ToList(); set => DBConnectHelper.DbObj.SaveChanges(); }
		#endregion

		#region Комманды
		#region Выгрузка в excel
		public ICommand ToExcel { get; set; }
		public void OnToExcelExecuted(object p) => GeneredNewExcel();
		public bool CanOnToExcelExecute(object p) => true;
		#endregion


		#endregion

		public MTRCatalogsPageViewModel()
        {
			#region Комманды
			ToExcel = new LambdaCommand(OnToExcelExecuted, CanOnToExcelExecute);
            #endregion
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
				xlSheet.Name = "Справочник МТР";

				//Выгрузка данных
				//DataTable dt = IEnumerableToDataTable.ToDataTable(DataTable);

				string data = "";

				xlSheet.Cells[1, 1] = "ГИД";
				IEnumerable<MTR> rececords = DBConnectHelper.DbObj.MTR.ToList();

				for (int i = 0; i < rececords.Count(); i++)
				{
					data = rececords.ElementAt(i).IdSap.ToString();
					xlSheet.Cells[i + 2, 1] = data;

					//выделяем первую строку
					xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

					//делаем полужирный текст и перенос слов
					xlSheetRange.WrapText = true;
					xlSheetRange.Font.Bold = true;
				}

				xlSheet.Cells[1, 2] = "Название МТР";
				for (int i = 0; i < rececords.Count(); i++)
				{
					data = rececords.ElementAt(i).Name;
					xlSheet.Cells[i + 2, 2] = data;
				}
				xlSheet.Cells[1, 3] = "Базовая ЕИ";
				for (int i = 0; i < rececords.Count(); i++)
				{
					data = rececords.ElementAt(i).Unit;
					xlSheet.Cells[i + 2, 3] = data;
				}
								
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
}