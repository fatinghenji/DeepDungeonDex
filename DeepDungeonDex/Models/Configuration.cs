﻿using System.IO;

namespace DeepDungeonDex.Models;

public class Configuration : IBinaryLoadable
{
    public byte Version { get; } = 2;
    public bool ClickThrough { get; set; }
    public bool HideRed { get; set; }
    public bool HideJob { get; set; }
    public bool HideFloor { get; set; }
    public bool HideSpawns { get; set; }
    public bool Debug { get; set; }
    public bool LoadAll { get; set; }
    public int Locale { get; set; } = 0;
    public int FontSize { get; set; } = 16;
    public float Opacity { get; set; } = 1f;

    [JsonIgnore] public int PrevFontSize;
    [JsonIgnore] public int PrevLocale;
    [JsonIgnore] public float RemoveScaling => 1 / ImGui.GetIO().FontGlobalScale;
    [JsonIgnore] public float FontSizeScaled => FontSize * RemoveScaling;
    [JsonIgnore] public float WindowSizeScaled => Math.Max(FontSize / 16f, 1f) * RemoveScaling;
    [JsonIgnore] public Action<Configuration>? OnChange { get; set; }
    [JsonIgnore] public Action? OnSizeChange { get; set; }

    public NamedType? Save(string path)
    {
        if (FontSize != PrevFontSize)
        {
            PrevFontSize = FontSize;
            OnSizeChange?.Invoke();
        }
        if (PrevLocale != Locale && !LoadAll)
        {
            PrevLocale = Locale;
            OnSizeChange?.Invoke();
        }
        OnChange?.Invoke(this);
        return BinarySave(path);
    }

    public void Dispose()
    {
    }

    public IBinaryLoadable StringLoad(string str)
    {
        throw new NotImplementedException();
    }

    public NamedType? BinarySave(string path)
    {
        var origPath = path;
        if(!path.EndsWith(".tmp"))
            path += ".tmp";
        Stream stream = File.Open(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
        BinaryWriter writer = new(stream);
        writer.Write(Version);
        byte flags = 0;
        flags |= (byte)(ClickThrough ? 1 << 1 : 0);
        flags |= (byte)(HideFloor ? 1 << 2 : 0);
        flags |= (byte)(HideSpawns ? 1 << 3 : 0);
        flags |= (byte)(Debug ? 1 << 4 : 0);
        flags |= (byte)(LoadAll ? 1 << 5 : 0);
        writer.Write(flags);
        writer.Write(Locale);
        writer.Write(FontSize);
        writer.Write(Opacity);
        writer.Close();
        stream.Close();
        if (File.Exists(origPath))
            File.Delete(origPath);
        File.Move(path, origPath);
        return null;
    }

    public IBinaryLoadable BinaryLoad(string path)
    {
        throw new NotImplementedException();
    }

    public Storage.Storage Load(string path)
    {
        throw new NotImplementedException();
    }

    public Storage.Storage Load(string path, string name)
    {
        throw new NotImplementedException();
    }
}