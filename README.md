# ShopRu
### Get Project
`
git clone https://github.com/harunmarangoz/ShopRu.git
`
## Test
- Locate to root project folder
- Configure `Business.Test/appsettings.json` for database connection
- Run `dotnet test .\Business.Test\`
## Debug
- Locate to root project folder
- Configure `WebApi/appsettings.Development.json` for database connection
- Migrate db with this command `dotnet ef database update --project Data --startup-project WebApi`
- Run with `dotnet run --project WebApi`

### Api
You can use Postman collection with `ShopRu.postman_collection.json` file in root folder .
### Database
App supports two types of database
- InMemory for testing
- SqlServer for debugging
