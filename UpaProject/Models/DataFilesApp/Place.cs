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
    
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            this.OpRececord = new HashSet<OpRececord>();
            this.Place_MTR = new HashSet<Place_MTR>();
        }
    
        public int IDPlace { get; set; }
        public Nullable<int> IdDepartment { get; set; }
        public Nullable<int> IdSystemAsu { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
    
        public virtual Departments Departments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpRececord> OpRececord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place_MTR> Place_MTR { get; set; }
        public virtual SystemsAsu SystemsAsu { get; set; }
    }
}
