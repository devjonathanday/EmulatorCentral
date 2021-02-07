using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilePathPicker : MonoBehaviour
{
    public enum SelectionType { File, Folder }

    public string title;
    public string[] searchFileTypes;
    public SelectionType type;
    public TextMeshProUGUI pathDisplay;
    public Button browseButton;

    private void Start()
    {
        switch (type)
        {
            case SelectionType.File:
                browseButton.onClick.AddListener(OpenFile);
                break;
            case SelectionType.Folder:
                browseButton.onClick.AddListener(OpenFolder);
                break;
        }
    }

    void OpenFile()
    {
        var extensions = new[] {
            new ExtensionFilter("Application", searchFileTypes),
            new ExtensionFilter("All Files", "*" ),
        };
        var selectedPath = StandaloneFileBrowser.OpenFilePanel(title, System.IO.Directory.GetCurrentDirectory(), extensions, false);
        if (selectedPath.Length > 0)
            pathDisplay.text = selectedPath[0];
    }

    void OpenFolder()
    {
        var selectedPath = StandaloneFileBrowser.OpenFolderPanel(title, System.IO.Directory.GetCurrentDirectory(), false);
        if (selectedPath.Length > 0)
            pathDisplay.text = selectedPath[0];
    }

    private void OnDestroy()
    {
        browseButton.onClick.RemoveAllListeners();
    }
}