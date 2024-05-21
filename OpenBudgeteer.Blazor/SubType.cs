﻿namespace OpenBudgeteer.Blazor;

public class SubType
{
    public static class Credit
    {
        public const string Visa = "VISA";
        public const string Mastercard = "Mastercard";
    }

    public static class Deposit
    {
        public const string Savings = "Savings";
        public const string Checking = "Checking";
    }

    public static class Loan
    {
        public const string Borrowing = "Borrowing";
        public const string Lending = "Lending";
    }

    public static class Investment
    {
        public const string Brokerage = "Brokerage";
        public const string Pension = "Pension";
        public const string Retirement = "Retirement";
        public const string MutualFund = "Mutual Fund";

    }
}