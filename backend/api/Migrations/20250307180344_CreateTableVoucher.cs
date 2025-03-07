using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                name: "vouchers");
        }
    }
}
