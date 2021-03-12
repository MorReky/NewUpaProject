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
    internal class MTRCatalogsPageViewModel:ViewModel
    {
        #region Свойства
        public IEnumerable<MTR> TableSource { get => DBConnectHelper.DbObj.MTR.ToList(); set => DBConnectHelper.DbObj.SaveChanges(); }
        #endregion

        #region Комманды
        #endregion

        public MTRCatalogsPageViewModel()
        {
            #region Комманды
            #endregion
        }
    }
}
