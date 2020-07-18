# Notes

## Database Setup

Start from the `MyEventPresentations.Data.Sqlite` folder

``` 
dotnet ef migrations add InitialCreate --startup-project ../MyEventPresentations.Api
dotnet ef database update --startup-project ../MyEventPresentations.Api
```

## API Design

Based off of [Create a web API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api) and [Web API with MongoDB](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)

## Azurite

Install [documentation](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

```bash
npm install -g azurite
```

Run Azurite

```bash
azurite --silent --location c:\azurite --debug c:\azurite\debug.log
```

*Optional*: Add the following to your `.gitignore`

```yaml
# Azurite
__blogstorage__/
__queuestorage__/
__azurite*.json
```

### Azurite Connections

To use locally, Azurite supports the well known [storage account and key](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?toc=/azure/storage/blobs/toc.json#authorization-for-tools-and-sdks).

* Account name: `devstoreaccount1`
* Account key: `Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==`