//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpaProject.DataFilesApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemsOfEq
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemsOfEq()
        {
            this.AsuMainEq = new HashSet<AsuMainEq>();
        }
    
        public int IdItemsOfEq { get; set; }
        public string UnitEq { get; set; }
        public string TechObjName { get; set; }
        public Nullable<int> IdDepartment { get; set; }
        public string GroupOfPlanners { get; set; }
        public string FactoryLocationTechEq { get; set; }
        public string ResponsibleWorkplace { get; set; }
        public string EqUnitType { get; set; }
        public string CodeABC { get; set; }
        public string TechPlace { get; set; }
        public string TechPlaceName { get; set; }
        public string UseStatus { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<System.DateTime> ExplStartDate { get; set; }
        public string MVZ { get; set; }
        public string PrymaryMeans { get; set; }
        public string InventoryNum { get; set; }
        public string SysStatus { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }
        public string LastWriter { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsuMainEq> AsuMainEq { get; set; }
        public virtual DepartmentEq DepartmentEq { get; set; }
    }
}
