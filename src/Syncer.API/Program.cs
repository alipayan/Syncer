using Syncer.APIs.Endpoints;
using Syncer.APIs.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddHostedService<MemoryCachSetup>();

builder.Services.AddDbContext<SyncerDbContext>((sp, configure) =>
{
    //this will call for every request and will consume memory, so it's better to generate it outside of this section and pass it.
    var connectionstring = sp.GetRequiredService<IConfiguration>().GetConnectionString(SyncerDbContext.ConnectionStringName);
    configure.UseSqlServer(connectionstring);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEmojiEndpoints();
app.MapPresentationEndpoints();

app.Run();
