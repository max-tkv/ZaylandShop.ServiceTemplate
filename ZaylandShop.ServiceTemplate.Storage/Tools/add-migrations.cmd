// ef commands templates for the Rider's terminal

cd ZaylandShop.ServiceTemplate.Storage
dotnet restore
dotnet ef -h
dotnet ef migrations add Initial --verbose --project ../ZaylandShop.ServiceTemplate.Storage --startup-project ../ZaylandShop.ServiceTemplate.Web
dotnet ef database update --verbose --project ../ZaylandShop.ServiceTemplate.Storage --startup-project ../ZaylandShop.ServiceTemplate.Web