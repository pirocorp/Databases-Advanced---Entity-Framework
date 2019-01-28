using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShopSystem.Data.Migrations
{
    public partial class RelatedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedBooks",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    RelatedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedBooks", x => new { x.BookId, x.RelatedId });
                    table.ForeignKey(
                        name: "FK_RelatedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedBooks_Books_RelatedId",
                        column: x => x.RelatedId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedBooks_RelatedId",
                table: "RelatedBooks",
                column: "RelatedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedBooks");
        }
    }
}
