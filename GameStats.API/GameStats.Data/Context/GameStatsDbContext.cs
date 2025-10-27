using GameStats.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStats.Data.Context;

public class GameStatsDbContext : DbContext
{
    public GameStatsDbContext(DbContextOptions<GameStatsDbContext> options)
        : base(options)
    {
    }

    public DbSet<CODE_VALUE> CODE_VALUE { get; set; }
    public DbSet<GAME> GAME { get; set; }
    public DbSet<MAP> MAP { get; set; }
    public DbSet<MATCH> MATCH { get; set; }
    public DbSet<MATCH_TEAM> MATCH_TEAM { get; set; }
    public DbSet<MATCH_PLAYER> MATCH_PLAYER { get; set; }
    public DbSet<MATCH_TYPE> MATCH_TYPE { get; set; }
    public DbSet<PLAYER> PLAYER { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CODE_VALUE>(entity =>
        {
            entity.HasKey(e => e.CODE_TABLE_ID);
            entity.Property(e => e.CODE_TABLE_ID)
                .ValueGeneratedNever();

            entity.Property(e => e.CODE_VALUE_ID)
                .ValueGeneratedNever();

            entity.Property(e => e.DECODE_TXT)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.EXTRA_INFO)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<GAME>(entity =>
        {
            entity.HasKey(e => e.GAME_ID);
            entity.Property(e => e.GAME_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.GAME_NAME)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<MAP>(entity =>
        {
            entity.HasKey(e => e.MAP_ID);
            entity.Property(e => e.MAP_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.MAP_NAME)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(m => m.GAME)
                .WithMany(g => g.MAPS)
                .HasForeignKey(e => e.GAME_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PLAYER>(entity =>
        {
            entity.HasKey(e => e.PLAYER_ID);
            entity.Property(e => e.PLAYER_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.PLAYER_NAME)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(m => m.GAME)
                .WithMany(g => g.PLAYERS)
                .HasForeignKey(e => e.GAME_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MATCH>(entity =>
        {
            entity.HasKey(e => e.MATCH_ID);
            entity.Property(e => e.MATCH_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.OLD_MATCH_ID).IsRequired();
            entity.Property(e => e.MATCH_NAME).IsRequired()
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.GAME_ID).IsRequired();
            entity.Property(e => e.TYPE_CD).IsRequired();
            entity.Property(e => e.MAP_ID).IsRequired();

            entity.HasOne(m => m.GAME)
                .WithMany(g => g.MATCHES)
                .HasForeignKey(e => e.GAME_ID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.MAP)
                .WithMany(g => g.MATCHES)
                .HasForeignKey(e => e.MAP_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MATCH_TEAM>(entity =>
        {
            entity.HasKey(e => e.MATCH_TEAM_ID);
            entity.Property(e => e.MATCH_TEAM_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.MATCH_ID).IsRequired();
            entity.Property(e => e.TEAM_COLOR)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(m => m.MATCH)
                .WithMany(m => m.MATCH_TEAM)
                .HasForeignKey(e => e.MATCH_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MATCH_PLAYER>(entity =>
        {
            entity.HasKey(e => e.MATCH_PLAYER_ID);
            entity.Property(e => e.MATCH_PLAYER_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.PLAYER_ID).IsRequired();
            entity.Property(e => e.MATCH_ID).IsRequired();

            entity.HasOne(m => m.MATCH)
                .WithMany(g => g.MATCH_PLAYER)
                .HasForeignKey(e => e.MATCH_ID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.MATCH_TEAM)
                .WithMany(g => g.MATCH_PLAYERS)
                .HasForeignKey(e => e.MATCH_TEAM_ID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.PLAYER)
                .WithMany(g => g.MATCH_PLAYERS)
                .HasForeignKey(e => e.PLAYER_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MATCH_TYPE>(entity =>
        {
            entity.HasKey(e => e.MATCH_TYPE_ID);
            entity.Property(e => e.MATCH_TYPE_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.MATCH_TYPE_NAME)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.GAME_ID).IsRequired();

            entity.HasOne(m => m.GAME)
                .WithMany(g => g.MATCH_TYPES)
                .HasForeignKey(e => e.GAME_ID)
                .OnDelete(DeleteBehavior.Cascade);
        });

       
    }
}
