using Infrastructure.DbContext;
using Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Products.Middleware;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Cache");
    options.Configuration = connection;
});

builder.Services.AddScoped<Products.Contracts.ICartService, Products.Services.CartService>();
builder.Services.AddAuthorization();


var app = builder.Build();

//app.MapDefaultEndpoints();

app.UseMiddleware<ExceptionMiddleware>();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    //app.MapIdentityApi<AppUser>();

app.Run();
