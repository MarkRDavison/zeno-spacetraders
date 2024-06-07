namespace mark.davison.spacetraders.console.Procedures;

public static class QueryContract
{
    private static IDictionary<ShipRole, HashSet<ContractType>> _validContractTypesPerShipRole;

    static QueryContract()
    {
        _validContractTypesPerShipRole = new Dictionary<ShipRole, HashSet<ContractType>>
        {
            { ShipRole.EXCAVATOR, new(){ ContractType.PROCUREMENT } }
        };
    }

    public static async Task AssignAvailableContract(
        SpacetradersDbContext dbContext,
        SpaceTradersApiClient api,
        Guid shipId,
        ShipRole role)
    {
        if (!_validContractTypesPerShipRole.ContainsKey(role))
        {
            Console.Error.WriteLine("Ship role '{0}' does not have any valid contract types", role);
            return;
        }

        var validContracts = _validContractTypesPerShipRole[role];

        var contracts = await api.GetContractsAsync(null, null);
        await Task.Delay(1000);

        var validContractsForShip = contracts.Data
            .Where(_ => !_.Accepted && validContracts.Contains(_.Type))
            .ToList();

        if (!validContractsForShip.Any())
        {
            Console.Error.WriteLine("Ship role '{0}' does not have any available contracts", role);
            return;
        }

        var ship = await dbContext.SpaceShips.FirstOrDefaultAsync(_ => _.Id == shipId);
        if (ship == null)
        {
            Console.Error.WriteLine("Ship '{0}' was not found", shipId);
            return;
        }
        var contract = validContractsForShip.First();
        var externalContractId = contract.Id;
        var acceptedContractResponse = await api.AcceptContractAsync(externalContractId);
        await Task.Delay(1000);

        var localContract = new SpaceContract
        {
            Id = Guid.NewGuid(),
            ExternalId = externalContractId
        };

        await dbContext.AddAsync(localContract);
        ship.ContractId = localContract.Id;
        dbContext.Update(ship);

        await dbContext.SaveChangesAsync();
        Console.WriteLine("{0} ship {1} has been assigned a contract", role, ship.Symbol);
    }
}
