using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IClientService : IService<ClientDomainModel>
{
    public Task<ClientDomainModel> GetByUserId(decimal id);
    public Task<TransactionDomainModel> Prepaid(TransactionDTO dto);
    public Task<List<VehicleDomainModel>> GetCards(decimal id);
}

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ITransactionService _transactionService;

    public ClientService(IClientRepository clientRepository, ITransactionService transactionService)
    {
        _clientRepository = clientRepository;
        _transactionService = transactionService;
    }

    public async Task<List<VehicleDomainModel>> GetCards(decimal id)
    {
        Client client = await _clientRepository.GetByUserId(id);
        List<VehicleDomainModel> result = new List<VehicleDomainModel>();
        foreach (var vehicle in client.Vehicles)
            result.Add(VehicleService.ParseToModel(vehicle));
        return result;
    }

    public async Task<ClientDomainModel> GetByUserId(decimal id)
    {
        return ParseToModel(await _clientRepository.GetByUserId(id));
    }

    public async Task<List<ClientDomainModel>> GetAll()
    {
        List<Client> clients = await _clientRepository.GetAll();
        List<ClientDomainModel> result = new List<ClientDomainModel>();
        foreach (var item in clients)
            result.Add(ParseToModel(item));
        return result;
    }

    public static ClientDomainModel ParseToModel(Client client)
    {
        ClientDomainModel clientModel = new ClientDomainModel
        {
            Id = client.Id,
            Balance = client.Balance,
            LegalName = client.LegalName,
            Name = client.Name,
            Surname = client.Surname,
            UserId = client.UserId,
            UserIdentificationNumber = client.UserIdentificationNumber,
            Vat = client.Vat
        };

        clientModel.Transactions = new List<TransactionDomainModel>();
        if (client.Transactions != null)
            foreach (var item in client.Transactions)
                clientModel.Transactions.Add(TransactionService.ParseToModel(item));
        
        clientModel.Vehicles = new List<VehicleDomainModel>();
        if (client.Vehicles != null)
            foreach (var item in client.Vehicles)
                clientModel.Vehicles.Add(VehicleService.ParseToModel(item));

        if (client.User != null)
            clientModel.User = UserService.ParseToModel(client.User);
        
        return clientModel;
    }

    public async Task<TransactionDomainModel> Prepaid(TransactionDTO dto)
    {
        Client client = await _clientRepository.GetById(dto.ClientId);
        if (client is null)
        {
            throw new Exception("Non existant client");
        }
        client.Balance += dto.Amount;
        _clientRepository.Update(client);
        _clientRepository.Save();
        return await _transactionService.Create(dto, true);
    }
}
