using Microsoft.AspNetCore.SignalR;
using MychatAPI;
using MychatAPI.Data;
using MychatAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<PostRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins( "http://localhost:5200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//    builder =>
//    {
//        builder.WithOrigins("*")
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});
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

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();
app.MapHub<LikeHub>("/likeHub");
app.MapHub<MessageHub>("/messageHub");
app.Run();
