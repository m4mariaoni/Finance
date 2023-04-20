using AutoMapper;
using Finance.Data.Automapper;
using Finance.Infrastructure.ServiceExtension;
using Finance.Service.Interface;
using Finance.Service.Services;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDIServices(builder.Configuration);
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IInvoiceService, InvoiceService>();

        //Add automapper
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MyMappingProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);


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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}