using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI label;
    public Image icon;
    public Dictionary<string, object> customData;
    public Action viewAction;
    public Button viewButton;
    public int listIndex;

    private void OnDestroy()
    {
        if (viewButton != null)
        {
            viewButton.onClick.RemoveAllListeners();
        }
    }
}

public class ListItemData
{
    public string id;
    public string text;
    public Sprite sprite;
    public Dictionary<string, object> customData;
    public Action viewAction;
}