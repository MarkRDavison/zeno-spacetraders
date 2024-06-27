namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly IServiceProvider _services;
    private readonly ICommonApplicationNotificationService _commonApplicationNotificationService;
    private readonly IApplicationNotificationService _applicationNotificationService;
    public MainWindowViewModel(IServiceProvider services)
    {
        _services = services;
        _commonApplicationNotificationService = services.GetRequiredService<ICommonApplicationNotificationService>();
        _applicationNotificationService = services.GetRequiredService<IApplicationNotificationService>();

        BasicApplicationViewModel = new BasicApplicationViewModel(
            "Spacetraders",
            services)
        {
            AppBarChildContentViewModel = services.GetRequiredService<AgentSummaryViewModel>()
        };

        BasicApplicationViewModel.PageGroups.Add(new(
            "Accounts",
            [services.GetRequiredService<AccountsPageViewModel>()],
            PageGroupConstants.AccountGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Contracts",
            [services.GetRequiredService<ContractsPageViewModel>()],
            PageGroupConstants.ContractGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Ships",
            [services.GetRequiredService<ShipsPageViewModel>()],
            PageGroupConstants.ShipGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Waypoints",
            [services.GetRequiredService<WaypointsPageViewModel>()],
            PageGroupConstants.WaypointGroupId));

        Setup();
    }

    private void Setup()
    {
        _applicationNotificationService.AccountChanged += AccountChanged;
        _applicationNotificationService.OpenShipRequested += OpenShip;
        _applicationNotificationService.OpenContractRequested += OpenContract;
    }

    private void Teardown()
    {
        _applicationNotificationService.OpenContractRequested -= OpenContract;
        _applicationNotificationService.OpenShipRequested -= OpenShip;
        _applicationNotificationService.AccountChanged -= AccountChanged;
    }

    private void OpenContract(object? sender, RequestOpenContractArgs e)
    {
        var pageGroup = BasicApplicationViewModel.PageGroups.First(_ => _.Id == PageGroupConstants.ContractGroupId);

        var existing = pageGroup.SubPages.FirstOrDefault(_ => _ is ManageContractViewModel mcvm && mcvm.ContractId == e.ContractId);

        if (existing is null)
        {
            var newPage = _services.GetRequiredService<ManageContractViewModel>();
            newPage.SetContractId(e.ContractId);
            pageGroup.SubPages.Add(newPage);

            _commonApplicationNotificationService.ChangePage(PageGroupConstants.ContractGroupId, e.ContractId);
        }
    }

    private void OpenShip(object? sender, RequestOpenShipArgs e)
    {
        var pageGroup = BasicApplicationViewModel.PageGroups.First(_ => _.Id == PageGroupConstants.ShipGroupId);

        var existing = pageGroup.SubPages.FirstOrDefault(_ => _ is ManageShipViewModel msvm && msvm.ShipSymbol == e.ShipSymbol);

        if (existing is null)
        {
            var newPage = _services.GetRequiredService<ManageShipViewModel>();
            newPage.SetShipSymbol(e.ShipSymbol);
            pageGroup.SubPages.Add(newPage);
        }

        _commonApplicationNotificationService.ChangePage(PageGroupConstants.ShipGroupId, e.ShipSymbol);
    }

    private void AccountChanged(object? sender, AccountChangeArgs e)
    {
        // TODO: Nicer way of resetting these
        var shipPageGroup = BasicApplicationViewModel.PageGroups.First(_ => _.Id == PageGroupConstants.ShipGroupId);
        foreach (var msvm in shipPageGroup.SubPages.Where(_ => _ is not ShipsPageViewModel).ToList())
        {
            shipPageGroup.SubPages.Remove(msvm);
        }

    }

    public void Dispose() => Teardown();

    public BasicApplicationViewModel BasicApplicationViewModel { get; }
}
