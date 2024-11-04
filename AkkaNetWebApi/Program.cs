using AkkaNetWebApi.Actors;
using AkkaNetWebApi.Data;
using AkkaNetWebApi.implements;
using AkkaNetWebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//DI
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>(); // or Singleton if appropriate


builder.Services.AddAkka();

// Learn more about configuring Swagger/OpenAPI at 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

