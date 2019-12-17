dotnet ef database update --context IdentityDbContext --project Identity\Shop.Identity.DataAccess.MsSql --startup-project Shop.Web
dotnet ef database update --context CommonDbContext --project Common\Shop.Common.DataAccess.MsSql --startup-project Shop.Web
dotnet ef database update --context OrderDbContext --project Order\Shop.Order.DataAccess.MsSql --startup-project Shop.Web

