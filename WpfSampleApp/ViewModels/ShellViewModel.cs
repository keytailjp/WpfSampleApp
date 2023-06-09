﻿using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using WpfSampleApp.Constants;
using WpfSampleApp.Contracts.Services;

namespace WpfSampleApp.ViewModels;

public class ShellViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private readonly IRightPaneService _rightPaneService;
    private ICommand _loadedCommand;
    private IRegionNavigationService _navigationService;
    private ICommand _unloadedCommand;

    public ShellViewModel(IRegionManager regionManager, IRightPaneService rightPaneService)
    {
        _regionManager = regionManager;
        _rightPaneService = rightPaneService;
    }

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

    private void OnLoaded()
    {
        _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        _navigationService.RequestNavigate(PageKeys.Main);
    }

    private void OnUnloaded()
    {
        _regionManager.Regions.Remove(Regions.Main);
        _rightPaneService.CleanUp();
    }
}
