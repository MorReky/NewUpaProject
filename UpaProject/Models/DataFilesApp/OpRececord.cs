//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpaProject.Models.DataFilesApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpRececord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpRececord()
        {
            this.HistroyOpRecord = new HashSet<HistroyOpRecord>();
            this.OpShifts_OpRecord = new HashSet<OpShifts_OpRecord>();
            this.Realizers = new HashSet<Realizers>();
        }
    
        public int IDOpRecord { get; set; }
        public System.DateTime DateOccurence { get; set; }
        public System.TimeSpan TimeOccurence { get; set; }
        public System.TimeSpan TimeSolution { get; set; }
        public int IdDepartment { get; set; }
        public int Criticality { get; set; }
        public int IdPlace { get; set; }
        public string Occurence { get; set; }
        public string Account { get; set; }
        public string Solution { get; set; }
        public string Comments { get; set; }
    
        public virtual Departments Departments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistroyOpRecord> HistroyOpRecord { get; set; }
        public virtual Place Place { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpShifts_OpRecord> OpShifts_OpRecord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Realizers> Realizers { get; set; }
    }
}