
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
using UpaProject.Views.Catalogs.EqCatalog;

namespace UpaProject.ViewModels
{
    internal class PersonsCatalogPageViewModel:ViewModel
    {
        #region Свойства
        public IEnumerable<Persons> TableSource { get => DBConnectHelper.DbObj.Persons.ToList(); }
        #endregion

        #region Комманды
        #region Открытие окна Добавления нового сотрудника
        public ICommand AddPerson { get; }
        public void OnAddPersonExecuted(object p)=> FrameLoader.frmObj.Navigate(new PageNewCatalogPage());
        public bool CanOnAddPersonExecute(object p) => true;
        #endregion

        #endregion
        public PersonsCatalogPageViewModel()
        {
            #region Комманды
            AddPerson = new LambdaCommand(OnAddPersonExecuted, CanOnAddPersonExecute);
            #endregion
        }
    }
}
