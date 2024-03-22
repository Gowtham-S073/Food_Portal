 using FoodPortal.Interfaces;
using FoodPortal.Mapper;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.Repos;
using FoodPortal.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
#nullable disable


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenGenerate, TokenService>();
builder.Services.AddScoped<ICrud<User, UserDTO>, UserRepo>();
builder.Services.AddScoped<IAdditionalCategoryMasterService, AdditionalCategoryMasterService>();
builder.Services.AddScoped<ICrud<AdditionalCategoryMaster, IdDTO>, AdditionalCategoryMasterRepo>();
builder.Services.AddScoped<IGroupSizeService, GroupSizeService>();
builder.Services.AddScoped<ICrud<GroupSize, IdDTO>, GroupSizeRepo>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ICrud<TimeSlot, IdDTO>, TimeSlotRepo>();
builder.Services.AddScoped<IDeliveryOptionService, DeliveryOptionService>();
builder.Services.AddScoped<ICrud<DeliveryOption, IdDTO>, DeliveryOptionRepo>();
builder.Services.AddScoped<IAddOnsMasterService, AddOnsMasterService>();
builder.Services.AddScoped<ICrud<AddOnsMaster, IdDTO>, AddOnsMasterRepo>();
builder.Services.AddScoped<IStdProductService, StdProductService>();
builder.Services.AddScoped<ICrud<StdProduct, IdDTO>, StdProductRepo>();
builder.Services.AddScoped<IAdditionalProductService, AdditionalProductService>();
builder.Services.AddScoped<ICrud<AdditionalProduct, IdDTO>, AdditionalProductRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICrud<Order, IdDTO>, OrderRepo>();
builder.Services.AddScoped<IAdditionalProductsDetailService, AdditionalProductsDetailService>();
builder.Services.AddScoped<ICrud<AdditionalProductsDetail, IdDTO>, AdditionalProductsDetailRepo>();
builder.Services.AddScoped<IAddOnsDetailService, AddOnsDetailService>();
builder.Services.AddScoped<ICrud<AddOnsDetail, IdDTO>, AddOnsDetailRepo>();
builder.Services.AddScoped<IAllergyDetailService, AllergyDetailService>();
builder.Services.AddScoped<ICrud<AllergyDetail, IdDTO>, AllergyDetailRepo>();
builder.Services.AddScoped<IFoodTypeCountService, FoodTypeCountService>();
builder.Services.AddScoped<ICrud<FoodTypeCount, IdDTO>, FoodTypeCountRepo>();
builder.Services.AddScoped<IStdFoodCategoryMasterService, StdFoodCategoryMasterService>();
builder.Services.AddScoped<ICrud<StdFoodCategoryMaster, IdDTO>, StdFoodCategoryMasterRepo>();
builder.Services.AddScoped<IStdFoodOrderDetailService, StdFoodOrderDetailService>();
builder.Services.AddScoped<ICrud<StdFoodOrderDetail, IdDTO>, StdFoodOrderDetailRepo>();
builder.Services.AddScoped<IPlateSizeService, PlateSizeService>();
builder.Services.AddScoped<ICrud<PlateSize, IdDTO>, PlateSizeRepo>();
builder.Services.AddScoped<ITrackStatusService, TrackStatusService>();
builder.Services.AddScoped<ICrud<TrackStatus, IdDTO>, TrackStatusRepo>();
builder.Services.AddScoped<IAllergyMasterService, AllergyMasterService>();
builder.Services.AddScoped<ICrud<AllergyMaster, IdDTO>, AllergyMasterRepo>();
builder.Services.AddScoped<IDrinksMasterService, DrinksMasterService>();
builder.Services.AddScoped<ICrud<DrinksMaster, IdDTO>, DrinksMasterRepo>();
builder.Services.AddScoped<IAdditionalOrderDetailService, AdditionalOrderDetailService>();
builder.Services.AddScoped<IPlateCostService, PlateCostService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true, // Ensure token has not expired
                       ClockSkew = TimeSpan.Zero // Set clock skew to zero for exact expiration checks
                   };
               });
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}

                     }
                 });
});

builder.Services.AddDbContext<FoodPortal4Context>(
    optionsAction: options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "SQLConnection")));

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactCORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactCORS");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
