using MidtermAPI_AryanGajjar.Middleware;
using MidtermAPI_AryanGajjar.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IAGProductService,AGProductService>();
builder.Services.AddSingleton<AGUsageCounter>();


var app = builder.Build();
app.UseMiddleware<AGGlobalExceptionMiddleware>();
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
