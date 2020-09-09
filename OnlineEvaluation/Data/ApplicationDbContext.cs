using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineEvaluation.Models;

namespace OnlineEvaluation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvaluationType> EvaluationType { get; set; }
        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<EvaluationSubject> EvaluationSubject { get; set; }
        public DbSet<Response> Response { get; set; }
        // public DbSet<CEO> CEO { get; set; }
        // public DbSet<Chairperson> Chairperson { get; set; }
        // public DbSet<Director> Director { get; set; }
        public DbSet<StateCorporation> StateCorporation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.Property(e => e.RespondOtions).IsRequired().
                    HasConversion(
                        x => JsonConvert.SerializeObject(x),
                        v => v == null ? new Dictionary<int, string>() : JsonConvert.DeserializeObject<Dictionary<int, string>>(v));

            });
            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.HasMany(p => p.Responses)
               .WithOne(t => t.question)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            });
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.HasMany(p => p.EvaluationSubjects)
               .WithOne(t => t.Chairperson)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            });
        }
    }
}
