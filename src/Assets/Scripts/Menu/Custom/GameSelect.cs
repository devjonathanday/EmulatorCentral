using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSelect : MonoBehaviour
{
    public ListView listView;
    public string currentConsoleKey;

    public float upperListBounds;
    public float lowerListBounds;

    public RectTransform listContainer;
    public RectTransform scrollContainer;
    public float listScrollLerp;
    public float listOffset;

    public Vector3 targetScrollListPos;

    public void OnEnable()
    {
        StartCoroutine(SelectFirstElementWaited());
        scrollContainer.localPosition = new Vector3(scrollContainer.localPosition.x, scrollContainer.sizeDelta.y, scrollContainer.localPosition.z);
    }

    public void Refresh(string consoleKey)
    {
        if (!HubManager.romData.ContainsKey(consoleKey)) return;
        //TODO Display a message indicating there were no games at this directory.

        currentConsoleKey = consoleKey;
        List<ListItemData> data = new List<ListItemData>();

        foreach (var rom in HubManager.romData[consoleKey])
        {
            data.Add(new ListItemData
            {
                id = rom.fileName,
                text = rom.displayName,
                customData = new Dictionary<string, object>
                {
                    {rom.fileName, rom}
                }
            });
        }

        listView.Build(data, SetupButton);
    }

    private void Update()
    {
        scrollContainer.localPosition = Vector3.Lerp(scrollContainer.localPosition, targetScrollListPos, listScrollLerp);
        if(Input.GetAxis("Vertical") != 0)
        {
            SetTargetListPosition();
        }
    }

    public float GetScrollPosition(float scalar)
    {
        float endPos = listContainer.sizeDelta.y - scrollContainer.sizeDelta.y;
        return endPos * scalar;
    }

    public void SetTargetListPosition()
    {
        if (listContainer.sizeDelta.y < scrollContainer.sizeDelta.y ||
           EventSystem.current.currentSelectedGameObject == null)
        {
            targetScrollListPos = Vector3.zero;
            return;
        }
        var selected = EventSystem.current.currentSelectedGameObject.transform;
        var targetListPosition = GetScrollPosition((float)selected.GetComponent<ListItem>().listIndex / listContainer.childCount) + listOffset;
        targetScrollListPos = new Vector3(scrollContainer.localPosition.x, targetListPosition, scrollContainer.localPosition.z);
    }

    public IEnumerator SelectFirstElementWaited()
    {
        yield return new WaitForSeconds(.1f);
        SelectFirstElement();
    }

    public void SetupButton(List<ListItem> data)
    {
        foreach (var item in data)
        {
            var convertedItem = (GameItem)item;
            convertedItem.SetupRom(HubManager.consoleData[currentConsoleKey].accentColor, currentConsoleKey, ((RomData)item.customData[item.id]).fileName);
        }
    }

    private void SelectFirstElement()
    {
        EventSystem.current.SetSelectedGameObject(listView.container.GetChild(0).gameObject);
        SetTargetListPosition();
    }
}
