
namespace mark.davison.spacetraders.console.Procedures;

public static class PerformContract
{
    public static async Task UpdateContract(
        SpacetradersDbContext dbContext,
        SpaceTradersApiClient api,
        Guid shipId,
        Guid contractId)
    {
        var ship = await dbContext.SpaceShips.FindAsync(shipId);
        var contract = await dbContext.Contracts.FindAsync(contractId);

        if (ship == null)
        {
            throw new InvalidOperationException(string.Format("Ship '{0}' was not found", shipId));
        }

        if (contract == null)
        {
            throw new InvalidOperationException(string.Format("Contract '{0}' was not found", contractId));
        }

        var remoteContractResponse = await api.GetContractAsync(contract.ExternalId);
        var remoteContract = remoteContractResponse.Data;

        switch (remoteContract.Type)
        {
            case ContractType.PROCUREMENT:
                await HandleUpdateProcurementContract(dbContext, api, ship, contract, remoteContract);
                break;
            default:
                throw new InvalidOperationException(string.Format("Unhandled contract type: {0}", remoteContract.Type));
        }
    }

    private static Task HandleUpdateProcurementContract(
        SpacetradersDbContext dbContext,
        SpaceTradersApiClient api,
        Spaceship ship,
        SpaceContract contract,
        Contract remoteContract
    )
    {
        throw new NotImplementedException();
    }
}
