using Microsoft.EntityFrameworkCore;
using ServiceStation.Models.Table.IT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStation.Models.DBConnect
{
    public class ITAppcenter : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Data Source=THSDB;Initial Catalog=IT;UID=SDE_Developer;PWD=SDE_Developer;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewrpEmail>(entity =>
            {
                entity.HasKey(k => new { k.emEmpcode });
            });
        }

        public DbSet<ViewrpEmail> Emails { get; set; }
        //public DbSet<ViewUserPermission> UserPermission { get; set; }
        public DbSet<ViewPrograms> Programs { get; set; }
    }


    public class IT : DbContext
    {
        public IT(DbContextOptions<IT> options) : base(options)
        { }


        public DbSet<ViewrpEmail> rpEmails { get; set; }
        public DbSet<ViewsvsMastServiceSub> svsMastServiceSub { get; set; }
        public DbSet<ViewsvsMastServiceMain> svsMastServiceMain { get; set; }
        public DbSet<ViewsvsServiceRequest> svsServiceRequest { get; set; }



        public DbSet<ViewsvsMastFlowApprove> svsMastFlowApprove { get; set; }
        public DbSet<ViewsvsHistoryApproved> svsHistoryApproved { get; set; }
        public DbSet<ViewAttachment> Attachment { get; set; }
        public List<ViewAttachment> _listAttachment { get; set; }


        public DbSet<ViewProgramList> ProgramList { get; set; }

        //F1
        public DbSet<ViewsvsGeneral> svsGeneral { get; set; }


        //F2
        public DbSet<ViewsvsDataRestore> svsDataRestore { get; set; }
        public DbSet<ViewsvsDataRestore_FileName> svsDataRestore_FileName { get; set; }

        //F5
        public DbSet<ViewsvsVPN> svsVPN { get; set; }


        //F3
        public DbSet<ViewsvsNotebookSpare> svsNotebookSpare { get; set; }
        public DbSet<ViewsvsMastNotebookSpare> svsMastNotebookSpare { get; set; }
        public DbSet<ViewsvsBorrowNotebookSpare> svsBorrowNotebookSpare { get; set; }

        //F6
        public DbSet<ViewsvsSDE_SystemRegister> svsSDE_SystemRegister { get; set; }
        //public DbSet<ViewsvsSDE_SystemRegister> dbset_svsSDE_SystemRegister { get; set; }
        //F7
        public DbSet<ViewsvsITMS_SystemRegister> svsITMS_SystemRegister { get; set; }

        //F4
        public DbSet<ViewsvsRegisterUSB> svsRegisterUSB { get; set; }
        public DbSet<ViewsvsRegisterUSB_Cancel> svsRegisterUSB_Cancel { get; set; }
        public DbSet<ViewsvsRegisterUSB_New> svsRegisterUSB_New { get; set; }
        public DbSet<ViewsvsMastUSB> svsMastUSB { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewsvsMastServiceSub>(entity =>
            {
                entity.HasKey(k => new { k.msSecCode, k.msSubNo });
            });
            modelBuilder.Entity<ViewAttachment>(entity =>
            {
                entity.HasKey(k => new { k.fnNo, k.fnPath });
            });
            modelBuilder.Entity<ViewsvsMastFlowApprove>(entity =>
            {
                entity.HasKey(k => new { k.mfFlowNo, k.mfStep, k.mfDept });
            });
            modelBuilder.Entity<ViewsvsDataRestore_FileName>(entity =>
            {
                entity.HasKey(k => new { k.rfNo, k.rfFileNo });
            });
            modelBuilder.Entity<ViewsvsSDE_SystemRegister>(entity =>
            {
                entity.HasKey(k => new { k.sysNo, k.sysEmpCode, k.sysProgramName });
            });
            modelBuilder.Entity<ViewsvsITMS_SystemRegister>(entity =>
            {
                entity.HasKey(k => new { k.itNo, k.itEmpcode });
            });
            modelBuilder.Entity<ViewsvsRegisterUSB_Cancel>(entity =>
            {
                entity.HasKey(k => new { k.cuCancelNo, k.cuNo });
            });
            modelBuilder.Entity<ViewsvsRegisterUSB_New>(entity =>
            {
                entity.HasKey(k => new { k.nuNewNo, k.nuNo });
            });
            modelBuilder.Entity<ViewsvsBorrowNotebookSpare>(entity =>
            {
                entity.HasKey(k => new { k.bnPCName, k.bnStratDate, k.bnEndDate });
            });

        }
    }
}
