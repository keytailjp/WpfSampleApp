namespace WpfSampleApp.Contracts.Services;

public interface IPersistAndRestoreService
{
    void PersistData();

    void RestoreData();
}
