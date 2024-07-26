using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Core;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>();

var app = builder.Build();

app.Map("/", async () =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

});


app.Run();
