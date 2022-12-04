﻿using System;
using System.Linq;
using System.Reflection;
using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using DeepDungeonDex.Hooks;
using DeepDungeonDex.Models;
using DeepDungeonDex.Requests;
using DeepDungeonDex.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DeepDungeonDex
{
    public class Main : IDalamudPlugin
    {
        public string Name => "DeepDungeonDex";
        
        private IServiceProvider _provider;
        private AddonAgent _addon;

        public Main(DalamudPluginInterface pluginInterface, Framework framework, CommandManager manager, TargetManager target, Condition condition, DataManager gameData, ClientState state)
        {
            _provider = BuildProvider(this, pluginInterface, framework, manager, target, condition, gameData, state);
            _provider.GetRequiredService<Data>();
            _provider.GetRequiredService<Language>();
            _provider.GetRequiredService<StorageHandler>().GetInstance<Configuration>()!.OnSizeChange += pluginInterface.UiBuilder.RebuildFonts;
            
            pluginInterface.UiBuilder.BuildFonts += BuildFont;
            var sys = LoadWindows();
            pluginInterface.UiBuilder.Draw += sys.Draw;
            _addon = new AddonAgent(framework);
        }

        public void BuildFont()
        {
            _provider.GetRequiredService<Font>().BuildFonts(_provider.GetRequiredService<StorageHandler>().GetInstance<Configuration>()?.FontSizeScaled ?? 1f);
        }

        public void Dispose()
        {
            _addon.Dispose();
            _provider.GetRequiredService<StorageHandler>().GetInstance<Configuration>()!.OnSizeChange -= _provider.GetRequiredService<DalamudPluginInterface>().UiBuilder.RebuildFonts;
            _provider.GetRequiredService<DalamudPluginInterface>().UiBuilder.BuildFonts -= BuildFont;
            _provider.GetRequiredService<Data>().Dispose();
            _provider.GetRequiredService<Language>().Dispose();
            _provider.GetRequiredService<StorageHandler>().Dispose();
            _provider.GetRequiredService<CommandHandler>().Dispose();
            _provider.GetRequiredService<WindowSystem>().RemoveAllWindows();
            _provider.GetRequiredService<Font>().Dispose();
        }

        public WindowSystem LoadWindows()
        {
            var sys = _provider.GetRequiredService<WindowSystem>();
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Window)))
                .ToList()
                .ForEach(t => sys.AddWindow((Window)ActivatorUtilities.CreateInstance(_provider, t)!));
            return sys;
        }

        private static IServiceProvider BuildProvider(Main main, DalamudPluginInterface pluginInterface, Framework framework, CommandManager manager, TargetManager target, Condition condition, DataManager gameData, ClientState state)
        {
            return new ServiceCollection()
                .AddSingleton(pluginInterface)
                .AddSingleton(framework)
                .AddSingleton(manager)
                .AddSingleton(target)
                .AddSingleton(condition)
                .AddSingleton(gameData)
                .AddSingleton(state)
                .AddSingleton(new WindowSystem("DeepDungeonDex"))
                .AddSingleton(main)
                .AddSingleton(provider => ActivatorUtilities.CreateInstance<StorageHandler>(provider))
                .AddSingleton(provider => ActivatorUtilities.CreateInstance<Data>(provider))
                .AddSingleton(provider => ActivatorUtilities.CreateInstance<Language>(provider))
                .AddSingleton(provider => ActivatorUtilities.CreateInstance<Font>(provider))
                .AddSingleton(provider => ActivatorUtilities.CreateInstance<CommandHandler>(provider))
                .BuildServiceProvider();
        }
    }
}
