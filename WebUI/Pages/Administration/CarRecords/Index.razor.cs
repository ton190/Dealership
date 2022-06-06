using Microsoft.AspNetCore.Components.Web;
using ModelLibrary.CarBrands;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecords;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected RecordActionBase RecordAction { get; set; } = null!;
    protected List<CarRecordDto>? CarRecords { get; set; }
    protected List<CarBrandDto> CarBrands { get; set; } = new();
    protected string _searchString { get; set; } = string.Empty;

    private int page = 1;
    protected int Page
    {
        get => page <= TotalPages ? page : TotalPages;
        set => page = value;
    }
    protected int PageSize { get; set; } = 10;
    protected int TotalPages =>
        (int)Math.Ceiling(Search(CarRecords).Count() / (double)PageSize);
    protected List<PagBox> PagBoxes { get; set; } = new();

    private void SetBoxHandlers()
    {
        PagBoxes = new();
        for (var i = 1; i < TotalPages+1; i++)
        {
            var temp = i;
            var box = new PagBox();
            box.Id = i;
            box.Action = (e) => Page = temp;
            PagBoxes.Add(box);
        }
    }

    protected List<CarRecordDto> CarRecordList
        => Search(CarRecords).Skip((Page - 1) * PageSize)
        .Take(PageSize).ToList();

    protected async Task Refresh()
    {
        await GetRecords();
        SetBoxHandlers();
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        await GetRecords();
        SetBoxHandlers();
    }

    protected async Task GetRecords()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<List<CarRecordDto>>>(
                ApiRoutes.CarRecords.GetAll);

        if (request != null && request.Success && request.Response != null)
            CarRecords = request.Response;
    }

    protected string GetBrandName(int id)
    {
        var result = CarBrands.Where(x => x.Id == id).FirstOrDefault();
        return result is null ? "" : result.Name;
    }

    protected async Task GetBrands()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<List<CarBrandDto>>>(
                ApiRoutes.CarBrands.GetAll);

        if (request != null && request.Success && request.Response != null)
            CarBrands = request.Response;
    }

    protected void OnSearch(ChangeEventArgs args)
    {
        if (args.Value is null) return;

        _searchString = (string)args.Value;
        SetBoxHandlers();
    }

    private List<CarRecordDto> Search(List<CarRecordDto>? records)
    {
        if (records is null) return new();

        if (_searchString == "")
        {
            return records;
        }
        List<CarRecordDto> newRecords = new();

        newRecords.AddRange(records.Where(x => x.BusinessName.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase) ||
            x.FINCode.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase) ||
            x.BusinessAddress.Address.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase) ||
            CarBrands.First(y => y.Name.ToLower() == x.CarBrand.ToLower())
            .Name.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase) ||
            x.ContactNames.Any(x => x.FirstName.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase) ||
            x.LastName.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase)) ||
            x.PhoneNumbers.Any(x => x.Number.Contains(
            _searchString, StringComparison.OrdinalIgnoreCase))));

        return newRecords;
    }

    protected class PagBox
    {
        public int Id { get; set; }
        public Action<MouseEventArgs> Action { get; set; } = e => { };
    }
}
