using AccountMicro.Accounting.Querys;
using AccountMicro.BusMessage;
using Domain.AccountMicroDomain.Interfaces;
using Domain.AccountMicroDomain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderMicro.BusMessage;
using Repositories.AccountMicroRepositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AccountContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("miDB"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLDb"), new MySqlServerVersion(new System.Version(8, 0, 22)));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IMessageBusClient, MessageBusPublisher>();
//builder.Services.AddHostedService<MessageBusSuscriber>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnityOfWork, AccountUnitOfWork>();


// Configure the HTTP request pipeline.

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AccountContext>();
    context.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
