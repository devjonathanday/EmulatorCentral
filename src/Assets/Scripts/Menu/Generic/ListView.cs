using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour
{
    public Transform container;
    public ListItem listItemPrefab;
    private List<ListItem> cachedData = new List<ListItem>();

    public void Build(List<ListItemData> data, Action<List<ListItem>> postRefreshAction = null)
    {
        cachedData.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach (var item in data)
        {
            var newItem = Instantiate(listItemPrefab, container);

            if (item.id != null)
            {
                newItem.id = item.id;
                newItem.gameObject.name = item.id;
            }
            if (item.text != null) newItem.label.text = item.text;
            if (item.sprite != null) newItem.icon.sprite = item.sprite;
            if (item.customData != null) newItem.customData = item.customData;
            if (item.viewAction != null) newItem.viewAction = item.viewAction;
            newItem.listIndex = i;

            cachedData.Add(newItem);

            i++;
        }

        postRefreshAction.Invoke(cachedData);
    }
}