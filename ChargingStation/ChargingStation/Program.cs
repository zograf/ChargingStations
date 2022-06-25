using System.Text.Json.Serialization;
using ChargingStation.Data;
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
//builder.Services.AddTransient<IAntiTrollRepository, AntiTrollRepository>();

//Domain
//builder.Services.AddTransient<IAntiTrollService, AntiTrollService>();


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


//builder.Services.AddCronJob<CronJobNotifications>(c =>
//{
//    c.TimeZoneInfo = TimeZoneInfo.Local;
//    c.CronExpression = @"*/5 * * * *";
//});
//MailSender sender = new MailSender("usi2022hospital@gmailcom", "lazzarmilanovic@gmail.com");
//sender.SetBody("test");
//sender.SetSubject("test");
//sender.Send();


// Cron jobs
//builder.Services.AddCronJob<CronJobNotifications>(c =>
//{
//    c.TimeZoneInfo = TimeZoneInfo.Local;
//    c.CronExpression = @"* * * * *";
//});


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