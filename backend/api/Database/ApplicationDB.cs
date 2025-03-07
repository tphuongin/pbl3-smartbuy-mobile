using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDB: DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options): base(options)
        {
        }
        public DbSet<Voucher> Vouchers{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            Table vouchers
            */
            modelBuilder.Entity<Voucher>().ToTable("vouchers");

            modelBuilder.Entity<Voucher>().Property(v => v.Id)
                .HasColumnName("voucher_id")
                .HasColumnType("INT")
                .IsRequired();

            modelBuilder.Entity<Voucher>().Property(v => v.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            modelBuilder.Entity<Voucher>().Property(v => v.DiscountPercentage)
                .HasColumnName("discount_percentage")
                .HasColumnType("DECIMAL(5, 2)")
                .IsRequired(false);

            modelBuilder.Entity<Voucher>().Property(v => v.DiscountAmount)
                .HasColumnName("discount_amount")
                .HasColumnType("DECIMAL(10, 2)")
                .IsRequired(false);

            modelBuilder.Entity<Voucher>().Property(v => v.Code)
                .HasColumnName("code")
                .HasColumnType("VARCHAR(50)")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Voucher>().Property(v => v.MinOrderValue)
                .HasColumnName("min_order_value")
                .HasColumnType("DECIMAL(10, 2)")
                .IsRequired(false);

            modelBuilder.Entity<Voucher>().Property(v => v.MaxDiscountAmount)
                .HasColumnName("max_discount_amount")
                .HasColumnType("DECIMAL(10, 2)")
                .IsRequired(false);

            modelBuilder.Entity<Voucher>().Property(v => v.MaxUses)
                .HasColumnName("max_uses")
                .HasColumnType("INT")
                .IsRequired(false);

            modelBuilder.Entity<Voucher>().Property(v => v.UsedCount)
                .HasColumnName("used_count")
                .HasColumnType("INT")
                .IsRequired()
                .HasDefaultValue(0);

            modelBuilder.Entity<Voucher>().Property(v => v.Status)
                .HasColumnName("status")
                .HasColumnType("VARCHAR(10)")
                .IsRequired();

            modelBuilder.Entity<Voucher>().Property(v => v.StartDate)
                .HasColumnName("start_date")
                .HasColumnType("DATE")
                .IsRequired();

            modelBuilder.Entity<Voucher>().Property(v => v.EndDate)
                .HasColumnName("end_date")
                .HasColumnType("DATE")
                .IsRequired();

            modelBuilder.Entity<Voucher>().HasKey(v => v.Id);

            modelBuilder.Entity<Voucher>().ToTable(t =>
            {
                t.HasCheckConstraint("ck_vouchers_status", "status IN ('active', 'inactive')");
                t.HasCheckConstraint("ck_vouchers_discount", 
                    "discount_percentage IS NOT NULL OR discount_amount IS NOT NULL");
                t.HasCheckConstraint("ck_vouchers_max_uses", 
                    "max_uses IS NULL OR max_uses >= used_count");
                t.HasCheckConstraint("ck_vouchers_dates", 
                    "start_date <= end_date");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}