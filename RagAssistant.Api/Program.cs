using RagAssistant.Api.Base.AppStarts;
using RagAssistant.Api.Handlers.Queries;
using RagAssistant.Share.AppStarts;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddControllerService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ExtractFileToTextQueryHandler).Assembly));
builder.Services.AddResponseCompressionService();

#region Logging
LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}");

// create logger before add Serilog logging with dispose: true
Log.Logger = loggerConfiguration.CreateLogger();

// by adding logging here will automatically added as a singleton scoped
builder.Services.AddLogging(i => i.AddSerilog(dispose: true));
#endregion

// add collections
builder.Services.AddFileCollection();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// add middlewares
app.UseResponseCompressionService();

// open world access for n8n
// เนื่องจากใช้ https://localhost:7274 ใน n8n local ไม่ได้ เพราะมันเข้าใจว่าเป็น localhost ของ n8n เอง
// เมื่อ deploy และได้ IP มา บรรทัดนี้จะไม่จำเป็น
app.Urls.Add("http://0.0.0.0:7274");

app.Run();
