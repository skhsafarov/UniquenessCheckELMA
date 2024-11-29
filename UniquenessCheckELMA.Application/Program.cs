using Microsoft.OpenApi.Models;

using System.Reflection;

using UniquenessCheckELMA.Infrastructure;

namespace UniquenessCheckELMA.Application;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<DataContext>();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapDefaultEndpoints();

        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        //app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}