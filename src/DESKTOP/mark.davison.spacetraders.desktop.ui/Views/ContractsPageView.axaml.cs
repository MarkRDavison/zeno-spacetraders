namespace mark.davison.spacetraders.desktop.ui;

public partial class ContractsPageView : UserControl
{
    public ContractsPageView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (DataContext is ContractsPageViewModel vm &&
            vm.CommandMenuCommand.CanExecute("OPEN"))
        {
            vm.CommandMenuCommand.Execute("OPEN");
        }
    }
}