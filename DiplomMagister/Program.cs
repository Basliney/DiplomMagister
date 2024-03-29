using DiplomMagister.Classes;
using DiplomMagister.Data;
using DiplomMagister.Middlewares;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Targets;
using NLog;
using NLog.Web;
using System.Text;
using Microsoft.Extensions.Logging;
using DiplomMagister.Services.OutServices;

internal class Program
{
    public static NLog.Logger Logger = LogManager.GetCurrentClassLogger();

    private static void Main(string[] args)
    {


        var folderName = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Logs";

        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }

        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        var fileTarget = new FileTarget($"target")
        {
            FileName = $"{folderName}{Path.DirectorySeparatorChar}${{shortdate}}.log",
            Layout = "${longdate}|${event-properties:item=EventId:whenEmpty=0}|${level:uppercase=true}|${_logger}|${message}|${exception:format=tostring}",
            KeepFileOpen = false,
            MaxArchiveDays = 14,
            ArchiveAboveSize = 4096000,
            Encoding = Encoding.UTF8,
        };
        LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().Targets.Add(fileTarget);
        });

        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var connection = configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ��������, ����� �� �������������� �������� ��� ��������� ������
                    ValidateIssuer = true,
                    // ������, �������������� ��������
                    ValidIssuer = configuration.GetSection("Bearer:ISSUER").Value,  //builder.Configuration["Bearer:ISSUER"],

                    // ����� �� �������������� ����������� ������
                    ValidateAudience = true,
                    // ��������� ����������� ������
                    ValidAudience = configuration.GetSection("Bearer:AUDIENCE").Value,  //builder.Configuration["Bearer:AUDIENCE"],
                    // ����� �� �������������� ����� �������������
                    ValidateLifetime = true,

                    // ��������� ����� ������������
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // ��������� ����� ������������
                    ValidateIssuerSigningKey = true,
                };
            });

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connection));

        builder.Logging.ClearProviders();
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Position));
        builder.Services.AddSingleton<IEmailService, EmailService>();
        builder.Host.UseNLog();

        try
        {
            Logger = logger;
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<TokenMiddleware>();
            app.UseMiddleware<AccessMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            Logger.Info("������ ����������");
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Program has been stopped");
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}