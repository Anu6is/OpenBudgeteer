using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBudgeteer.Extensions.Data.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BucketGroup",
                columns: ["BucketGroupId", "Name", "Position"],
                values: [Guid.Parse("00000000-0000-0000-0000-000000000001"), "System", 0]);

            migrationBuilder.InsertData(
                table: "Bucket",
                columns: ["BucketId", "Name", "BucketGroupId", "ValidFrom", "IsInactive", "IsInactiveFrom"],
                values: [Guid.Parse("00000000-0000-0000-0000-000000000001"), "Income", Guid.Parse("00000000-0000-0000-0000-000000000001"), "1990-01-01", false, $"{DateTime.MaxValue:yyyy-MM-dd}"]);
            
            migrationBuilder.InsertData(
                table: "Bucket",
                columns: ["BucketId", "Name", "BucketGroupId", "ValidFrom", "IsInactive", "IsInactiveFrom"],
                values: [Guid.Parse("00000000-0000-0000-0000-000000000002"), "Transfer", Guid.Parse("00000000-0000-0000-0000-000000000001"), "1990-01-01", false, $"{DateTime.MaxValue:yyyy-MM-dd}"]);

            migrationBuilder.InsertData(
                table: "BucketVersion",
                columns:
                [
                    "BucketVersionId", "BucketId",
                    "Version", "BucketType", "BucketTypeXParam", "BucketTypeYParam",
                    "BucketTypeZParam", "Notes", "ValidFrom"
                ],
                values:
                [
                    Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    1, 1, 0, 0,
                    "1990-01-01", null, "1990-01-01"
                ]);

            migrationBuilder.InsertData(
                table: "BucketVersion",
                columns:
                [
                    "BucketVersionId", "BucketId",
                    "Version", "BucketType", "BucketTypeXParam", "BucketTypeYParam",
                    "BucketTypeZParam", "Notes", "ValidFrom"
                ],
                values:
                [
                    Guid.Parse("00000000-0000-0000-0000-000000000002"), Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    1, 1, 0, 0,
                    "1990-01-01", null, "1990-01-01"
                ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BucketGroup",
                keyColumn: "BucketGroupId",
                keyValue: Guid.Parse("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Bucket",
                keyColumn: "BucketId",
                keyValue: Guid.Parse("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Bucket",
                keyColumn: "BucketId",
                keyValue: Guid.Parse("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "BucketVersion",
                keyColumn: "BucketId",
                keyValue: Guid.Parse("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "BucketVersion",
                keyColumn: "BucketId",
                keyValue: Guid.Parse("00000000-0000-0000-0000-000000000002"));
        }
    }
}
