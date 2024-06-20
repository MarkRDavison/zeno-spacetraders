namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageShip
{
    private bool _inProgress;

    [Parameter]
    public required string Identifier { get; set; }

    [Parameter]
    public required string ShipSymbol { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IState<ContractState> ContractState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Identifier))
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchContractsAction, UpdateContractsActionResponse>(new()
            {
                Identifier = Identifier
            });
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrEmpty(Identifier) &&
            !string.IsNullOrEmpty(ShipSymbol))
        {
            var ship = ShipState.Value.GetShip(ShipSymbol);
            var shipNav = ShipState.Value.GetShipNav(ShipSymbol);

            if (ship == null || shipNav == null || WaypointState.Value.GetWaypoint(shipNav.WaypointSymbol) is null)
            {
                await Refresh();
            }
        }
    }

    private List<CommandMenuItem> CargoCommandMenuItems(ShipCargoItemDto item)
    {
        var nav = ShipState.Value.GetShipNav(ShipSymbol);
        var contract = ContractState.Value.Contracts
            .FirstOrDefault(_ => _.Terms.ContractDeliverGoods.Any(__ => __.DestinationSymbol == nav?.WaypointSymbol));

        if (contract is not null)
        {
            Console.WriteLine($"Found a contract ({contract.Id}) that {item.Name} can be delivered to");
        }

        var items = new List<CommandMenuItem>
        {
            new CommandMenuItem
            {
                Id = "SELL_ALL",
                Text = "Sell all",
                Disabled = ShipState.Value.GetShipNav(ShipSymbol)?.Status != "DOCKED"
            },
            new CommandMenuItem
            {
                Id = "JETTISON_ALL",
                Text = "Jettison all"
            },
        };

        if (contract != null)
        {
            items.Add(new CommandMenuItem
            {
                Id = "DELIVER_ALL",
                Text = "Deliver all",
                Disabled = ShipState.Value.GetShipNav(ShipSymbol)?.Status != "DOCKED"
            });
        }

        return items;
    }

    private async Task CargoCommandMenuItemSelected(CommandMenuItem item, ShipCargoItemDto cargoItem)
    {
        if (item.Id == "SELL_ALL")
        {
            await StoreHelper.DispatchAndWaitForResponse<SellCargoAction, UpdateShipsActionResponse>(new()
            {
                Identifier = Identifier,
                ShipSymbol = ShipSymbol,
                TradeSymbol = cargoItem.Symbol,
                Units = cargoItem.Units
            });
        }
        else if (item.Id == "JETTISON_ALL")
        {
            await StoreHelper.DispatchAndWaitForResponse<JettisonCargoAction, UpdateShipsActionResponse>(new()
            {
                Identifier = Identifier,
                ShipSymbol = ShipSymbol,
                TradeSymbol = cargoItem.Symbol,
                Units = cargoItem.Units
            });
        }
        else if (item.Id == "DELIVER_ALL")
        {
            var nav = ShipState.Value.GetShipNav(ShipSymbol);
            var contract = ContractState.Value.Contracts
                .FirstOrDefault(_ => _.Terms.ContractDeliverGoods.Any(__ => __.DestinationSymbol == nav?.WaypointSymbol));

            if (contract is null)
            {
                Console.WriteLine("FAILED TO DELIVER ALL :(");
                throw new InvalidOperationException("FAILED TO DELIVER ALL");
            }

            await StoreHelper.DispatchAndWaitForResponse<DeliverContractCargoAction, UpdateShipsActionResponse>(new()
            {
                Identifier = Identifier,
                ContractId = contract.Id,
                ShipSymbol = ShipSymbol,
                TradeSymbol = cargoItem.Symbol,
                Units = cargoItem.Units
            });

        }
    }

    private async Task Refresh()
    {
        _inProgress = true; // TODO: Process monitor/activity monitor

        await StoreHelper.DispatchAndWaitForResponse<FetchShipAction, UpdateShipsActionResponse>(new()
        {
            Identifier = Identifier,
            ShipSymbol = ShipSymbol
        });

        var nav = ShipState.Value.GetShipNav(ShipSymbol);
        if (nav is not null && WaypointState.Value.GetWaypoint(nav.WaypointSymbol) is null)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchWaypointAction, UpdateWaypointsActionResponse>(new()
            {
                Identifier = Identifier,
                SystemSymbol = nav.SystemSymbol,
                WaypointSymbol = nav.WaypointSymbol
            });
        }
        _inProgress = false;
    }

    private async Task ExtractResources()
    {
        _inProgress = true; // TODO: Process monitor/activity monitor
        await StoreHelper.DispatchAndWaitForResponse<ExtractResourcesAction, UpdateShipsActionResponse>(new()
        {
            Identifier = Identifier,
            ShipSymbol = ShipSymbol
        });
        _inProgress = false;
    }

    private async Task Refuel()
    {
        var fuel = ShipState.Value.GetShipFuel(ShipSymbol);

        _inProgress = true; // TODO: Process monitor/activity monitor
        await StoreHelper.DispatchAndWaitForResponse<RefuelShipAction, UpdateShipsActionResponse>(new()
        {
            Identifier = Identifier,
            ShipSymbol = ShipSymbol,
            FromCargo = false,
            Units = fuel is null ? 9999 : fuel.Capacity - fuel.Current
        });
        _inProgress = false;
    }
}
