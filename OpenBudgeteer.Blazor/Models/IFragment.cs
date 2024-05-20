using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OpenBudgeteer.Blazor.Models;

public interface IFragment { }

public static class FragmentExtension
{
    public static RenderFragment CreateRenderFragmentFrom<T>(Action? callback = null,
        Dictionary<string, object>? parameters = null) where T : IFragment, new()
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(T));

            if (callback is not null) builder.AddAttribute(1, "CallbackMethod", callback);
            if (parameters is not null) builder.AddAttribute(2, "Parameters", parameters);

            builder.CloseComponent();
        };
    }

    public static RenderFragment CreateRenderFragmentFrom<T, TData>(Action<TData>? callback = null,
        Dictionary<string, object>? parameters = null) 
        where T : IFragment, new()
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(T));

            if (callback is not null) builder.AddAttribute(1, "CallbackMethod", callback);
            if (parameters is not null) builder.AddAttribute(2, "Parameters", parameters);

            builder.CloseComponent();
        };
    }

    public static RenderFragment CreateRenderFragmentFrom<T>(Dictionary<string, object>? parameters,
        Action? callback = null) where T : IFragment, new()
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(T));

            if (callback is not null) builder.AddAttribute(1, "CallbackMethod", callback);

            if (parameters is not null)
            {
                var index = 2;

                foreach (var parameter in parameters)
                {
                    builder.AddComponentParameter(index++, parameter.Key, parameter.Value);
                }
            }

            builder.CloseComponent();
        };
    }

    public static RenderFragment CreateRenderFragmentFrom<T, TData>(Dictionary<string, object>? parameters,
        Action<TData>? callback = null)
    where T : IFragment, new()
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(T));

            if (callback is not null) builder.AddAttribute(1, "CallbackMethod", callback);

            if (parameters is not null)
            {
                var index = 2;

                foreach (var parameter in parameters)
                {
                    builder.AddComponentParameter(index++, parameter.Key, parameter.Value);
                }
            }

            builder.CloseComponent();
        };
    }
}