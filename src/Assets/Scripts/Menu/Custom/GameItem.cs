using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : ListItem
{
    public void SetupRom(Color color, string consoleKey, string romPath)
    {
        label.color = Color.white;
        //Get the accent color for the console
        Color accentColor = HubManager.consoleData[consoleKey].accentColor;
        Color darkenedColor = new Color(accentColor.r, accentColor.g, accentColor.b, 0.5f);
        //Create a new color block for the button
        var colorBlock = new UnityEngine.UI.ColorBlock();
        colorBlock.normalColor = darkenedColor;
        colorBlock.highlightedColor = accentColor;
        colorBlock.selectedColor = accentColor;
        colorBlock.pressedColor = darkenedColor;
        colorBlock.disabledColor = darkenedColor;
        colorBlock.fadeDuration = 0.1f;
        colorBlock.colorMultiplier = 1;
        //Set the colors from the color block
        viewButton.colors = colorBlock;

        viewButton.onClick.AddListener(() =>
        {
            if (!UIController.instance.inputEnabled) return;
            HubManager.consoleData[consoleKey].LoadROM(romPath);
        });
    }
}