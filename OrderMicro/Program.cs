using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderMicro.BusMessage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<MessageBusSuscriber>();
builder.Services.AddDbContext<OrderMicro.Model.OrderContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("miDB"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLDb"), new MySqlServerVersion(new System.Version(8, 0, 22)));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageBusClient, MessageBusPublisher>();

var app = builder.Build();
using (var scope = app.Services.CreateScope()) 
{
    var context = scope.ServiceProvider.GetRequiredService<OrderMicro.Model.OrderContext>();
    context.Database.Migrate();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseAuthorization();

app.MapControllers();

app.Run();
