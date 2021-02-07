using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirectoryEditor : MonoBehaviour
{
    public TMP_InputField IDKey;
    public TMP_InputField DisplayName;
    public TMP_InputField SupportedFileTypes;
    public TextMeshProUGUI EmulatorPath;
    public TextMeshProUGUI ROMPath;
    public TextMeshProUGUI ImagePath;
    public ColorPicker AccentColor;

    public void SaveChanges()
    {
        #region Null Checks / Error Messages

        if (IDKey.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please enter an ID Key.");
            return;
        }
        else if (HubManager.consoleData.ContainsKey(IDKey.text))
        {
            ErrorMessage.ShowErrorMessage("Error", "Duplicate ID Key detected. Please enter a unique ID Key.");
            return;
        }

        if (DisplayName.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please enter a Display Name.");
            return;
        }
        if (SupportedFileTypes.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please enter at least one ROM file type.");
            return;
        }
        if (EmulatorPath.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please specify an emulator path.");
            return;
        }
        if (ROMPath.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please specify a ROM path.");
            return;
        }
        if (ImagePath.text == string.Empty)
        {
            ErrorMessage.ShowErrorMessage("Error", "Please specify an image path.");
            return;
        }

        #endregion

        HubManager.consoleData.Add(IDKey.text, new ConsoleData
        {
            id = IDKey.text,
            name = DisplayName.text,
            supportedFileTypes = SupportedFileTypes.text,
            emulatorPath = EmulatorPath.text,
            romPath = ROMPath.text,
            iconPath = ImagePath.text,
            accentColor = AccentColor.GetColor()
        });
        HubManager.WriteConfigToFile();
    }
}