using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public static class HubManager
{
    //List of data for the console (name, key, accent color, etc)
    public static Dictionary<string, ConsoleData> consoleData = new Dictionary<string, ConsoleData>();
    //Individual data for each ROM, denoted by the console key
    public static Dictionary<string, List<RomData>> romData = new Dictionary<string, List<RomData>>();
    public static void RefreshConsoles()
    {
        consoleData.Clear();
        foreach (var console in HubManagerData.instance.consoles)
        {
            consoleData.Add(console.id, console);
        }
    }

    public static void RefreshGames()
    {
        romData.Clear();
        foreach (var console in HubManagerData.instance.consoles)
        {
            if (console.romPath == string.Empty) continue;
            var info = new DirectoryInfo(console.romPath);
            var fileInfo = info.GetFiles();

            List<RomData> consoleRoms = new List<RomData>();

            foreach (var file in fileInfo)
            {
                //Filter the file by the console's supported file types
                var supportedFileTypes = console.supportedFileTypes.Split(',');
                bool supported = false;
                foreach (var str in supportedFileTypes)
                {
                    if (str.Contains(Path.GetExtension(file.FullName).ToLower()))
                    {
                        supported = true;
                        break;
                    }
                }
                if (!supported) continue;

                consoleRoms.Add(new RomData
                {
                    fileName = file.Name,
                    filePath = file.FullName,
                    consoleKey = console.id,
                    displayName = Path.GetFileNameWithoutExtension(file.FullName)
                });
            }

            romData.Add(console.id, consoleRoms);
        }
    }

    public static void WriteConfigToFile()
    {
        var path = Application.streamingAssetsPath + "\\config.cfg";
        List<string> lines = new List<string>();
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        foreach (var console in consoleData.Values)
        {
            lines.Add($"{console.id}|{console.name}|{console.supportedFileTypes}|{console.emulatorPath}|{console.romPath}|{console.iconPath}|#{ColorUtility.ToHtmlStringRGB(console.accentColor)}");
        }
        File.WriteAllLines(path, lines.ToArray());

        RefreshConsoles();
        RefreshGames();
    }
}

[System.Serializable]
public class ConsoleData
{
    public string id;
    public string name;

    [Header("Lowercase CSV")]
    public string supportedFileTypes;

    public Sprite icon;
    public string iconPath;
    public Color accentColor;

    [Header("Insert [ROM] for file path.")]
    public string emulatorPath;
    public string romPath;

    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

    public void LoadROM(string romName)
    {
        var args = romPath;
        args += $"\\{romName}";

        var p = new System.Diagnostics.Process();
        p.StartInfo.Arguments = $"\"{args}\"";
        p.StartInfo.FileName = emulatorPath;
        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
        p.Start();
        //Hide the window
        var hwnd = GetActiveWindow();
        ShowWindow(hwnd, 0);
        //Wait for the emulator to close
        p.WaitForExit();
        p.Close();
        //Show the window
        ShowWindow(hwnd, 5);
    }
}

[System.Serializable]
public class RomData
{
    public string fileName;
    public string filePath;
    public string displayName;
    public string consoleKey;
}