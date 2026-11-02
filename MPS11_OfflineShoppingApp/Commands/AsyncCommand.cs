using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MPS11_OfflineShoppingApp.Commands;

public class AsyncCommand : ICommand
{
    private readonly Func<Task> execute;
    private readonly Func<bool>? canExecute;
    private bool isExecuting;

    public event EventHandler? CanExecuteChanged;

    public AsyncCommand(
        Func<Task> execute,
        Func<bool>? canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return !isExecuting && (canExecute?.Invoke() ?? true);
    }

    public async void Execute(object? parameter)
    {
        await ExecuteAsync();
    }

    private async Task ExecuteAsync()
    {
        if (!CanExecute(null))
            return;

        try
        {
            isExecuting = true;
            RaiseCanExecuteChanged();

            await execute();
        }
        finally
        {
            isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
