using kursovayaApi.Models;
using Microsoft.EntityFrameworkCore;
using kursovayaApi.Models.Services;
using kursovayaApi.Models.Abstractions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    setup =>
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {jwtSecurityScheme, Array.Empty<string>() }
        });
    });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddDbContext<KursovayaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICommonService<BatchStepExecution, int>, BatchStepExecutionService>();
builder.Services.AddScoped<ICommonService<Equipment, int>, EquipmentService>();
builder.Services.AddScoped<ICommonService<LabTest, int>, LabTestService>();
builder.Services.AddScoped<ICommonService<MaterialBatch, int>, MaterialBatchService>();
builder.Services.AddScoped<ICommonService<ProductionBatch, int>, ProductionBatchService>();
builder.Services.AddScoped<ICommonService<Product, int>, ProductService>();
builder.Services.AddScoped<ICommonService<RawMaterial, int>, RawMaterialService>();
builder.Services.AddScoped<ICommonService<RecipeComponent, int>, RecipeComponentService>();
builder.Services.AddScoped<ICommonService<Recipe, int>, RecipeService>();
builder.Services.AddScoped<ICommonService<TechCard, int>, TechCardService>();
builder.Services.AddScoped<ICommonService<TechStep, int>, TechStepService>();
builder.Services.AddScoped<ICommonService<User, int>, UsersService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
