﻿namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class SystemBucket
{
    public static class Group
    {
        public static Guid AutoGenerated { get; } = Guid.Parse("00000000-0000-0000-0000-000000000005");
    }

    public static Guid Income { get; } = Guid.Parse("00000000-0000-0000-0000-000000000001");
    public static Guid Transfer { get; } = Guid.Parse("00000000-0000-0000-0000-000000000002");
    public static Guid Payables { get; } = Guid.Parse("00000000-0000-0000-0000-000000000003");
    public static Guid Receivables { get; } = Guid.Parse("00000000-0000-0000-0000-000000000004");
}