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
        public virtual DbSet<HistoryLoginUsers> HistoryLoginUsers { get; set; }
        public virtual DbSet<HistoryMTR> HistoryMTR { get; set; }
        public virtual DbSet<HistoryStorages> HistoryStorages { get; set; }
        public virtual DbSet<HistroyOpRecord> HistroyOpRecord { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<MTR> MTR { get; set; }
        public virtual DbSet<MTR_Images> MTR_Images { get; set; }
        public virtual DbSet<OpRececord> OpRececord { get; set; }
        public virtual DbSet<OpShifts> OpShifts { get; set; }
        public virtual DbSet<OpShifts_OpRecord> OpShifts_OpRecord { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PromActivites> PromActivites { get; set; }
        public virtual DbSet<Realizers> Realizers { get; set; }
        public virtual DbSet<Shifts_Persons> Shifts_Persons { get; set; }
        public virtual DbSet<Storage_MTR> Storage_MTR { get; set; }
        public virtual DbSet<Storages> Storages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemsAsu> SystemsAsu { get; set; }
        public virtual DbSet<SystemsAsu_Tags> SystemsAsu_Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
