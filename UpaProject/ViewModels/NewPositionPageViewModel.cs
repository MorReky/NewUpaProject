using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
   internal class NewPositionPageViewModel : ViewModel
    {
        #region Свойства
        public IEnumerable<SystemsAsu> ASUSystems { get => DBConnectHelper.DbObj.SystemsAsu.ToList(); }
        private string _Tag;
       public string Tag
        {
            get => _Tag;
            set => Set(ref _Tag, value);
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        #endregion
        #region Комманды
        #region Вернуться назад
        public ICommand GoBack { get; }

        public void OnGoBackExecuted(object p)
        {
            FrameLoader.frmObj.GoBack();
        }

        public bool CanOnGoBackExecute(object p) => true;
        #endregion
        #endregion
        public NewPositionPageViewModel()
        {
            #region Комманды
            GoBack = new LambdaCommand(OnGoBackExecuted, CanOnGoBackExecute);
            #endregion
        }
    }
}
