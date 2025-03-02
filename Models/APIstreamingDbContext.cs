using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIStreaming.Models;

public partial class ApistreamingDbContext : DbContext
{
    public ApistreamingDbContext()
    {
    }

    public ApistreamingDbContext(DbContextOptions<ApistreamingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contenido> Contenidos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Suscripcione> Suscripciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaContenidoConDuracion> VistaContenidoConDuracions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = .\\SQLEXPRESS;Initial Catalog=APIstreamingDB; Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contenido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CONTENID__3214EC07C33B83FA");

            entity.ToTable("CONTENIDO");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(50);

            entity.HasOne(d => d.Plan).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__CONTENIDO__PlanI__14270015");

            entity.HasOne(d => d.Suscripcion).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.SuscripcionId)
                .HasConstraintName("FK__CONTENIDO__Suscr__59063A47");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PAGOS__3214EC07AEE497DF");

            entity.ToTable("PAGOS");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Suscripcion).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.SuscripcionId)
                .HasConstraintName("FK__PAGOS__Suscripci__534D60F1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__PAGOS__UsuarioId__5441852A");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PLANES__3214EC073CBF4B8D");

            entity.ToTable("PLANES");

            entity.Property(e => e.Descargas).HasMaxLength(20);
            entity.Property(e => e.Exclusividad).HasMaxLength(20);
            entity.Property(e => e.NombrePlan).HasMaxLength(50);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Suscripcione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SUSCRIPC__3214EC0725CBF090");

            entity.ToTable("SUSCRIPCIONES");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaFinalizacion)
                .HasComputedColumnSql("(dateadd(day,(30),[FechaInicio]))", false)
                .HasColumnType("datetime");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Plan).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__SUSCRIPCI__PlanI__5070F446");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__SUSCRIPCI__Usuar__4D94879B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USUARIOS__3214EC0754FA63F4");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Contra).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.RefreshTokenExpiration)
                .HasColumnType("datetime")
                .HasColumnName("refreshTokenExpiration");
            entity.Property(e => e.ResetToken).HasMaxLength(100);
            entity.Property(e => e.ResetTokenExpiration).HasColumnType("datetime");
            entity.Property(e => e.Rol).HasMaxLength(50);

            entity.HasOne(d => d.Plan).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__USUARIOS__PlanId__2739D489");
        });

        modelBuilder.Entity<VistaContenidoConDuracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaContenidoConDuracion");

            entity.Property(e => e.DuracionFormato)
                .HasMaxLength(41)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
