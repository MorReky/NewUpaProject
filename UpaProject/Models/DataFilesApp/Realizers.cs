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
    
    public partial class Realizers
    {
        public int ID { get; set; }
        public int IdOpRecord { get; set; }
        public int IdPerson { get; set; }
    
        public virtual OpRececord OpRececord { get; set; }
        public virtual Persons Persons { get; set; }
    }
}
