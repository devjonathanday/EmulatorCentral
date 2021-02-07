using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{
    private static ErrorMessage instance;

    public TextMeshProUGUI title;
    public TextMeshProUGUI message;
    public Button okayButton;
    public Animator animator;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        okayButton.onClick.AddListener(Close);
    }

    public static void ShowErrorMessage(string title, string message)
    {
        instance.title.text = title;
        instance.message.text = message;
        instance.animator.SetTrigger("Show");
    }
    public void Close()
    {
        animator.SetTrigger("Hide");
    }

    private void OnDestroy()
    {
        okayButton.onClick.RemoveAllListeners();
    }
}