using api.Database;
using api.Exception;
using api.Repository;
using api.Services;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDB>(option => option.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(9, 2, 0))
)); // Thêm dịch vụ DbContext với MySQL
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoucherReponsitory, VoucherResponsitory>();
builder.Services.AddExceptionHandler<APIException>(); // Thêm dịch vụ xử lý ngoại lệ

// builder.Services.AddDbContext<ApplicationDB>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8, 0, 32))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.Run();
