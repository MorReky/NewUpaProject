using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    internal class HistoriesPageViewModel:ViewModel
    {
        #region Свойства
        
        public IEnumerable<HistoryLoginUsers> historyLoginUsers
        {
            //Внести правки. Пусть возвращает последние 20 элементов
            get=> DBConnectHelper.DbObj.HistoryLoginUsers.ToList();
        }
        public IEnumerable<HistoryMTR> historyMTR
        { 
            get => DBConnectHelper.DbObj.HistoryMTR.ToList(); 
        }
        public IEnumerable<HistoryStorages> historyStorages
        {
            get => DBConnectHelper.DbObj.HistoryStorages.ToList();
        }
        #endregion
        #region Комманды 

        #endregion
        public HistoriesPageViewModel()
        {

        }
    }
}
