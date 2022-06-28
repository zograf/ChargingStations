using System.Transactions;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;
using Transaction = ChargingStation.Data.Entity.Transaction;

namespace ChargingStation.Service;

public interface ITransactionService : IService<TransactionDomainModel>
{
    public Task<TransactionDomainModel> Create(TransactionDTO transaction, bool increase);
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
            Type = transaction.Type
        };

        return transactionModel;
    }

    public async Task<TransactionDomainModel> Create(TransactionDTO transactionDTO, bool increase)
    {
        Transaction transaction = new Transaction
        {
            Amount = transactionDTO.Amount,
            Time = DateTime.Now,
            ClientId = transactionDTO.ClientId,
            IsDeleted = false,
            Type = increase ? "increase" : "decrease",
        };
        _transactionRepository.Post(transaction);
        _transactionRepository.Save();
        return ParseToModel(transaction);
    }
}
