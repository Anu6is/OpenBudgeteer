﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBudgeteer.Extensions.Data.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "BucketGroup",
                columns: table => new
                {
                    BucketGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketGroup", x => x.BucketGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetail",
                columns: table => new
                {
                    AccountDetailId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountType = table.Column<int>(type: "INTEGER", nullable: false),
                    Currency_ISO_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Currency_Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Currency_Precision = table.Column<short>(type: "INTEGER", nullable: false),
                    Alias = table.Column<string>(type: "TEXT", nullable: true),
                    SubType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetail", x => x.AccountDetailId);
                    table.ForeignKey(
                        name: "FK_AccountDetail_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Payee = table.Column<string>(type: "TEXT", nullable: true),
                    Memo = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(65, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_BankTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportProfile",
                columns: table => new
                {
                    ImportProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HeaderRow = table.Column<int>(type: "INTEGER", nullable: false),
                    Delimiter = table.Column<char>(type: "TEXT", nullable: false),
                    TextQualifier = table.Column<char>(type: "TEXT", nullable: false),
                    DateFormat = table.Column<string>(type: "TEXT", nullable: true),
                    NumberFormat = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionDateColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    PayeeColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    MemoColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    AmountColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    AdditionalSettingCreditValue = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    CreditColumnIdentifierColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    CreditColumnIdentifierValue = table.Column<string>(type: "TEXT", nullable: true),
                    AdditionalSettingAmountCleanup = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdditionalSettingAmountCleanupValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportProfile", x => x.ImportProfileId);
                    table.ForeignKey(
                        name: "FK_ImportProfile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringBankTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecurrenceType = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurrenceAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstOccurrenceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Payee = table.Column<string>(type: "TEXT", nullable: true),
                    Memo = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(65, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringBankTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_RecurringBankTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bucket",
                columns: table => new
                {
                    BucketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    BucketGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ColorCode = table.Column<string>(type: "TEXT", nullable: true),
                    TextColorCode = table.Column<string>(type: "TEXT", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsInactive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInactiveFrom = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bucket", x => x.BucketId);
                    table.ForeignKey(
                        name: "FK_Bucket_BucketGroup_BucketGroupId",
                        column: x => x.BucketGroupId,
                        principalTable: "BucketGroup",
                        principalColumn: "BucketGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    AccountDetailId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_BudgetUser_AccountDetail_AccountDetailId",
                        column: x => x.AccountDetailId,
                        principalTable: "AccountDetail",
                        principalColumn: "AccountDetailId");
                });

            migrationBuilder.CreateTable(
                name: "BucketMovement",
                columns: table => new
                {
                    BucketMovementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BucketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65, 2)", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketMovement", x => x.BucketMovementId);
                    table.ForeignKey(
                        name: "FK_BucketMovement_Bucket_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Bucket",
                        principalColumn: "BucketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BucketRuleSet",
                columns: table => new
                {
                    BucketRuleSetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TargetBucketId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketRuleSet", x => x.BucketRuleSetId);
                    table.ForeignKey(
                        name: "FK_BucketRuleSet_Bucket_TargetBucketId",
                        column: x => x.TargetBucketId,
                        principalTable: "Bucket",
                        principalColumn: "BucketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BucketVersion",
                columns: table => new
                {
                    BucketVersionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BucketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    BucketType = table.Column<int>(type: "INTEGER", nullable: false),
                    BucketTypeXParam = table.Column<int>(type: "INTEGER", nullable: false),
                    BucketTypeYParam = table.Column<decimal>(type: "decimal(65, 2)", nullable: false),
                    BucketTypeZParam = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketVersion", x => x.BucketVersionId);
                    table.ForeignKey(
                        name: "FK_BucketVersion_Bucket_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Bucket",
                        principalColumn: "BucketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetedTransaction",
                columns: table => new
                {
                    BudgetedTransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BucketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetedTransaction", x => x.BudgetedTransactionId);
                    table.ForeignKey(
                        name: "FK_BudgetedTransaction_BankTransaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "BankTransaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetedTransaction_Bucket_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Bucket",
                        principalColumn: "BucketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MappingRule",
                columns: table => new
                {
                    MappingRuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BucketRuleSetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ComparisonField = table.Column<int>(type: "INTEGER", nullable: false),
                    ComparisonType = table.Column<int>(type: "INTEGER", nullable: false),
                    ComparisonValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingRule", x => x.MappingRuleId);
                    table.ForeignKey(
                        name: "FK_MappingRule_BucketRuleSet_BucketRuleSetId",
                        column: x => x.BucketRuleSetId,
                        principalTable: "BucketRuleSet",
                        principalColumn: "BucketRuleSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetail_AccountId",
                table: "AccountDetail",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_AccountId",
                table: "BankTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bucket_BucketGroupId",
                table: "Bucket",
                column: "BucketGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BucketMovement_BucketId",
                table: "BucketMovement",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BucketRuleSet_TargetBucketId",
                table: "BucketRuleSet",
                column: "TargetBucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BucketVersion_BucketId",
                table: "BucketVersion",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetedTransaction_BucketId",
                table: "BudgetedTransaction",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetedTransaction_TransactionId",
                table: "BudgetedTransaction",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetUser_AccountDetailId",
                table: "BudgetUser",
                column: "AccountDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProfile_AccountId",
                table: "ImportProfile",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingRule_BucketRuleSetId",
                table: "MappingRule",
                column: "BucketRuleSetId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringBankTransaction_AccountId",
                table: "RecurringBankTransaction",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BucketMovement");

            migrationBuilder.DropTable(
                name: "BucketVersion");

            migrationBuilder.DropTable(
                name: "BudgetedTransaction");

            migrationBuilder.DropTable(
                name: "BudgetUser");

            migrationBuilder.DropTable(
                name: "ImportProfile");

            migrationBuilder.DropTable(
                name: "MappingRule");

            migrationBuilder.DropTable(
                name: "RecurringBankTransaction");

            migrationBuilder.DropTable(
                name: "BankTransaction");

            migrationBuilder.DropTable(
                name: "AccountDetail");

            migrationBuilder.DropTable(
                name: "BucketRuleSet");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Bucket");

            migrationBuilder.DropTable(
                name: "BucketGroup");
        }
    }
}
