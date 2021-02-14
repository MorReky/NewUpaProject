using System;
using System.Linq;
using System.Windows.Controls;
using UpaProject.DataFilesApp;

namespace UpaProject.Journals
{
    /// <summary>
    /// Логика взаимодействия для PageOpJournal.xaml
    /// </summary>
    public partial class PageOpJournal : Page
    {
        public PageOpJournal()
        {
            InitializeComponent();
        }

        private void UpdateContext(object sender, EventArgs e)
        {
            GridList.ItemsSource = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
        }

    }
}
