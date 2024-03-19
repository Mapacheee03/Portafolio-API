using Portafolio_API.Configurations;
using Portafolio_API.Services;
using SharpCompress.Compressors.PPMd;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<PortafolioServices>();
//agregar CORS rules
builder.Services.AddCors(options => options.AddPolicy("AngularClient", policy => {
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AngularClient");

app.MapControllers();

app.Run();
