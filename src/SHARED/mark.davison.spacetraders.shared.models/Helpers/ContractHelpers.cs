namespace mark.davison.spacetraders.shared.models.Helpers;

public static class ContractHelpers
{
    public static ContractDto ToContractDto(Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            Accepted = contract.Accepted,
            DeadlineToAccept = contract.DeadlineToAccept,
            FactionSymbol = contract.FactionSymbol,
            Fulfilled = contract.Fulfilled,
            Type = contract.Type.ToString(),
            Terms = new ContractTermsDto
            {
                Deadline = contract.Terms.Deadline,
                ContractDeliverGoods = [.. contract.Terms.Deliver.Select(_ => new ContractDeliverGoodDto
                {
                    TradeSymbol = _.TradeSymbol,
                    DestinationSymbol = _.DestinationSymbol,
                    UnitsFulfilled = _.UnitsFulfilled,
                    UnitsRequired = _.UnitsRequired
                })],
                Payment = new ContractPaymentDto
                {
                    AcceptedCredits = contract.Terms.Payment.OnAccepted,
                    FulfilledCredits = contract.Terms.Payment.OnFulfilled
                }
            }
        };
    }
}
