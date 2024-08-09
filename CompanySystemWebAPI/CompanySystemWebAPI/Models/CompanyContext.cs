using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departement> Departements { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Workson> Worksons { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departement>(entity =>
        {
            entity.HasKey(e => e.Deptno).HasName("departements_pkey");

            entity.HasOne(d => d.MgrempnoNavigation).WithOne(p => p.Departement).HasConstraintName("fk_employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Empno).HasName("employees_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Employees).HasConstraintName("fk_departement");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Projno).HasName("projects_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Projects).HasConstraintName("fk_departement");
        });

        modelBuilder.Entity<Workson>(entity =>
        {
            entity.HasKey(e => new { e.Empno, e.Projno }).HasName("worksons_pkey");

            entity.HasOne(d => d.EmpnoNavigation).WithMany(p => p.Worksons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("worksons_empno_fkey");

            entity.HasOne(d => d.ProjnoNavigation).WithMany(p => p.Worksons).HasConstraintName("fk_project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
