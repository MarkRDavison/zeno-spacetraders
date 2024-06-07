namespace mark.davison.spacetraders.console.Procedures.Models;

public record QueryShipInfo(Guid Id, string Symbol, ShipRole Role, SpaceshipState GlobalState, Guid? ContractId);
