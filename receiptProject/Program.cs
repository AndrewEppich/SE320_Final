using Microsoft.EntityFrameworkCore;
using receiptProject.Data;
using receiptProject.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<WeeklySummaryFilter>();
builder.Services.AddScoped<MonthlySummaryFilter>();
builder.Services.AddScoped<VendorFilter>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        policy =>
        {
           // Option 1: Allow specific origin with credentials
           policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
           
           // Option 2: Allow any origin but without credentials
           // policy
           //     .AllowAnyOrigin()
           //     .AllowAnyHeader()
           //     .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactAppPolicy");

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();



try
{

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            

            bool dbCreated = await context.Database.EnsureCreatedAsync();
            if (dbCreated) 
            {
                app.Logger.LogInformation("Database created successfully");
            }

            await DbInitializer.Initialize(services, app.Configuration);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error during database initialization");
        }
    }
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "An error occurred while setting up the database initialization process");
}

app.Run();

