using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Data;
using receiptProject.receiptProjectBackend.Services;
using Google.Cloud.Vision.V1;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Console.WriteLine($"DEBUG: Connection string being used: {connectionString}");
var connectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;AllowPublicKeyRetrieval=true;SslMode=none;";
Console.WriteLine($"DEBUG: Connection string being used: {connectionString}");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<WeeklySummaryFilter>();
builder.Services.AddScoped<MonthlySummaryFilter>();
builder.Services.AddScoped<VendorFilter>();
builder.Services.AddScoped<ReceiptImageProcessor>(provider => {
    var logger = provider.GetRequiredService<ILogger<ReceiptImageProcessor>>();
    var config = provider.GetRequiredService<IConfiguration>();
    var observer = provider.GetRequiredService<ConsoleReceiptObserver>();
    var processor = new ReceiptImageProcessor(logger, config);
    processor.AddObserver(observer);
    return processor;
});
builder.Services.AddSingleton(provider =>
{
    return ImageAnnotatorClient.Create();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        policy =>
        {
           // Option 1: Allow specific origin with credentials
           policy
                .WithOrigins("http://localhost:5173")
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
builder.Services.AddSingleton<ConsoleReceiptObserver>();
var app = builder.Build();


// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseCors("ReactAppPolicy");

//app.UseHttpsRedirection();


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

