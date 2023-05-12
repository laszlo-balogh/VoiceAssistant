using System;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "ReactCors";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:34456/");
        }
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();


var reactPath = config.GetValue<string>("ReactPath");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();


Process killNodePorts = new Process();
killNodePorts.StartInfo.FileName = "cmd.exe";
killNodePorts.StartInfo.WorkingDirectory = @$"{reactPath}";
killNodePorts.StartInfo.Arguments = "/C npm stop";
killNodePorts.Start();
killNodePorts.WaitForExit();
Process process = new Process();
process.StartInfo.FileName = "cmd.exe";
process.StartInfo.WorkingDirectory = @$"{reactPath}";
process.StartInfo.Arguments = " /C npm start";
process.StartInfo.CreateNoWindow = true;
process.StartInfo.UseShellExecute = false;

string url = "http://localhost:34456/";
ProcessStartInfo startInfo = new ProcessStartInfo("chrome.exe", url);
Process.Start(startInfo);
process.Start();
process.WaitForExit();
app.Run();