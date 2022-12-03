# PortiaNet.Helper

This tool is a .Net class library to provide some functionalities and helper methods in MVC projects.
Helpers are as the following:

## MasterData

This part contains some master information which doesn't change too much. Currently, the following list of the masters are included:

* Continent
* Country
* Currency
* Language

### How To Use

You can simply call:

``` csharp
var continents = Continent.GetAll();
// Or
var countries = Country.GetAll();
```

## Security Helper

This helper provides the functionality to inject the encription and decription functions to any class. It gets the Enc/Dec key in the configuration provides the encryption and decryption using AES128 algorythm.

### How To Use

1. Configure DI

``` csharp
using PortiaNet.Helper.SecurityHelper

public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
{
    ...
    services.AddEncryptionDecryptionHelper(f =>
    {
        f.Key = 'Enc/Dec Key'; // The key length must be 32 chars
    });
    ...
}
```

2. Inject the class

``` csharp

public class AnyService
{
    private readonly EncryptionDecryptionHelper _encDecHelper;

    // Inject the EncryptionDecryptionHelper class
    public AnyService(EncryptionDecryptionHelper encDecHelper)
    {
        _encDecHelper = encDecHelper;
    }

    public void DoSomethingSpecial()
    {
        // Data encryption
        string? encryptedData = _encDecHelper.EncryptString("data");
    
        // Data decryption
        string? decryptedData = _encDecHelper.DecryptString(encryptedData);
    }
}

```

## ExtensionMethods.cs

This class contains some of the frequently used static methods. The namespace of this class is `System`, then it will be available everywhere by default.

``` csharp
string GetControllerName(this string value)
// var controllerName = nameof(HomeController).GetControllerName();

string GetEnumDescription(this Enum value)
// var enumDescription = Gender.Male.GetEnumDescription();

string GetDisplayName(this Enum enumValue)
// var enumDisplayName = Gender.Male.GetDisplayName();

IDictionary<int, String> GetEnumValueNames(Type type)
// var enumWithNames = GetEnumValueNames(typeof(Gender));

string GetDisplayGroup(this Enum value)
// var enumGroupName = Gender.Male.GetDisplayGroup();
```

## Pagination.cs

The Pagination class containes a base class to keep the main pagination information like `TotalRecords Count`, `FilteredRecords Count`, `PageSize`, `PageIndex`, and `CurrentPageRecordsCount`, and a derived generic class to keep the fetched and peged information in a list.

There is another static helper class inside this file to decrease the pagination headache and mistakes. It gets the `IQueryable<K>` as the data source, and filtering , sorting, and mapping functionalities as the input parameter, then returns a `PaginationModel<T>` as the result.

``` csharp
public Task<PaginationModel<StateViewModel>> GetStatesAsync(string filter, string? countryId, int pageIndex = 1, int pageSize = 10000)
    => PaginationHelper.GetPaginationAsync(_context.States.Include(f => f.Country)
        .Where(f => !f.IsDeleted && !f.Country.IsDeleted),
        query =>
        {
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(f => f.Name.Contains(filter) ||
                    f.RefNo.Contains(filter));

            if (!string.IsNullOrEmpty(countryId))
                query = query.Where(f => f.CountryId == countryId);

            return query;
        },
        sort =>
        {
            return sort.OrderBy(f => f.Name);
        },
        mapping =>
        {
            return mapping.Select(f => new StateViewModel
            {
                Id = f.Id,
                Name = f.Name,
                RefNo = f.RefNo,
                Country = f.Country?.Name,
                CountryId = f.CountryId,
                IsGovernorate = f.IsGovernorate
            }).ToList();
        },
        pageSize,
        pageIndex);
```