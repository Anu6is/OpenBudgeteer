using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data.Entities.Models;
using static OpenBudgeteer.Core.ViewModels.EntityViewModels.BucketVersionViewModel;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountManager(ExtendedServiceManager serviceManager)
{
    public ViewModelOperationResult CreateAccount(AccountDetailViewModel accountDetailModel)
    {
        try
        {
            var account = accountDetailModel.ToAccount();
            account = serviceManager.AccountService.Create(account); //create account

            var accountDetails = accountDetailModel.ToAccountDetail();
            accountDetails.AccountId = account.Id;
            serviceManager.AccountDetailService.Create(accountDetails); //add account details

            if (accountDetailModel.Balance != 0 && accountDetailModel.SubType is not SubType.Loan.Lending) //fund account
            {
                CreateBucketTransaction(account.Id, SystemBucket.Income, accountDetailModel.Balance,
                                        DateTime.Now.Subtract(TimeSpan.FromDays(1)), GetCreationMemo(accountDetailModel.SubType));
            }

            if (accountDetailModel.AccountType == AccountType.Loan) 
            {
                var result = accountDetailModel.SubType!.Equals(SubType.Loan.Borrowing)
                    ? DepositLoanTo(accountDetailModel.AssociatedAccountId!.Value, accountDetails, accountDetailModel.Balance)
                    : WithdrawLoanFrom(accountDetailModel.AssociatedAccountId!.Value, accountDetails, accountDetailModel.Balance);

                if (!result.IsSuccessful) return result;
            }

            return new ViewModelOperationResult(true, true);
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, e.Message);
        }
    }

    public static string GetCreationMemo(string? subType) => subType switch
    {
        SubType.Credit.Mastercard or SubType.Credit.Visa => "Current Balance",
        SubType.Loan.Borrowing => "Loan Acquisition",
        SubType.Loan.Lending => "Loan Disbursement",
        _ => "Initial Balance",
    };

    private ViewModelOperationResult CreateBucketTransaction(Guid accountId, Guid bucketId, decimal balance, DateTime transactionDate, string memo)
    {
        try
        {
            //Create transaction
            var bankTransaction = new BankTransaction
            {
                Id = Guid.Empty,
                TransactionDate = transactionDate,
                AccountId = accountId,
                Memo = memo,
                Amount = balance
            };

            bankTransaction = serviceManager.BankTransactionService.Create(bankTransaction);

            //Assign to bucket
            var bucketTransaction = new BudgetedTransaction()
            {
                Id = Guid.Empty,
                TransactionId = bankTransaction.Id,
                BucketId = bucketId,
                Amount = balance
            };
            
            serviceManager.BudgetedTransactionService.Create(bucketTransaction);

            return new ViewModelOperationResult(true, true);
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, e.Message);
        }
    }

    /// <summary>
    /// Deposits a received loan into an associated asset account
    /// </summary>
    /// <param name="associatedAccountId">The account in which the loan should be deposited</param>
    /// <param name="sourceAccount">The loan account</param>
    /// <param name="balance">The amount to be deposited</param>
    private ViewModelOperationResult DepositLoanTo(Guid associatedAccountId, AccountDetail sourceAccount, decimal balance)
    {
        var result = Transfer(sourceAccount.AccountId, associatedAccountId, balance, $"Loan received from {sourceAccount.Alias}");

        if (!result.IsSuccessful) return result;

        //add value to payables bucket
        result = CreateBucketTransaction(sourceAccount.AccountId, SystemBucket.Payables, -balance, DateTime.Now,
                                         $"Loan payable to {sourceAccount.Alias} by {sourceAccount.EffectiveDate:yyyy-MM-dd}");

        //create expense by month bucket to track repaying the loan

        return result.IsSuccessful ? new ViewModelOperationResult(true) : result;
    }

    /// <summary>
    /// Withdraws funds from an asset account in order to fund a loan lending account
    /// </summary>
    /// <param name="associatedAccountId">The account from which the funds should be withdrawn</param>
    /// <param name="destinationAccount">The loan account</param>
    /// <param name="balance">The amount to be withdrawn</param>
    /// <returns></returns>
    private ViewModelOperationResult WithdrawLoanFrom(Guid associatedAccountId, AccountDetail destinationAccount, decimal balance)
    {
        var result = Transfer(associatedAccountId, destinationAccount.AccountId, balance, $"Give loan to {destinationAccount.Alias}");

        if (!result.IsSuccessful) return result;

        //payout loan
        var bucket = new Bucket()
        {
            Id = Guid.Empty,
            BucketGroupId = SystemBucket.Group.AutoGenerated,
            Name = $"Loan Payout: {destinationAccount.Alias}",
            ValidFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
            ColorCode = "IndianRed",
            TextColorCode = "White",
            CurrentVersion = new BucketVersion 
            { 
                Version = 1, 
                BucketType = (int)BucketType.StandardBucket
            },
            BucketMovements = [new BucketMovement
            {
                Amount = balance,
                MovementDate = DateTime.Now
            }]
        };

        bucket = serviceManager.BucketService.Create(bucket);

        result = CreateBucketTransaction(destinationAccount.AccountId, bucket.Id, -balance, DateTime.Now, $"Loan payout to {destinationAccount.Alias}");

        if (!result.IsSuccessful) return result;

        //add value to receivables bucket
        result = CreateBucketTransaction(destinationAccount.AccountId, SystemBucket.Receivables, balance, DateTime.Now,
                                         $"Loan recoverable from {destinationAccount.Alias} by {destinationAccount.EffectiveDate:yyyy-MM-dd}");

        //create save by date bucket to track recovering the loan
        return result.IsSuccessful ? new ViewModelOperationResult(true) : result;
    }

    public ViewModelOperationResult UpdateAccount(AccountDetailViewModel accountDetailModel)
    {
        try
        {
            var account = accountDetailModel.ToAccount();
            var accountDetails = accountDetailModel.ToAccountDetail();

            serviceManager.AccountService.Update(account);
            serviceManager.AccountDetailService.Update(accountDetails);

            return new ViewModelOperationResult(true, true);
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, e.Message);
        }

    }

    public ViewModelOperationResult Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal amount, string memo = "Account transfer")
    {
        try
        {
            CreateBucketTransaction(sourceAccountId, SystemBucket.Transfer, -amount, DateTime.Now, $"Initiate transfer: {memo}");

            CreateBucketTransaction(destinationAccountId, SystemBucket.Transfer, amount, DateTime.Now, $"Finalize transer: {memo}");

            return new ViewModelOperationResult(true, true);
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, e.Message);
        }
    }
}
