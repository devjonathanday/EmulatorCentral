using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleSelect : MonoBehaviour
{
    public ListView listView;
    public AnimationCurve scrollCurve;
    public int currentIndex = 0;

    public float spacing;
    public float elementWidth;
    private float initialOffset = 0;

    public float scrollDuration;

    private void Start()
    {
        Refresh();
        initialOffset = -(elementWidth / 2);
        transform.localPosition = GetScrollPosition();
        //StartCoroutine(SetScrollPosition());
    }

    //private IEnumerator SetScrollPosition()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    //if (transform.childCount % 2 == 0)
    //    //{
    //        initialOffset = -(elementWidth / 2);
    //    //}
    //    StartCoroutine(MoveToPosition());
    //}

    private void Update()
    {
        if (UIController.instance.inputEnabled)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                MoveRight();
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                MoveLeft();
            }
            if(Input.GetButtonDown("Submit"))
            {
                UIController.instance.ShowGames(HubManagerData.instance.consoles[currentIndex].id);
            }
        }
    }

    private Vector3 GetScrollPosition()
    {
        return new Vector3((-(elementWidth + spacing) * currentIndex) + initialOffset, transform.localPosition.y, transform.localPosition.z);
    }

    private void MoveLeft()
    {
        if (currentIndex >= transform.childCount - 1) return;
        currentIndex++;
        StartCoroutine(MoveToPosition());
    }

    private void MoveRight()
    {
        if (currentIndex == 0) return;
        currentIndex--;
        StartCoroutine(MoveToPosition());
    }

    private IEnumerator MoveToPosition()
    {
        UIController.instance.DisableInput();
        var startPosition = transform.localPosition;
        var endPosition = GetScrollPosition();

        for (float t = 0; t < scrollDuration; t += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, scrollCurve.Evaluate(t / scrollDuration));
            yield return null;
        }

        transform.localPosition = endPosition;
        UIController.instance.EnableInput();
    }

    public void Refresh()
    {
        List<ListItemData> data = new List<ListItemData>();

        foreach(var console in HubManager.consoleData.Values)
        {
            data.Add(new ListItemData
            {
                id = console.id,
                text = console.name,
                sprite = console.icon
            });
        }

        listView.Build(data, SetupButton);
    }

    public void SetupButton(List<ListItem> data)
    {
        foreach(var item in data)
        {
            var convertedItem = (ConsoleItem)item;
            convertedItem.SetAccentColors(HubManager.consoleData[item.id].accentColor);

            //convertedItem.viewButton.onClick.AddListener();
        }
    }
}
