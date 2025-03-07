using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Voucher> Vouchers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            Table users
            */
            // Entity là đối tượng đại diện cho bảng trong cơ sở dữ liệu
            // ToTable("tên bảng") là phương thức chỉ định tên bảng trong cơ sở dữ liệu
            modelBuilder.Entity<User>().ToTable("users");

            // Property là phương thức chỉ định các thuộc tính của bảng
            // HasColumnName("tên cột") là phương thức chỉ định tên cột trong bảng
            // HasColumnType("kiểu dữ liệu") là phương thức chỉ định kiểu dữ liệu của cột
            // IsRequired() là phương thức chỉ định cột có bắt buộc hay không
            // HasMaxLength(độ dài tối đa) là phương thức chỉ định độ dài tối đa của cột
            modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("user_id").HasColumnType("VARCHAR(36)").IsRequired(true).HasMaxLength(36);
            modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name").HasColumnType("VARCHAR(100)").IsRequired(true).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Gender).HasColumnName("gender").HasColumnType("VARCHAR(10)").IsRequired(true).HasMaxLength(10);
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email").HasColumnType("VARCHAR(100)").IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password").HasColumnType("VARCHAR(60)").IsRequired().HasMaxLength(60);
            modelBuilder.Entity<User>().Property(u => u.Phone).HasColumnName("phone").HasColumnType("VARCHAR(10)").IsRequired(false).HasMaxLength(10);
            modelBuilder.Entity<User>().Property(u => u.Address).HasColumnName("address").HasColumnType("VARCHAR(255)").IsRequired(false).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(u => u.Role).HasColumnName("role").HasColumnType("VARCHAR(10)").IsRequired(true).HasMaxLength(10);
            modelBuilder.Entity<User>().Property(u => u.Avatar).HasColumnName("avatar").HasColumnType("BLOB").IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasColumnName("created_at").HasColumnType("TIMESTAMP").IsRequired(true);

            // HasKey(u => u.UserId) là phương thức chỉ định khóa chính của bảng là cột user_id
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            // HasCheckConstraint("tên ràng buộc", "điều kiện") là phương thức chỉ định ràng buộc kiểm tra cho bảng
            modelBuilder.Entity<User>().ToTable(t =>
            {
                t.HasCheckConstraint("ck_users_role", "role IN ('admin', 'user')");
                t.HasCheckConstraint("ck_users_phone", "phone IS NULL OR phone REGEXP '^[0-9]{10}$'");
                t.HasCheckConstraint("ck_users_email", "email IS NULL OR email REGEXP '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$'");
                t.HasCheckConstraint("ck_gender", "gender IN ('male','female','other')");
                t.HasCheckConstraint("ck_phone_and_email", "phone IS NOT NULL OR email IS NOT NULL");
            });

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