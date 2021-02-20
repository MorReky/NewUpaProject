using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    internal class StoragesPageViewModel : ViewModel
    {
        #region Свойства
        #region Источник таблицы
        private IEnumerable<Storage_MTR> _DataTable;
        public IEnumerable<Storage_MTR> DataTable
        {
            get => _DataTable;
            set => Set(ref _DataTable, value);
        }
        #endregion
        #region Выбранный склад
        private string _SelectedStorages;
        public string SelectedStorages
        {
            get => _SelectedStorages;
            set => Set(ref _SelectedStorages, value);
        }
        #endregion

        private IEnumerable<Storages> _StoragesCollection;
        public IEnumerable<Storages> StoragesCollection
        {
            get => _StoragesCollection;
            set => Set(ref _StoragesCollection, value);
        }
         
        #endregion

        #region Комманды

        #region Обновление данных на странице
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p) => DataTable = DBConnectHelper.DbObj.Storage_MTR.ToList();
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion

        #endregion

        public StoragesPageViewModel()
        {
            #region Комманды
            ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            #endregion
            StoragesCollection = DBConnectHelper.DbObj.Storages.ToList();
           
        }

    }
}
