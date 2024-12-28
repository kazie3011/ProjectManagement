using API;
using API.Api.Base.Extensions;
using API.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddMediaR();
builder.MemoryCache();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddEndpoints();

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MapOpenApi();
    app.MapScalarApiReference();    
}

app.MapEndpoints();

app.Run();

