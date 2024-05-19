using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBudgeteer.Extensions.MetaData.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnIsoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency_ISO_Code",
                table: "AccountDetail",
                newName: "Currency_IsoCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency_IsoCode",
                table: "AccountDetail",
                newName: "Currency_ISO_Code");
        }
    }
}
