namespace mark.davison.spacetraders.shared.client.Adapters;

public static class ContractAdapters
{
    public static ContractDto Adapt(this Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            Type = contract.Type.ToString(),
            Accepted = contract.Accepted,
            Fulfilled = contract.Fulfilled,
            FactionSymbol = contract.FactionSymbol,
            DeadlineToAccept = contract.DeadlineToAccept,
            Terms = contract.Terms.Adapt()
        };
    }

    public static ContractTermsDto Adapt(this ContractTerms contractTerms)
    {
        return new ContractTermsDto
        {
            Deadline = contractTerms.Deadline,
            Payment = contractTerms.Payment.Adapt(),
            Deliver = contractTerms.Deliver is null ? [] : [.. contractTerms.Deliver.Select(Adapt)]
        };
    }

    public static ContractPaymentDto Adapt(this ContractPayment contractPayment)
    {
        return new ContractPaymentDto
        {
            OnAccepted = contractPayment.OnAccepted,
            OnFulfilled = contractPayment.OnFulfilled
        };
    }

    public static ContractDeliverGoodDto Adapt(this ContractDeliverGood contractDeliverGood)
    {
        return new ContractDeliverGoodDto
        {
            DestinationSymbol = contractDeliverGood.DestinationSymbol,
            TradeSymbol = contractDeliverGood.TradeSymbol,
            UnitsFulfilled = contractDeliverGood.UnitsFulfilled,
            UnitsRequired = contractDeliverGood.UnitsRequired
        };
    }
}
