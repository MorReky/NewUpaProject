using System;
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
    class NewMTRPageViewModel : ViewModel
    {
        #region Свойства
        private IEnumerable<MTR> _MTRs;
        public IEnumerable<MTR> MTRs
        {
            get => _MTRs;
            set => Set(ref _MTRs, value);
        }
        private MTR _SapIdMTR;
        public MTR SapIdMTR
        {
            get => _SapIdMTR;
            set
            {      
                Set(ref _SapIdMTR, value);
                if (_SapIdMTR!= null)
                {
                    NameMTR = SapIdMTR.Name;
                    UnitMTR = SapIdMTR.Unit;
                }
                else
                {
                    NameMTR = "";
                    UnitMTR = "";
                }
            }
        }
        private string _NameMTR;
        public string NameMTR
        {
            get => _NameMTR;
            set => Set(ref _NameMTR, value);
        }
        private string _UnitMTR;
        public string UnitMTR
        {
            get => _UnitMTR;
            set => Set(ref _UnitMTR, value);
        }
        private string _QuantityMTR;
        public string QuantityMTR
        {
            get => _QuantityMTR;
            set => Set(ref _QuantityMTR, value);
        }

        #endregion
        
        #region Комманды
        #endregion

        public NewMTRPageViewModel()
        {
            #region Комманды
           
            #endregion
            MTRs = DBConnectHelper.DbObj.MTR.OrderBy(x => x.IdSap).ToList();
        }
        public void SelectMTR()
        {

        }
    }
}
