using ModelLibrary.CarBrands;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecords;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected RecordActionBase RecordAction { get; set; } = null!;
    protected List<CarRecordDto>? CarRecords { get; set; }
    protected List<CarRecordDto>? DisplayCarRecords { get; set; }
    protected List<CarBrandDto> CarBrands { get; set; } = new();
    protected string _searchString { get; set; } = string.Empty;

    protected async Task Refresh()
    {
        await GetRecords();
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        await GetRecords();
    }

    protected async Task GetRecords()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<List<CarRecordDto>>>(
                ApiRoutes.CarRecords.GetAll);

        if (request != null && request.Success && request.Response != null)
            CarRecords = request.Response;
        Search();
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
        if(args.Value is null) return;

        _searchString = (string)args.Value;
        Search();
    }

    private void Search()
    {
        if(CarRecords is null) return;

        if (_searchString == "")
        {
            DisplayCarRecords = CarRecords;
            return;
        }
        List<CarRecordDto> records = new();

        records.AddRange(CarRecords.Where(x => x.BusinessName.Contains(
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

        DisplayCarRecords = records;
    }
}
