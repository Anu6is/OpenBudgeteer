using Microsoft.AspNetCore.Components;
using System;

namespace OpenBudgeteer.Blazor.Services;

public class DrawerService
{
    public event Action<RenderFragment>? OnRenderFragmentChanged;
    public event Action<string>? OnDrawerStateChanged;

    private RenderFragment? _renderFragment;

    public bool DrawerOpen { get; private set; }

    public RenderFragment? RenderFragment
    {
        get => _renderFragment;
        set
        {
            if (_renderFragment == value) return;

            _renderFragment = value;
            OnRenderFragmentChanged?.Invoke(value!);
        }
    }

    // Toggles the state of the drawer and invokes the OnDrawerStateChanged event with the provided title.
    public void ToggleDrawer(string? title = null)
    {
        DrawerOpen = !DrawerOpen;
        OnDrawerStateChanged?.Invoke(title ?? string.Empty);
    }
}