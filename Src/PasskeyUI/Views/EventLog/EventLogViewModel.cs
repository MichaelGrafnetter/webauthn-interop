using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.Events;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class EventLogViewModel : BindableBase, IEventLogViewModel
{
    private readonly IDialogService _dialogService;

    public EventLogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        LoadEventsCommand = new DelegateCommand(OnLoadEvents);
    }

    public ObservableCollection<WebAuthnOperation> Operations { get; } = [];

    public ICommand LoadEventsCommand { get; }

    private void OnLoadEvents()
    {
        try
        {
            Operations.Clear();

            var events = WebAuthnEventReader.ReadEvents();
            var operations = WebAuthnOperationBuilder.Build(events);

            foreach (var operation in operations)
            {
                Operations.Add(operation);
            }
        }
        catch (Exception ex)
        {
            DialogParameters parameters = new($"Message={ex.Message}");
            _dialogService.ShowDialog(nameof(NotificationDialog), parameters);
        }
    }
}
