namespace mark.davison.spacetraders.desktop.ui;

public partial class ShipsPageView : UserControl
{
    public ShipsPageView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (DataContext is ShipsPageViewModel vm &&
            vm.CommandMenuCommand.CanExecute("OPEN"))
        {
            vm.CommandMenuCommand.Execute("OPEN");
        }
    }
}