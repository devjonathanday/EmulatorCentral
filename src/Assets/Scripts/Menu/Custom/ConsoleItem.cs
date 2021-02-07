using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleItem : ListItem
{
    public void SetAccentColors(Color color)
    {
        label.color = color;
        icon.color = color;
    }
}
