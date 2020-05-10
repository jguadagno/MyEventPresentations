# Notes

## Database Setup

Start from the `MyEventPresentations.Data.Sqlite` folder

``` 
dotnet ef migrations add InitialCreate --startup-project ../MyEventPresentations.Api
dotnet ef database update --startup-project ../MyEventPresentations.Api
```

## Azurite

Install [documentation](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

Run Azurite

```npm
azurite --silent --location c:\azurite --debug c:\azurite\debug.log
```

### Azurite Connections

To use locally, Azurite supports the well known [storage account and key](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?toc=/azure/storage/blobs/toc.json#authorization-for-tools-and-sdks).

* Account name: `devstoreaccount1`
* Account key: `Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==`