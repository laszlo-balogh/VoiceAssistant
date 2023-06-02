using System;
using System.Diagnostics;
using VoiceAssistant.Controllers;
using VoiceAssistant.Data;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "ReactCors";

IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

var reactPath = config.GetValue<string>("ReactPath");
var reactUrl = config.GetValue<string>("ReactUrl");

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//        policy =>
//        {
//            policy.WithOrigins(reactUrl);
//        }
//    );
//});
builder.Services.AddHttpClient();
//builder.Services.AddSingleton<IConfiguration>(provider => configuration); nem kell inejctalni mert a builder propertije
//https://stackoverflow.com/questions/70430384/how-to-inject-iconfiguration-in-asp-net-core-6
builder.Services.AddTransient<ICommandRepository, CommandRepository>();
builder.Services.AddTransient<CommandController>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseAuthorization();


app.UseRouting();



app.UseCors(x => x
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:34456"));


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



//app.MapControllers();
Process killNodePorts = new Process();
killNodePorts.StartInfo.FileName = "cmd.exe";
killNodePorts.StartInfo.WorkingDirectory = @$"{reactPath}";
killNodePorts.StartInfo.Arguments = "/C npm stop";

Task killNodePortsTask = new Task(() =>
{
    killNodePorts.Start();
    killNodePorts.WaitForExit();
}, TaskCreationOptions.LongRunning);
killNodePortsTask.Start();

Process reactProcess = new Process();
reactProcess.StartInfo.FileName = "cmd.exe";
reactProcess.StartInfo.WorkingDirectory = @$"{reactPath}";
reactProcess.StartInfo.Arguments = " /C npm start";
reactProcess.StartInfo.CreateNoWindow = true;
reactProcess.StartInfo.UseShellExecute = false;


ProcessStartInfo startInfo = new ProcessStartInfo("chrome.exe", reactUrl);
Task reactTask = new Task(() =>
{
    Process.Start(startInfo);
    reactProcess.Start();
    reactProcess.WaitForExit();
}, TaskCreationOptions.LongRunning);
reactTask.Start();

app.Run();