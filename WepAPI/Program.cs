using BusinessInterfaceLayer;
using BusinessLogicLayer;
using DataLayer;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddDbContext<BlockChainContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: allowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* Creating microsoft unity Container*/
//UnityContainer container = new UnityContainer();
//container.RegisterType<ITokenRepository, TokenRepository>(new HierarchicalLifetimeManager());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(allowSpecificOrigins); 

app.Run();
