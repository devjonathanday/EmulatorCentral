using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class HubManagerData : MonoBehaviour
{
    public static HubManagerData instance;

    public List<ConsoleData> consoles;

    private void Awake()
    {
        instance = this;
        StartCoroutine(InitializeDatabase());
    }

    private IEnumerator InitializeDatabase()
    {
        consoles.Clear();
        var path = Application.streamingAssetsPath + "\\config.cfg";
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        else
        {
            var chunks = File.ReadAllLines(path);
            foreach (var chunk in chunks)
            {
                var info = chunk.Split('|');

                var iconPath = info[5];

                var www = new WWW("file://" + iconPath);
                yield return www;
                Texture2D tex = new Texture2D(512, 512);
                www.LoadImageIntoTexture(tex);

                ColorUtility.TryParseHtmlString(info[6], out Color color);

                consoles.Add(new ConsoleData
                {
                    id = info[0],
                    name = info[1],
                    supportedFileTypes = info[2],
                    emulatorPath = info[3],
                    romPath = info[4],
                    icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)),
                    iconPath = iconPath,
                    accentColor = color
                });
            }
        }

        HubManager.RefreshConsoles();
        HubManager.RefreshGames();
    }
}