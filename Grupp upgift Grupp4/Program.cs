var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//SimonTest
app.MapGet("/", () => "Hello World!");

app.Run();
