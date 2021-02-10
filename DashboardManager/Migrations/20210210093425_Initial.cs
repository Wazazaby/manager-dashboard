using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DashboardManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commercial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CatchmentArea = table.Column<string>(nullable: true),
                    NbQuotes = table.Column<int>(nullable: false),
                    NbContracts = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DepartementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commercial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commercial_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DepartementId = table.Column<int>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CommercialId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Commercial_CommercialId",
                        column: x => x.CommercialId,
                        principalTable: "Commercial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Client_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_CommercialId",
                table: "Client",
                column: "CommercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_DepartementId",
                table: "Client",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Commercial_DepartementId",
                table: "Commercial",
                column: "DepartementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Commercial");

            migrationBuilder.DropTable(
                name: "Departement");
        }
    }
}
