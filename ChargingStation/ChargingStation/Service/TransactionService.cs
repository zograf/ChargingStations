using System.Transactions;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;
using Transaction = ChargingStation.Data.Entity.Transaction;

namespace ChargingStation.Service;

public interface ITransactionService : IService<TransactionDomainModel>
{
}

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    public async Task<List<TransactionDomainModel>> GetAll()
    {
        List<Transaction> transactions = await _transactionRepository.GetAll();
        List<TransactionDomainModel> result = new List<TransactionDomainModel>();
        foreach (var item in transactions)
            result.Add(ParseToModel(item));
        return result;
    }

    public static TransactionDomainModel ParseToModel(Transaction transaction)
    {
        TransactionDomainModel transactionModel = new TransactionDomainModel
        {
            Id = transaction.Id,
            IsDeleted = transaction.IsDeleted,
            Amount = transaction.Amount,
            ClientId = transaction.ClientId,
            Time = transaction.Time,
            Type = (transaction.Type == "increase") ? TransactionDomainModel.TransactionType.INCREASE 
                : TransactionDomainModel.TransactionType.DECREASE
        };

        return transactionModel;
    }
}
