using System.Windows;
using Fluent;
using MahApps.Metro.Controls;
using Prism.Regions;
using WpfSampleApp.Constants;
using WpfSampleApp.Contracts.Services;

namespace WpfSampleApp.Views;

public partial class ShellWindow : MetroWindow, IRibbonWindow
{
    public static readonly DependencyProperty TitleBarProperty;
    private static readonly DependencyPropertyKey TitleBarPropertyKey;

    static ShellWindow()
    {
        TitleBarPropertyKey = DependencyProperty.RegisterReadOnly(nameof(TitleBar), typeof(RibbonTitleBar), typeof(ShellWindow), new PropertyMetadata());
        TitleBarProperty = TitleBarPropertyKey.DependencyProperty;
    }

    public ShellWindow(IRegionManager regionManager, IRightPaneService rightPaneService)
    {
        InitializeComponent();
        RegionManager.SetRegionName(menuContentControl, Regions.Main);
        RegionManager.SetRegionManager(menuContentControl, regionManager);
        rightPaneService.Initialize(splitView, rightPaneContentControl);
        navigationBehavior.Initialize(regionManager);
        tabsBehavior.Initialize(regionManager);
    }

    public RibbonTitleBar TitleBar
    {
        get => (RibbonTitleBar)GetValue(TitleBarProperty);
        private set => SetValue(TitleBarPropertyKey, value);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var window = sender as MetroWindow;
        TitleBar = window.FindChild<RibbonTitleBar>("RibbonTitleBar");
        TitleBar.InvalidateArrange();
        TitleBar.UpdateLayout();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        tabsBehavior.Unsubscribe();
    }
}
