using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UpaProject.ViewModels;

namespace UpaProjectTests
{
    [TestClass]
    public class AddMTRPageTests
    {
        [TestMethod]
        public void CanOnAddNewMTRExecuted_IdSapIsNull_false()
        {
            AddMTRPageViewModel addMTRPageVM = new AddMTRPageViewModel
            {
                NameMTR = "Unit-Tests",
                IdSap = ""
            };
            Assert.IsFalse(addMTRPageVM.AddNewMTR.CanExecute(null));
        }
        [TestMethod]
        public void CanOnAddNewMTRExecuted_NameMTRIsNull_false()
        {
            AddMTRPageViewModel addMTRPageVM = new AddMTRPageViewModel
            {
                NameMTR = "",
                IdSap = "123"
            };
            Assert.IsFalse(addMTRPageVM.AddNewMTR.CanExecute(null));
        }
        [TestMethod]
        public void CanOnAddNewMTRExecuted_IdSapAndNameMTRIsNotNull_true()
        {
            AddMTRPageViewModel addMTRPageVM = new AddMTRPageViewModel
            {
                NameMTR = "Unit-Tests",
                IdSap = "123"
            };
            Assert.IsTrue(addMTRPageVM.AddNewMTR.CanExecute(null));
        }
    }
}
