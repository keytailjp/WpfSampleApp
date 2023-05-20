using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using WpfSampleApp.Contracts.Services;
using WpfSampleApp.Models;

namespace WpfSampleApp.ViewModels;

// TODO: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
public class SettingsViewModel : BindableBase, INavigationAware
{
    private readonly AppConfig _appConfig;
    private readonly IApplicationInfoService _applicationInfoService;
    private readonly ISystemService _systemService;
    private readonly IThemeSelectorService _themeSelectorService;
    private ICommand _privacyStatementCommand;
    private ICommand _setThemeCommand;
    private AppTheme _theme;
    private string _versionDescription;

    public SettingsViewModel(AppConfig appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService)
    {
        _appConfig = appConfig;
        _themeSelectorService = themeSelectorService;
        _systemService = systemService;
        _applicationInfoService = applicationInfoService;
    }

    public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new DelegateCommand(OnPrivacyStatement));

    public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new DelegateCommand<string>(OnSetTheme));

    public AppTheme Theme
    {
        get { return _theme; }
        set { SetProperty(ref _theme, value); }
    }

    public string VersionDescription
    {
        get { return _versionDescription; }
        set { SetProperty(ref _versionDescription, value); }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
        => true;

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
    }

    private void OnPrivacyStatement()
        => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);

    private void OnSetTheme(string themeName)
    {
        var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
        _themeSelectorService.SetTheme(theme);
    }
}
