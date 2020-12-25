
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
    
public partial class EqList
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public EqList()
    {

        this.Equipments = new HashSet<Equipments>();

    }


    public int IdEqList { get; set; }

    public Nullable<int> IdEqType { get; set; }

    public string GlobalId { get; set; }

    public string NameAbbreviated { get; set; }

    public string FullName { get; set; }

    public string BaseUnit { get; set; }

    public string NameASU_MTR { get; set; }

    public string BrandAndSize { get; set; }

    public string CatalogNumberAndGost { get; set; }

    public string MaterialGrade { get; set; }

    public string DrawingNumber { get; set; }

    public string TechnicalSpecifications { get; set; }

    public string Equipment { get; set; }

    public string TypeMTRName { get; set; }

    public string ManufacturerGlobalIentifier { get; set; }

    public string ManufacturerName { get; set; }

    public string MTPClassClassCode { get; set; }

    public string MTPClassClassName { get; set; }

    public string Comments { get; set; }

    public Nullable<bool> OperationOfEquipmentOF { get; set; }

    public Nullable<bool> OperationOfEquipmentMPP { get; set; }

    public string Manual { get; set; }



    public virtual EqType EqType { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Equipments> Equipments { get; set; }

}

}
