using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SagasDemo.Infrastructure.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sagasdemo");

            migrationBuilder.CreateTable(
                name: "PaymentInstance",
                schema: "sagasdemo",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    CurrentState = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInstance", x => x.CorrelationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInstance",
                schema: "sagasdemo");
        }
    }
}
