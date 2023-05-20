using System.Windows.Controls;

using MahApps.Metro.Controls;

using Prism.Regions;

namespace WpfSampleApp.Contracts.Services;

public interface IRightPaneService
{
    event EventHandler PaneClosed;

    event EventHandler PaneOpened;

    void CleanUp();

    void Initialize(SplitView splitView, ContentControl rightPaneContentControl);

    void OpenInRightPane(string pageKey, NavigationParameters navigationParameters = null);
}
