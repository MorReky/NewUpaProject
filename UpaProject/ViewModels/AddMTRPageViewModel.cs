using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    public class AddMTRPageViewModel : ViewModel
    {
        #region Свойства
        #region IEnumerable<MTR> MTRs
      //  public readonly IEnumerable<MTR> MTRs = DBConnectHelper.DbObj.MTR.ToList();

        #endregion
        #region SapId
        private string _IdSap;
        public string IdSap
        {
            get => _IdSap;
            set => Set(ref _IdSap, value);
        }
        #endregion
        #region NameMTR
        private string _NameMTR;
        public string NameMTR
        {
            get => _NameMTR;
            set => Set(ref _NameMTR, value);
        }
        #endregion
        #region UnitMTR
        private string _UnitMTR;
        public string UnitMTR
        {
            get => _UnitMTR;
            set => Set(ref _UnitMTR, value);
        }
        #endregion
        #region ErrorStatus
        public Visibility ErrorStatus
        {
            get => (_ErrorText.Length > 1) ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion
        #region ErrorText
        private string _ErrorText;
        public string ErrorText
        {
            get => _ErrorText;
            set => Set(ref _ErrorText, value);
        }
        #endregion

        #endregion

        #region Комманды
        #region Добавление нового МТР
        public ICommand AddNewMTR { get; }
        public async void OnAddNewMTRExecute(object p)
        {
            MTR mtrObj = new MTR
            {
                IdSap = Convert.ToInt32(IdSap),
                Name = NameMTR,
                Unit = UnitMTR
            };
            await Task.Run(() =>
            {
                DBConnectHelper.DbObj.MTR.Add(mtrObj);
                DBConnectHelper.DbObj.SaveChanges();
            });
            MessageBox.Show($"МТР {mtrObj.IdSap} {mtrObj.Name} успешно добавлен!", "");
            FrameApp.FrameLoader.frmObj.GoBack();
        }
        public bool CanOnAddNewMTRExecuted(object p)
        {
            if (IdSap == "")
            {
                ErrorText = "Поле ГИД не может быть пустым\n";
                return false;
            }
            if (NameMTR == "")
            {
                ErrorText = "Поле Наименование не может быть пустым\n";
                return false;
            }
            //if (DBConnectHelper.DbObj.MTR.Any(x=>x.IdSap.ToString()==IdSap))
            //{
            //    ErrorText = "Указанный МТР уже есть в Базе данных";
            //    return false;
            //}
            ErrorText = "";
            return true;
        }
        #endregion

        #region Вернуться назад
        public ICommand GoBack { get; }
        public void OnGoBackExecuted(object p)
        {
            FrameLoader.frmObj.GoBack();
        }
        public bool CanOnGoBackExecute(object p) => true;
        #endregion
        #endregion

        public AddMTRPageViewModel()
        {
            #region Комманды
            AddNewMTR = new LambdaCommand(OnAddNewMTRExecute, CanOnAddNewMTRExecuted);
            GoBack = new LambdaCommand(OnGoBackExecuted, CanOnGoBackExecute);
            #endregion

        }

    }
}
