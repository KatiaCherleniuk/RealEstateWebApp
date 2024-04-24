using RealEstateWebApp.Business;
using RealEstateWebApp.Business.Identity;
using RealEstateWebApp.Models.User;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
//using RealEstateWebApp.UI.Components.ToastComponent;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstateWebApp.DataAccess.Repositories.Categories;
using RealEstateWebApp.DataAccess.Repositories.Files;
using RealEstateWebApp.DataAccess.Repositories.Identity;
using RealEstateWebApp.DataAccess.Repositories.Properties;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.DataAccess.Repositories.UserSettings;
using RealEstateWebApp.UI.Components.ToastComponent;
using RealEstateWebApp.DataAccess.Repositories.Stats;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using RealEstateWebApp.DataAccess.UserComments;





var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
/*builder.Services.AddBusiness(builder.Configuration); // Assuming Configuration is defined elsewhere*/
AddUiServices(builder.Services);
AddIdentity(builder.Services);
AddBusiness(builder.Services);
AddDataAccess(builder.Services);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapFallbackToPage("/admin/category/{CategoryId:int}/{*Filter}", "/_Host");
//app.MapFallbackToPage("/category/{CategoryId:int}/{*Filter}", "/_Host");
//app.MapFallbackToPage("/admin/{*clientroutes:nonfile}", "/admin/dashboard");
//app.MapFallbackToPage("/Admin/RecordsAsTable");


app.UseRequestLocalization(AddLocalizationOptions());

UseIdentity(app);

app.Run();

void AddUiServices(IServiceCollection services, Action<IndicatorOptions> optionsBuilder = null)
{
    services.AddProgressIndicatorLite();
    /*services.AddBlazorToast()*/
    services.AddBlazoredModal();
    services.AddScoped<IToastService, ToastService>();
    services.AddScoped<FiltersWatcher>();
    var options = new IndicatorOptions();
    optionsBuilder?.Invoke(options);
    services.AddScoped<IIndicatorService, IndicatorService>(_ => new IndicatorService
    {
        Options = options
    });
}

void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddLocalization();
    services.AddControllersWithViews();
    services.AddControllers();
    services.AddServerSideBlazor();
}

void AddBusiness(IServiceCollection services)
{
    /*
    services.AddSingleton<RecordValueService>();
    services.AddScoped<UserSettingsService>();
    services.AddScoped<RecordFieldsUserSettingsService>();*/
    services.AddSingleton<FileService>();
    services.AddSingleton<StatisticService>();
    services.AddSingleton<CategoryService>();
    services.AddSingleton<PropertyService>();
    services.AddSingleton<PropertyValueService>();
    services.AddSingleton<RecordService>();
    services.AddScoped<UserService>();
    services.AddScoped<UserCommentsService>();
}
void AddDataAccess(IServiceCollection services)
{
    services.AddSingleton<ICategoryRepository, CategoryRepository>();
    services.AddSingleton<IPropertyRepository, PropertyRepository>();
    services.AddSingleton<IPropertyValueRepository, PropertyValueRepository>();
    services.AddSingleton<IRecordValueRepository, RecordValueRepository>();
    services.AddSingleton<IRecordRepository, RecordRepository>();
    services.AddSingleton<IRoleRepository, RoleRepository>();
    services.AddSingleton<IUserRepository, UserRepository>();
    services.AddSingleton<IFileRepository, FileRepository>();
    services.AddSingleton<IUserSettingsRepository, UserSettingsRepository>();
    services.AddSingleton<IStatisticRepository, StatisticRepository>();
    services.AddSingleton<IUserCommentsRepository, UserCommentsRepository>();
}

void AddIdentity(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddUserStore<UserStore>()
        .AddRoleStore<RoleStore>();

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
        options.Secure = CookieSecurePolicy.Always;
    });
    services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
    services.AddHttpContextAccessor();
    services.AddScoped<ISessionUserResolver, SessionUserResolver>();
}

void UseIdentity(IApplicationBuilder app)
{
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();
}

RequestLocalizationOptions AddLocalizationOptions()
{
    var supportedCultures = new[] { "uk-UA", "en-US", "pl-PL" };

    var localizationOptions = new RequestLocalizationOptions()
        .SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    return localizationOptions;
}


