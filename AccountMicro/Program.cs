using AccountMicro.BusMessage;
using Microsoft.EntityFrameworkCore;
using OrderMicro.BusMessage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AccountMicro.Models.AccountContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("miDB"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLDb"), new MySqlServerVersion(new System.Version(8, 0, 22)));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageBusClient, MessageBusPublisher>();
builder.Services.AddHostedService<MessageBusSuscriber>();



// Configure the HTTP request pipeline.

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AccountMicro.Models.AccountContext>();
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
