using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevelopersHub.Models
{
    public partial class DevelopersHubContext : DbContext
    {
        public virtual DbSet<TblForums> TblForums { get; set; }
        public virtual DbSet<TblForumsThreads> TblForumsThreads { get; set; }
        public virtual DbSet<TblMembers> TblMembers { get; set; }
        public virtual DbSet<TblProposals> TblProposals { get; set; }
        public virtual DbSet<TblSkills> TblSkills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-P6C0M7BT\MSSQL2014;Database=DevelopersHub;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblForums>(entity =>
            {
                entity.ToTable("Tbl_Forums");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.M)
                    .WithMany(p => p.TblForums)
                    .HasForeignKey(d => d.Mid)
                    .HasConstraintName("FK_Tbl_Forums_Tbl_Members");
            });

            modelBuilder.Entity<TblForumsThreads>(entity =>
            {
                entity.ToTable("Tbl_ForumsThreads");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adddate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Ftid).HasColumnName("ftid");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.F)
                    .WithMany(p => p.TblForumsThreads)
                    .HasForeignKey(d => d.Fid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_ForumsThreads_Tbl_Forums");

                entity.HasOne(d => d.Ft)
                    .WithMany(p => p.InverseFt)
                    .HasForeignKey(d => d.Ftid)
                    .HasConstraintName("FK_Tbl_ForumsThreads_Tbl_ForumsThreads");

                entity.HasOne(d => d.M)
                    .WithMany(p => p.TblForumsThreads)
                    .HasForeignKey(d => d.Mid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_ForumsThreads_Tbl_Members");
            });

            modelBuilder.Entity<TblMembers>(entity =>
            {
                entity.ToTable("Tbl_Members");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bdate)
                    .HasColumnName("BDate")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Experience).HasColumnType("varchar(50)");

                entity.Property(e => e.Fname)
                    .HasColumnName("FName")
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).HasColumnType("varchar(50)");

                entity.Property(e => e.Sname)
                    .HasColumnName("SName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblProposals>(entity =>
            {
                entity.ToTable("Tbl_Proposals");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.SnapshotFile).HasColumnType("varchar(50)");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.M)
                    .WithMany(p => p.TblProposals)
                    .HasForeignKey(d => d.Mid)
                    .HasConstraintName("FK_Tbl_Proposals_Tbl_Members");
            });

            modelBuilder.Entity<TblSkills>(entity =>
            {
                entity.ToTable("Tbl_Skills");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Level).HasMaxLength(50);

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.M)
                    .WithMany(p => p.TblSkills)
                    .HasForeignKey(d => d.Mid)
                    .HasConstraintName("FK_Tbl_Skills_Tbl_Members");
            });
        }
    }
}