﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UpaDBEntities : DbContext
    {
        public UpaDBEntities()
            : base("name=UpaDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<MTR> MTR { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PromActivites> PromActivites { get; set; }
        public virtual DbSet<Storages> Storages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemsAsu> SystemsAsu { get; set; }
        public virtual DbSet<Storage_MTR> Storage_MTR { get; set; }
    }
}
