using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    internal class StoragesPageViewModel : ViewModel
    {
        #region Свойства
        #region TableSource
        private IEnumerable<Storage_MTR> _TableSource;

        public IEnumerable<Storage_MTR> TableSource
        {
            get=> _TableSource;
            set => Set(ref _TableSource, value);
            //set
            //{

            //    //if (IdSapFilter!=null)
            //    //    Set(ref _TableSource, DBConnectHelper.DbObj.Storage_MTR.Where(x => x.MTR.IdSap.ToString() == IdSapFilter).ToList());
            //    //else
            //}
        }
        #endregion
        #region StoragesCollection
        public IEnumerable<Storages> StoragesCollection => DBConnectHelper.DbObj.Storages.ToList();
        #endregion
        #region SelectedStoragesFilter
        private Storages _SelectedStoragesFilter;
        public Storages SelectedStoragesFilter
        {
            get => _SelectedStoragesFilter;
            set => Set(ref _SelectedStoragesFilter, value);
        }
        #endregion
        #region EditTable
        public bool EditTable
        {
            get => ClassUserHelper.Role == 3 ? true : false;
        }
        #endregion
        #region SelectedRow
        private Storage_MTR _SelectedRow;
        public Storage_MTR SelectedRow
        {
            get => _SelectedRow;
            set
            {
                if (!Equals(_SelectedRow, value))
                {
                    _SelectedRow = value;
                    DBConnectHelper.DbObj.SaveChanges();
                    OnPropertyChanged("SelectedRow");
                }
                else
                    _SelectedRow = value;
            }
        }
        #endregion
        #region MTRCollection
        public IEnumerable<MTR> MTRCollection { get => DBConnectHelper.DbObj.MTR.OrderBy(x => x.IdSap).ToList(); }
        #endregion
        #region IdSapFilter
        private string _IdSapFilter;
        public string IdSapFilter
        {
            get => _IdSapFilter;
            set
            {
                if ((!Equals(_IdSapFilter, value)))
                    Set(ref _IdSapFilter, value);
            }
        }
        #endregion
        #region NameFilter
        private string _NameFilter;
        public string NameFilter
        {
            get => _NameFilter;
            set => Set(ref _NameFilter, value);
        }

        #endregion
        #region CommentsFilter
        private string _CommentsFilter;
        public string CommentsFilter
        {
            get => _CommentsFilter;
            set => Set(ref _CommentsFilter, value);
        }
        #endregion

        #endregion
        #region Комманды

        #region Обновление данных на странице
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p) => TableSource = DBConnectHelper.DbObj.Storage_MTR.ToList();
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion

        #endregion

        public StoragesPageViewModel()
        {
            #region Комманды
            //ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            #endregion
            TableSource = DBConnectHelper.DbObj.Storage_MTR.ToList();
        }
    }
}
