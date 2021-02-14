using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
   internal class StoragesPageViewModel:ViewModel
    {
        private IEnumerable<Storage_MTR> _Source;
        public IEnumerable<Storage_MTR> Source
        {
            get => _Source;
            set=>Set(ref _Source,value);
        }

        public StoragesPageViewModel()
        {


        }
    }
}
