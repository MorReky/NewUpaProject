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
    
    public partial class SystemsAsu_Tags
    {
        public int ID { get; set; }
        public int IDSystemAsu { get; set; }
        public int IdPlace { get; set; }
    
        public virtual Place Place { get; set; }
        public virtual SystemsAsu SystemsAsu { get; set; }
    }
}
