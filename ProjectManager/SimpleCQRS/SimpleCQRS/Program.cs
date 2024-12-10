using Application;
using Infrastructure;
using Infrastructure.Data;
using SimpleCQRS;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.

    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationService();
    builder.Services.AddApiServices(builder.Configuration, builder.Environment);
    // Add services to the container.
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
        app.UseForwardedHeaders();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        await app.InitialDatabaseAsync();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseAuthorization();
    app.UseRouting();
    app.UseCors();
    app.UseAuthentication();
    app.UseIdentityServer();
    app.UseAuthorization();
    app.MapControllers();
    app.MapDefaultControllerRoute();
    app.MapRazorPages();
    app.Run();

}
catch(Exception ex)
{
    throw;
}