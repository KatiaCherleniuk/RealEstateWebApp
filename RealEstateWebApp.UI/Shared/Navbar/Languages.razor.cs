using System;
using System.Globalization;
using System.Threading.Tasks;
using RealEstateWebApp.UI.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace RealEstateWebApp.UI.Shared.Navbar
{
    public partial class Languages : ComponentBase
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }


        private CultureInfo[] supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("uk-UA"),
            new CultureInfo("pl-PL")
        };

        private CultureInfo _currentCulture;
        private CultureInfo CurrentCulture
        {
            get => _currentCulture == null ? new CultureInfo("uk-UA") : _currentCulture;
            set => _currentCulture = value;
        }

        protected override void OnInitialized()
        {
            CurrentCulture = CultureInfo.CurrentCulture;
        }

        protected async Task ChangeLocalization(CultureInfo culture)
        {
            if (CultureInfo.CurrentUICulture != culture)
            {
                CurrentCulture = culture;
                var uri = new Uri(Navigation.Uri)
                    .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var cultureEscaped = Uri.EscapeDataString(culture.Name);
                var uriEscaped = Uri.EscapeDataString(uri);

                Navigation.NavigateTo(
                    $"Culture/Set?culture={cultureEscaped}&" +
                    $"redirectUri={uriEscaped}", forceLoad: true);
            }
        }
    }
}