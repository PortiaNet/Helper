# PortiaNet.Helper

A comprehensive .NET class library providing enterprise-grade utilities, helpers, and extensions for ASP.NET applications. This library streamlines common development tasks including data pagination, encryption/decryption, audit logging, AI-powered vision processing, and reference data management.

---

## Table of Contents

1. [AI Vision Processing](#ai-vision-processing)
2. [Database & Auditing](#database--auditing)
3. [Security & Encryption](#security--encryption)
4. [Pagination](#pagination)
5. [Master Data Reference](#master-data-reference)
6. [Extension Methods](#extension-methods)

---

## AI Vision Processing

### Overview

The Vision Processing module provides intelligent card and document extraction capabilities powered by AI and optical character recognition (OCR). It extracts structured data from card images including payment cards, ID cards, and business cards.

### Key Features

- **Multi-format Support**: Process card front and back images
- **Built-in Extractors**: 20+ pre-configured field extractors (card numbers, names, contact details, etc.)
- **Extensibility**: Register custom extractors for specialized data extraction
- **Confidence Scoring**: Get confidence levels for extracted fields
- **Image Quality Assessment**: Evaluate extracted image quality

### Quick Start

```csharp
using PortiaNet.Helper.Ai.Vision;

// Initialize the processor
var processor = new VisionProcessor();

// Process a card from Base64 strings
var result = processor.ProcessCard(frontBase64, backBase64);

// Access extracted data
foreach (var field in result.Fields)
{
    var fieldType = field.Key;
    var value = field.Value.Value;
    var confidence = field.Value.Confidence;
    
    Console.WriteLine($"{fieldType}: {value} (Confidence: {confidence:P})");
}

// Check overall confidence
Console.WriteLine($"Overall Confidence: {result.OverallConfidence:P}");
Console.WriteLine($"Image Quality: {result.ImageQuality}");
```

### Supported Field Types

- Card numbers and security details (CVV, expiry date, cardholder name)
- Contact information (phone, mobile, WhatsApp, email)
- Personal identifiers (name, title, designation)
- Business information (company name, branch, website)
- Social media handles (Instagram)
- Physical addresses and QR codes

### Register Custom Extractors

```csharp
// Implement IFieldExtractor interface
public class CustomExtractor : IFieldExtractor
{
    public CardFieldType FieldType { get; }
    public CardFieldExtraction Extract(string text) { /* ... */ }
}

// Register with processor
var processor = new VisionProcessor();
processor.RegisterExtractor(new CustomExtractor());
```

---

## Database & Auditing

### Overview

The Database module provides advanced Entity Framework Core integration with built-in audit logging and secure data handling. Choose between standard auditing or encrypted auditing based on your security requirements.

### Key Features

- **Automatic Audit Trails**: Track all entity changes with user attribution
- **Encrypted Auditing**: Optional field-level encryption for sensitive data
- **Change History**: Complete audit log of created, modified, and deleted records
- **Flexible Configuration**: Exclude specific columns or entities from auditing
- **ASP.NET Identity Integration**: Built-in support for Identity users

### Setup

#### Option 1: Standard Auditable Context

```csharp
// In Startup.cs
services.AddDbContext<YourDbContext>(options =>
    options.UseSqlServer(connectionString));

// Your DbContext class
public class YourDbContext : AuditableIdentityDbContext<IdentityUser, YourDbContext>
{
    public YourDbContext(DbContextOptions<YourDbContext> options) 
        : base(options)
    {
        // Optionally exclude columns from auditing
        NonAuditedColumns.Add("LastSyncDate");
    }

    public DbSet<YourEntity> YourEntities { get; set; }
}

// Save changes with audit tracking
var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
await context.SaveChangesAsync(userId);
```

#### Option 2: Encrypted + Auditable Context

```csharp
// Configure encryption first
services.AddEncryptionDecryptionHelper(options =>
{
    options.Key = "your-32-character-encryption-key!"; // Must be exactly 32 chars
});

// Add DbContext
services.AddDbContext<YourSecureDbContext>(options =>
    options.UseSqlServer(connectionString));

// Your DbContext class
public class YourSecureDbContext : SecureAuditableIdentityDbContext<IdentityUser, YourSecureDbContext>
{
    public YourSecureDbContext(DbContextOptions<YourSecureDbContext> options, 
        EncryptionDecryptionHelper encDecHelper) 
        : base(options, encDecHelper)
    {
    }

    public DbSet<YourEntity> YourEntities { get; set; }
}
```

### Exclude Entities/Fields from Auditing

```csharp
// On entity class - do not audit entire entity
[NoAudit]
public class NonAuditedEntity
{
    public int Id { get; set; }
}

// On property - encrypt this field automatically (Secure context only)
[Encrypt]
public string SensitiveData { get; set; }
```

### Access Audit Logs

```csharp
// Retrieve audit history
var auditLogs = context.AuditLogs
    .Where(a => a.TableName == "Users" && a.RecordId == userId)
    .OrderByDescending(a => a.DateTime)
    .ToList();

foreach (var log in auditLogs)
{
    Console.WriteLine($"Action: {log.Action}");
    Console.WriteLine($"User: {log.UserId}");
    Console.WriteLine($"Date: {log.DateTime}");
    Console.WriteLine($"Old Values: {log.OldValues}");
    Console.WriteLine($"New Values: {log.NewValues}");
}
```

---

## Security & Encryption

### Overview

The Security Helper provides AES-128 encryption and decryption functionality through dependency injection, enabling seamless integration with your application's security architecture.

### Setup

```csharp
using PortiaNet.Helper.SecurityHelper;

// In Startup.cs or Program.cs
services.AddEncryptionDecryptionHelper(options =>
{
    options.Key = "your-32-character-encryption-key!"; // IMPORTANT: Must be exactly 32 characters
});
```

### Usage

```csharp
public class UserService
{
    private readonly EncryptionDecryptionHelper _encDecHelper;

    public UserService(EncryptionDecryptionHelper encDecHelper)
    {
        _encDecHelper = encDecHelper;
    }

    public string StoreUserCredential(string credential)
    {
        // Encrypt before storing
        string encrypted = _encDecHelper.EncryptString(credential);
        
        // Save to database
        return encrypted;
    }

    public string RetrieveUserCredential(string encryptedCredential)
    {
        // Decrypt when needed
        string decrypted = _encDecHelper.DecryptString(encryptedCredential);
        
        return decrypted;
    }
}
```

### Error Handling

```csharp
try
{
    var encrypted = _encDecHelper.EncryptString(sensitiveData);
    var decrypted = _encDecHelper.DecryptString(encrypted);
}
catch (EncryptDecryptException ex)
{
    Console.WriteLine($"Encryption error: {ex.Message}");
    // Handle error appropriately
}
```

---

## Pagination

### Overview

The Pagination module simplifies server-side pagination, filtering, and sorting with a fluent API. It handles complex queries and returns properly structured paginated results with comprehensive metadata.

### Key Features

- **Efficient Querying**: Skip/take pattern for optimal database performance
- **Filtering**: Declarative filtering with LINQ
- **Sorting**: Custom sort implementations
- **Mapping**: Automatic entity-to-DTO transformation
- **Metadata**: Total records, filtered records, page information

### Basic Usage

```csharp
using PortiaNet.Helper.Pagination;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public Task<PaginationModel<UserViewModel>> GetUsersAsync(
        string filter = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        return PaginationHelper.GetPaginationAsync(
            allData: _context.Users.AsQueryable(),
            
            // Define filtering logic
            filter: query =>
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(u => 
                        u.FirstName.Contains(filter) ||
                        u.LastName.Contains(filter) ||
                        u.Email.Contains(filter));
                }
                return query;
            },
            
            // Define sorting logic
            sort: query => query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName),
            
            // Define mapping logic (entity to ViewModel)
            mapping: entities => entities.Select(u => new UserViewModel
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email
            }).ToList(),
            
            pageSize: pageSize,
            pageIndex: pageIndex
        );
    }
}
```

### Complex Filtering Example

```csharp
public Task<PaginationModel<OrderViewModel>> GetOrdersAsync(
    string searchTerm,
    DateTime? fromDate,
    DateTime? toDate,
    decimal? minAmount,
    int pageIndex = 1,
    int pageSize = 20)
{
    return PaginationHelper.GetPaginationAsync(
        allData: _context.Orders.Include(o => o.Customer).AsQueryable(),
        
        filter: query =>
        {
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(o => 
                    o.OrderNumber.Contains(searchTerm) ||
                    o.Customer.Name.Contains(searchTerm));

            if (fromDate.HasValue)
                query = query.Where(o => o.CreatedDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.CreatedDate <= toDate.Value);

            if (minAmount.HasValue)
                query = query.Where(o => o.TotalAmount >= minAmount.Value);

            return query;
        },
        
        sort: query => query.OrderByDescending(o => o.CreatedDate),
        
        mapping: entities => entities.Select(o => new OrderViewModel
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            Customer = o.Customer.Name,
            Amount = o.TotalAmount,
            Date = o.CreatedDate
        }).ToList(),
        
        pageSize: pageSize,
        pageIndex: pageIndex
    );
}
```

### Response Model

```csharp
public class PaginationModel<T>
{
    public List<T> Items { get; set; }              // Current page items
    public int TotalRecords { get; set; }           // Total records before filtering
    public int FilteredRecords { get; set; }        // Total records after filtering
    public int PageIndex { get; set; }              // Current page (1-based)
    public int PageSize { get; set; }               // Items per page
    public int CurrentPageRecordsCount { get; set; }// Items on current page
}
```

---

## Master Data Reference

### Overview

Master Data provides access to commonly used reference information including geographical data, currencies, languages, and time zones. This data is static and optimized for quick lookup.

### Available Masters

| Master | Data |
|--------|------|
| **Continent** | 7 continents |
| **Country** | All countries with country codes and regions |
| **Region** | States, provinces, and regions per country |
| **Currency** | Currency codes and denominations |
| **Language** | Language codes and names |
| **TimeZone** | Timezone information and UTC offsets |

### Usage

```csharp
using PortiaNet.Helper.MasterData;

// Get all continents
var continents = Continent.GetAll();
foreach (var continent in continents)
{
    Console.WriteLine($"{continent.Id}: {continent.Name}");
}

// Get all countries
var countries = Country.GetAll();
var usaCountry = countries.FirstOrDefault(c => c.Code == "US");

// Get countries by continent
var asianCountries = Country.GetAll()
    .Where(c => c.ContinentId == continentId)
    .ToList();

// Get regions/states for a country
var usStates = Region.GetAll()
    .Where(r => r.CountryId == usaCountry.Id)
    .ToList();

// Get currencies
var currencies = Currency.GetAll();
var usdCurrency = currencies.FirstOrDefault(c => c.Code == "USD");

// Get languages
var languages = Language.GetAll();

// Get timezones
var timezones = TimeZone.GetAll();
var utc = timezones.FirstOrDefault(tz => tz.Name == "UTC");
```

### Integration with Forms

```csharp
// In your controller or service
public IActionResult GetCountriesByContinent(int continentId)
{
    var countries = Country.GetAll()
        .Where(c => c.ContinentId == continentId)
        .Select(c => new { c.Id, c.Name })
        .ToList();
        
    return Json(countries);
}

// In your view (Blazor, MVC, etc.)
<select id="countrySelect">
    <option value="">-- Select Country --</option>
    @foreach (var country in Model.Countries)
    {
        <option value="@country.Id">@country.Name</option>
    }
</select>
```

---

## Extension Methods

### Overview

Extension Methods provide globally available utility functions for common operations including string manipulation, enum handling, and reflection. These methods are in the `System` namespace and automatically available throughout your application.

### String Extensions

```csharp
// Extract controller name from type name
string controllerName = nameof(HomeController).GetControllerName();
// Result: "Home"

string name = "UsersController".GetControllerName();
// Result: "Users"
```

### Enum Extensions

```csharp
public enum Gender
{
    [Display(Name = "Male")]
    Male = 1,

    [Display(Name = "Female")]
    Female = 2
}

// Get display name
string displayName = Gender.Male.GetDisplayName();
// Result: "Male"

// Get enum description
string description = Gender.Male.GetEnumDescription();

// Get all enum values with names as dictionary
var genderOptions = GetEnumValueNames(typeof(Gender));
// Result: { 1: "Male", 2: "Female" }

// Get display group (if applicable)
string group = Gender.Male.GetDisplayGroup();
```

### Enum Helper Example

```csharp
public class FormHelper
{
    public static Dictionary<int, string> GetSelectOptions(Type enumType)
    {
        return GetEnumValueNames(enumType);
    }
}

// In view
<select>
    @foreach (var option in FormHelper.GetSelectOptions(typeof(Gender)))
    {
        <option value="@option.Key">@option.Value</option>
    }
</select>
```

---

## Installation & Setup

### NuGet Package

```powershell
Install-Package PortiaNet.Helper
```

### Target Frameworks

- .NET 5.0
- .NET 6.0
- .NET 8.0
- .NET 9.0
- .NET Standard 2.0

---

## Support & Troubleshooting

### Common Issues

**Q: Encryption key must be 32 characters**  
A: The AES-128 encryption requires a key of exactly 32 characters. Generate a secure key or use `new EncryptionDecryptionConfig { Key = "your-32-character-key-here!!!!" }`.

**Q: Why is my audit log empty?**  
A: Ensure you're calling `SaveChangesAsync(userId)` or `SaveChanges(userId)` with a valid user ID. Non-audited entities or unchanged records won't appear in the log.

**Q: How do I paginate async operations?**  
A: Use `GetPaginationAsync()` with async delegates for filter, sort, and mapping operations.

---

## Best Practices

1. **Security**: Always use 32-character encryption keys; rotate regularly
2. **Performance**: Use pagination for large datasets; include only necessary fields in mappings
3. **Auditing**: Always provide user context when saving changes
4. **Validation**: Validate image quality before processing cards; check confidence scores
5. **Error Handling**: Wrap encryption/decryption operations in try-catch blocks

---

## License

[Your License Here]

## Contributing

Contributions are welcome! Please follow our coding standards and include unit tests for new features.
