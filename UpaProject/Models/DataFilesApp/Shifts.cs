
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
    
public partial class Shifts
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Shifts()
    {

        this.OpLogJournal = new HashSet<OpLogJournal>();

    }


    public int IdShift { get; set; }

    public Nullable<System.DateTime> DateStartShift { get; set; }

    public Nullable<System.DateTime> DateEndShift { get; set; }

    public Nullable<int> DutyEngKIP { get; set; }

    public Nullable<int> DutyEngASU { get; set; }

    public Nullable<int> DutyRep1 { get; set; }

    public Nullable<int> DutyRep2 { get; set; }

    public Nullable<int> DutyRep3 { get; set; }



    public virtual Engineers Engineers { get; set; }

    public virtual Engineers Engineers1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<OpLogJournal> OpLogJournal { get; set; }

    public virtual Repairmens Repairmens { get; set; }

    public virtual Repairmens Repairmens1 { get; set; }

    public virtual Repairmens Repairmens2 { get; set; }

}

}
