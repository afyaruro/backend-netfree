using Infrastructure.Extensions.JWT;
using Infrastructure.Extensions.Persistence;
using Infrastructure.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServerContext(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddSwaggerConfig();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081);
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NetFree API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.UseCors("AllowAllOrigins");



app.Run();


// docker run --add-host=host.docker.internal:192.168.0.110 - d - p 8081:8081--name webapi miimagen
