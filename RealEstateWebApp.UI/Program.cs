

using System.Reflection;
using RealEstateWebApp.Business;
using RealEstateWebApp.Business.Identity;
using RealEstateWebApp.Models.User;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
//using RealEstateWebApp.UI.Components.ToastComponent;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstateWebApp.DataAccess.Repositories.Categories;
using RealEstateWebApp.DataAccess.Repositories.Files;
using RealEstateWebApp.DataAccess.Repositories.Identity;
using RealEstateWebApp.DataAccess.Repositories.Properties;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.DataAccess.Repositories.UserSettings;
using RealEstateWebApp.UI.Components.ToastComponent;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();
/*builder.Services.AddBusiness(builder.Configuration); // Assuming Configuration is defined elsewhere*/
builder.Services.AddProgressIndicatorLite();
builder.Services.AddBlazorToast();
builder.Services.AddBlazoredModal();
//AddUiServices(builder.Services);
AddProgressIndicatorLite(builder.Services);
AddIdentity(builder.Services);
AddBusiness(builder.Services);
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<IPropertyRepository, PropertyRepository>();
builder.Services.AddSingleton<IPropertyValueRepository, PropertyValueRepository>();
builder.Services.AddSingleton<IRecordValueRepository, RecordValueRepository>();
builder.Services.AddSingleton<IRecordRepository, RecordRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IFileRepository, FileRepository>();
builder.Services.AddSingleton<IUserSettingsRepository, UserSettingsRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
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

/*app.UseAuthentication();
app.UseAuthorization();*/

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapFallbackToPage("/category/{CategoryId:int}/{*Filter}", "/_Host");
UseIdentity(app);

app.Run();

/*void AddUiServices(IServiceCollection services)
{
    services.AddScoped<CategoryListWatcher>();
    services.AddScoped<FiltersWatcher>();
    services.AddScoped<ScrollObserver>();
    services.AddScoped<UserSettings>();
}
*/

void AddBusiness(IServiceCollection services)
{
    /*services.AddSingleton<CategoryService>();
    services.AddSingleton<PropertyService>();
    services.AddSingleton<PropertyValueService>();
    services.AddSingleton<RecordValueService>();
    services.AddSingleton<RecordService>();
    services.AddSingleton<FileService>();
    services.AddScoped<UserSettingsService>();
    services.AddScoped<RecordFieldsUserSettingsService>();*/

    services.AddScoped<UserService>();

    
}
void AddProgressIndicatorLite(IServiceCollection services, Action<IndicatorOptions> optionsBuilder = null)
{
    var options = new IndicatorOptions();
    optionsBuilder?.Invoke(options);
    services.AddScoped<IIndicatorService, IndicatorService>(_ => new IndicatorService
    {
        Options = options
    });
}
void AddIdentity(IServiceCollection services)
{
    //services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
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



