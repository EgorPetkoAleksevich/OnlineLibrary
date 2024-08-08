using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Core;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

