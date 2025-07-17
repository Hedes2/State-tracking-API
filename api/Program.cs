using api.Services
;using api.Storage;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddSingleton<InMemoryStorage>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();