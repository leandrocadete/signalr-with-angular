using Microsoft.AspNetCore.SignalR;
using Model;
using Model.Services;
using Service;
using SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

#region ........ JWT ........
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Login.issuer,
        ValidAudience = Login.issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Login.secretKey))
    };
});

#endregion ...................
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

#region ........ TESTING ..........
app.UseAuthentication();
app.UseAuthorization();
#endregion ........................

app.MapControllers(); // doing

app.MapHub<RealTimeHub>("/hubs/Realtimehub");
app.MapHub<UserHub>("/hubs/UserHub");
//app.UseMiddleware()

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
