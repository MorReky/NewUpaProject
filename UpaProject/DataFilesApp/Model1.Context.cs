﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UPAOborotEntities : DbContext
    {
        public UPAOborotEntities()
            : base("name=UPAOborotEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AsuEq> AsuEq { get; set; }
        public virtual DbSet<AsuMainEq> AsuMainEq { get; set; }
        public virtual DbSet<DepartmentEq> DepartmentEq { get; set; }
        public virtual DbSet<Engineers> Engineers { get; set; }
        public virtual DbSet<EqList> EqList { get; set; }
        public virtual DbSet<EqType> EqType { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<ItemsOfEq> ItemsOfEq { get; set; }
        public virtual DbSet<OpLogJournal> OpLogJournal { get; set; }
        public virtual DbSet<Repairmens> Repairmens { get; set; }
        public virtual DbSet<Shifts> Shifts { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
