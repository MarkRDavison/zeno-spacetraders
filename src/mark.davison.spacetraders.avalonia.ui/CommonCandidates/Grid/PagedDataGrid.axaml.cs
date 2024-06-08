using mark.davison.spacetraders.avalonia.ui.CommonCandidates.Grid;
using System.Collections;

namespace mark.davison.spacetraders.avalonia.ui;

public partial class PagedDataGrid : UserControl
{
    public PagedDataGrid()
    {
        InitializeComponent();

    }

    private Func<GridMeta, Task<(GridMeta, IEnumerable)>>? _changePage;

    public static readonly DirectProperty<PagedDataGrid, GridMeta> MetaProperty =
        AvaloniaProperty.RegisterDirect<PagedDataGrid, GridMeta>(
            nameof(Meta),
            _ => _.Meta,
            (o, e) => o.Meta = e);

    private GridMeta _meta = new();
    public GridMeta Meta
    {
        get => _meta;
        set => SetAndRaise(MetaProperty, ref _meta, value);
    }

    public static readonly DirectProperty<PagedDataGrid, IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.RegisterDirect<PagedDataGrid, IEnumerable>(
            nameof(ItemsSource),
            _ => _.ItemsSource,
            (o, e) => o.ItemsSource = e);

    private IEnumerable _itemsSource;
    public IEnumerable ItemsSource
    {
        get => _itemsSource;
        set => SetAndRaise(ItemsSourceProperty, ref _itemsSource, value);
    }

    private int MaxPageIndex => (int)Math.Ceiling((decimal)Meta.Total / Meta.Limit);

    [RelayCommand(CanExecute = nameof(CanAdjustPage))]
    private async Task AdjustPage(int offset, CancellationToken cancellationToken)
    {
        var changedMeta = new GridMeta
        {
            Limit = Meta.Limit,
            Page = Meta.Page,
            Total = Meta.Total
        };

        if (offset < 0)
        {
            if (offset == -1)
            {
                changedMeta.Page--;
            }
            else
            {

                changedMeta.Page = 1;
            }
        }
        else
        {
            if (offset == 1)
            {
                changedMeta.Page++;
            }
            else
            {

                changedMeta.Page = MaxPageIndex;
            }
        }

        if (_changePage != null)
        {
            var (newMeta, results) = await _changePage.Invoke(changedMeta);

            Meta = newMeta;
        }
        else
        {
            Meta = changedMeta;
        }
    }

    private bool CanAdjustPage(int offset)
    {
        if (offset > 0)
        {
            return (Meta.Page) < MaxPageIndex;
        }
        else
        {
            return (Meta.Page) > 1;
        }
    }
}