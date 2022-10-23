using Infrastructure;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateCormadoRight", policy =>
          policy.RequireRole("InnoetechAdmin", "CormadoAdmin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CreateUser}/{action=CreateUser}/{id}"
    );

app.UseSession();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("token");
    if (!string.IsNullOrEmpty(token))
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Request.Headers.Add("Authorization", "Bearer" + token);
        }
    }
    await next();
});


app.Run();
