﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeEvents.Api.Data;
using CodeEvents.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CodeEventsApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeEventsApiContext") ?? throw new InvalidOperationException("Connection string 'CodeEventsApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson();
              //  .AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

await app.SeedDataAsync();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
