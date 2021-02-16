using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Infrastracture.ClassHelper
{
    /// <summary>
    /// Вспомогательный класс для хранения набора правил для выбранной роли
    /// </summary>
    class ClassRulesHelper
    {
        public static IEnumerable<RulesForRole> rulesForRole;

        public ClassRulesHelper()
        {
            rulesForRole = DBConnectHelper.DbObj.RulesForRole.Where(x=>x.Role==ClassUserHelper.Role).ToList();
        }
    }
}
