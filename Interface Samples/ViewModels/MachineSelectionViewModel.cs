using System;

public interface IMachineSelectViewModel : IViewModel
{
    ISettingsScreenViewModel SettingsScreenViewModel { get; }
    void Continue();
    void Back();
}


public class MachineSelectViewModel : IMachineSelectViewModel
{
    [NonSerialized] private readonly MachineSelectController _controller;

    public MachineSelectViewModel(MachineSelectController machineSelectController)
    {
        _controller = machineSelectController;
        Init();
    }

    public void Continue()
    {
        _controller.GoToShop();
    }
    
    public void Back()
    {
        _controller.BackToMainMenu();
    }

    public ISettingsScreenViewModel SettingsScreenViewModel { get; private set; }

    public void Init()
    {
        SettingsScreenViewModel = new SettingsScreenViewModel(_controller.LocalizationController);
    }

    public void Dispose()
    {
    }
}