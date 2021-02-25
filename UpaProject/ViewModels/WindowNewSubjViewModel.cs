using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    class WindowNewSubjViewModel:ViewModel
    {

        #region Свойства
        private IEnumerable<Place> _TagsData;
        public IEnumerable<Place> TagsData { get => _TagsData; set => Set(ref _TagsData, value); }
        #endregion
        #region Комманды
        #endregion
        WindowNewSubjViewModel()
        {

        }
    }
}
