using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUserAndVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "VARCHAR(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<byte[]>(type: "BLOB", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.CheckConstraint("ck_gender", "gender IN ('male','female','other')");
                    table.CheckConstraint("ck_phone_and_email", "phone IS NOT NULL OR email IS NOT NULL");
                    table.CheckConstraint("ck_users_email", "email IS NULL OR email REGEXP '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$'");
                    table.CheckConstraint("ck_users_phone", "phone IS NULL OR phone REGEXP '^[0-9]{10}$'");
                    table.CheckConstraint("ck_users_role", "role IN ('admin', 'user')");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    voucher_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    discount_percentage = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    code = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    min_order_value = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    max_discount_amount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    max_uses = table.Column<int>(type: "INT", nullable: true),
                    used_count = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    status = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    end_date = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.voucher_id);
                    table.CheckConstraint("ck_vouchers_dates", "start_date <= end_date");
                    table.CheckConstraint("ck_vouchers_discount", "discount_percentage IS NOT NULL OR discount_amount IS NOT NULL");
                    table.CheckConstraint("ck_vouchers_max_uses", "max_uses IS NULL OR max_uses >= used_count");
                    table.CheckConstraint("ck_vouchers_status", "status IN ('active', 'inactive')");
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vouchers");
        }
    }
}
