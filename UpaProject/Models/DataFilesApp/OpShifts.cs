//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpaProject.Models.DataFilesApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpShifts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpShifts()
        {
            this.OpShifts_OpRecord = new HashSet<OpShifts_OpRecord>();
            this.Shifts_Persons = new HashSet<Shifts_Persons>();
        }
    
        public int IDShift { get; set; }
        public int Type { get; set; }
        public System.DateTime DateStartShift { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpShifts_OpRecord> OpShifts_OpRecord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shifts_Persons> Shifts_Persons { get; set; }
    }
}
