
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
    
public partial class Equipments
{

    public int IdEq { get; set; }

    public Nullable<int> IdAsuEq { get; set; }

    public int IdEqList { get; set; }



    public virtual AsuEq AsuEq { get; set; }

    public virtual EqList EqList { get; set; }

}

}