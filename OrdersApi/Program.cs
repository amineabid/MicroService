﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrdersApi.Consumer;
using OrdersApi.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrdersApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersApiContext") ?? throw new InvalidOperationException("Connection string 'OrdersApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(optinos => {
    optinos.AddConsumer<ProductCreatedConsumer>();
    optinos.UsingRabbitMq((context, cnf) => {
        cnf.Host(new Uri("rabbitmq://localhost:4100"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cnf.ReceiveEndpoint("event-listener", e =>
        {
            e.ConfigureConsumer<ProductCreatedConsumer>(context);
        });
    });
});
var app = builder.Build();

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
