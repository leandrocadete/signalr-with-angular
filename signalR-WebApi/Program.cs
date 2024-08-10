using Microsoft.AspNetCore.SignalR;
using Model;
using Model.Services;
using Service;
using SignalR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddTransient<ILogin, Login>();
//builder.Services.AddMvcCore(); // doing

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/LoginHelper", () => {
    return Task.FromResult(new User(){
        Name = "User Example",
        Email = "emailteste",
        Id = 150
    });
});
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers(); // doing

app.MapHub<RealTimeHub>("/hubs/Realtimehub");
app.MapHub<UserHub>("/hubs/userCount");
//app.UseMiddleware()
//app.MapControllers();

app.Use(async (context, next) => {
    var hubContext = context.RequestServices.GetRequiredService<IHubContext<RealTimeHub>>();
    if(next != null) {
        await next.Invoke();
    }
});

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());  // add CORS for angular 


//app.UseMvc();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
