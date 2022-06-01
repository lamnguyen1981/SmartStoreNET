﻿using System;
using System.Data.Entity;
using CC.Plugins.Core.Data.Mapping;
using CC.Plugins.Core.Domain;
using SmartStore.Core;
using SmartStore.Data;

namespace CC.Plugins.Program.Data
{

    /// <summary>
    /// Object context
    /// </summary>
    public class ProgramContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_Program";

        

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public ProgramContext()
            : base()
        {
           
        }
        

        public ProgramContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        
        }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new CCProgramMapping());
           
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<CCBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOnUtc = DateTime.UtcNow;                       
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOnUtc = DateTime.UtcNow;                        
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
