using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddedNavPropertiesToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                schema: "Catalog",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_LanguageId",
                schema: "Catalog",
                table: "Movies",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                schema: "Catalog",
                table: "Movies",
                column: "GenreId",
                principalSchema: "Catalog",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "Catalog",
                table: "Movies",
                column: "LanguageId",
                principalSchema: "Catalog",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                schema: "Catalog",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "Catalog",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                schema: "Catalog",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_LanguageId",
                schema: "Catalog",
                table: "Movies");
        }
    }
}
