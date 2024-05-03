using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace OpenBudgeteer.Blazor;

public class PreferenceManager(ILocalStorageService localStorageService)
{
    private const string StorageKey = "budget-preferences";

    public async Task<Preferences> LoadPreferencesAsync()
    {
        var preferences = await localStorageService.GetItemAsync<Preferences>(StorageKey);

        return preferences ?? new();
    }

    public async Task SavePreferencesAsync(Preferences preference)
    {
        await localStorageService.SetItemAsync(StorageKey, preference);
    }

    public async Task<Preferences> ModifyPreferencesAsync(Action<Preferences> modifiedPreference)
    {
        var userPreference = await LoadPreferencesAsync();

        modifiedPreference.Invoke(userPreference);

        await SavePreferencesAsync(userPreference);

        return userPreference;
    }

    public async Task ClearPreferences()
    {
        await Task.Delay(10);

        await localStorageService.RemoveItemAsync(StorageKey);
    }

}

public sealed class Preferences
{
    public bool DarkMode { get; set; }
    public bool MiniDrawer { get; set; }
}
