using System.Text.Json.Serialization;
using ChargingStation.Data;
using ChargingStation.Domain.Utilities;
using ChargingStation.Repository;
using ChargingStation.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


//Repositories
builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IBasePriceRepository, BasePriceRepository>();
builder.Services.AddTransient<ICardRepository, CardRepository>();
builder.Services.AddTransient<IChargingRepository, ChargingRepository>();
builder.Services.AddTransient<IChargingSpotRepository, ChargingSpotRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ICredentialsRepository, CredentialsRepository>();
builder.Services.AddTransient<IManagerRepository, ManagerRepository>();
builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IStationRepository, StationRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

//Domain
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IPriceService, PriceService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IChargingService, ChargingService>();
builder.Services.AddTransient<IChargingSpotService, ChargingSpotService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ICredentialsService, CredentialsService>();
builder.Services.AddTransient<IManagerService, ManagerService>();
builder.Services.AddTransient<IPlaceService, PlaceService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IStationService, StationService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<INotificationService, NotificationService>();


var connectionString = builder.Configuration.GetConnectionString("ChargingStationConnection");
builder.Services.AddDbContext<ChargingStationContext>(x => x.UseSqlServer(connectionString));
//builder.Services.AddDbContext<HealthCareContext>(x => x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddDbContext<ChargingStationContext>(x => x.EnableSensitiveDataLogging());

//builder.Services.AddSingleton<CronJobNotifications>();

//builder.Services.AddCors(options => 
//{
//    options.AddPolicy("CorsPolicy", 
//        corsBuilder => corsBuilder.WithOrigins("http://localhost:7195").AllowAnyMethod()
//           .AllowAnyHeader()
//            .AllowCredentials());
//});

builder.Services.AddCors(feature =>
                feature.AddPolicy(
                    "CorsPolicy",
                    apiPolicy => apiPolicy
                                    //.AllowAnyOrigin()
                                    //.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .SetIsOriginAllowed(host => true)
                                    .AllowCredentials()
                                ));

// Cron jobs
builder.Services.AddCronJob<CronJobReservationValidator>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = @"* * * * *";
});


var app = builder.Build();

// Configure the HTTP request pipeline.`
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
else 
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();