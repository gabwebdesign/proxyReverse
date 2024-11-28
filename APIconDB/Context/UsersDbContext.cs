using System;
using System.Collections.Generic;
using APIconDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIconDB.Context;

public partial class UsersDbContext : DbContext
{
    public UsersDbContext()
    {
    }

    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
      //  => optionsBuilder.UseSqlite("Data Source=identifier.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity => entity.ToTable("users"));
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    public DbSet<APIconDB.Entities.Users> Users { get; set; } = default!;
}
