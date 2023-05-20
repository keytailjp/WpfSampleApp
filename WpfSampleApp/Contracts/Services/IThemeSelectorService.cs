using WpfSampleApp.Models;

namespace WpfSampleApp.Contracts.Services;

public interface IThemeSelectorService
{
    AppTheme GetCurrentTheme();

    void InitializeTheme();

    void SetTheme(AppTheme theme);
}
