using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace MisaWebApi.Models
{
    public partial class AmisContext : DbContext
    {
        public AmisContext()
        {
        }

        public AmisContext(DbContextOptions<AmisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FieldData> FieldData { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Phase> Phase { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersHasPhase> UsersHasPhase { get; set; }
        public virtual DbSet<UsersHasProcess> UsersHasProcess { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=amisprocess.cli3j8mtdaga.ap-northeast-1.rds.amazonaws.com;port=3306;user=nguyetmoon;password=nguyetmoon180499;database=Misa.amis;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FieldData>(entity =>
            {
                entity.ToTable("fieldData");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PhaseId)
                    .HasName("fk_fieldData_phase1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasColumnName("fieldName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhaseId)
                    .IsRequired()
                    .HasColumnName("phase_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Required)
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.FieldData)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fieldData_phase1");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.HasIndex(e => e.FieldDataId)
                    .HasName("fk_option_fieldData1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.FieldDataId)
                    .IsRequired()
                    .HasColumnName("fieldData_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FieldData)
                    .WithMany(p => p.Option)
                    .HasForeignKey(d => d.FieldDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_option_fieldData1");
            });

            modelBuilder.Entity<Phase>(entity =>
            {
                entity.ToTable("phase");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProcessId)
                    .HasName("fk_phase_Process1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasColumnName("icon")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsFirstPhase)
                    .HasColumnName("isFirstPhase")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IsTb)
                    .HasColumnName("isTB")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IsTc)
                    .HasColumnName("isTC")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.LimitUser)
                    .HasColumnName("limitUser")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.PhaseName)
                    .IsRequired()
                    .HasColumnName("phaseName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasColumnName("Process_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.Phase)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_phase_Process1");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("date");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedAt)
                    .HasColumnName("modifiedAt")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NameProcess)
                    .IsRequired()
                    .HasColumnName("nameProcess")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("passwordHash")
                    .HasMaxLength(45)
                    .IsUnicode(false);
                entity.Property(e => e.PasswordSalt)
                 .IsRequired()
                 .HasColumnName("passwordSalt")
                 .HasMaxLength(45)
                 .IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersHasPhase>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.PhaseId })
                    .HasName("PRIMARY");

                entity.ToTable("users_has_phase");

                entity.HasIndex(e => e.PhaseId)
                    .HasName("fk_users_has_phase_phase1_idx");

                entity.HasIndex(e => e.UsersId)
                    .HasName("fk_users_has_phase_users1_idx");

                entity.Property(e => e.UsersId)
                    .HasColumnName("users_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.PhaseId)
                    .HasColumnName("phase_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.UsersHasPhase)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_has_phase_phase1");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UsersHasPhase)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_has_phase_users1");
            });

            modelBuilder.Entity<UsersHasProcess>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.ProcessId })
                    .HasName("PRIMARY");

                entity.ToTable("users_has_Process");

                entity.HasIndex(e => e.ProcessId)
                    .HasName("fk_users_has_Process_Process1_idx");

                entity.HasIndex(e => e.UsersId)
                    .HasName("fk_users_has_Process_users1_idx");

                entity.Property(e => e.UsersId)
                    .HasColumnName("users_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessId)
                    .HasColumnName("Process_id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.UsersHasProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_has_Process_Process1");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UsersHasProcess)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_has_Process_users1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
