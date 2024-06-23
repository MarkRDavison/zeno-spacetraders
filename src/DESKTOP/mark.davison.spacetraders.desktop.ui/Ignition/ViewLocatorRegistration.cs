namespace mark.davison.spacetraders.desktop.ui.Ignition;

public static class ViewLocatorRegistration
{
    public static void RegisterViews()
    {
        ViewLocator.Register<AccountsPageViewModel, AccountsPageView>();
        ViewLocator.Register<AgentSummaryViewModel, AgentSummaryView>();
        ViewLocator.Register<ContractsPageViewModel, ContractsPageView>();
        ViewLocator.Register<RegisterAgentDialogViewModel, RegisterAgentDialogView>();
    }
}
