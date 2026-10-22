using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private string dialogueTitle;

    [Tooltip("The dialogue text to display. Use three underscores (___) to separate pages.")]
    [TextArea(7, 10)]
    [SerializeField] private string dialogueText;

    [Tooltip("The speed at which the text is displayed (characters per second).")]
    [SerializeField] private float textSpeed = 10f;


    [Header("UI Objects")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueTitleLabel;
    [SerializeField] private TextMeshProUGUI dialogueTextLabel;

    private List<string> dialoguePages;
    private string currentPage;
    private int currentPageIndex = 0;
    private int currentStringIndex = 0;

    private float textDelay = 0;
    private float textTimer = 0;

    void Start()
    {
        dialoguePages = new List<string>(dialogueText.Split("___"));
        dialogueTitleLabel.text = dialogueTitle;
        textDelay = 1f / Mathf.Max(textSpeed, 0.01f);
    }

    void Update()
    {
        UpdateText();
    }

    public void Next()
    {
        // Close dialogue if there are no more pages to display
        if (currentPageIndex >= dialoguePages.Count)
        {
            dialogueCanvas.SetActive(false);
            currentPageIndex = 0;
            currentStringIndex = 0;
            return;
        }

        // Display the dialog canvas if this is the first page
        if (currentPageIndex == 0)
        {
            dialogueCanvas.SetActive(true);
        }

        currentPage = dialoguePages[currentPageIndex];
        currentPageIndex++;
    }

    private void UpdateText()
    {
        if (currentPage == null)
        {
            return;
        }
        if (currentStringIndex >= currentPage.Length)
        {
            return;
        }

        textTimer += Time.deltaTime;
        int charsToShow = Mathf.FloorToInt(textTimer / textDelay);
        for (int i = 0; i < charsToShow; i++)
        {
            if (currentStringIndex < currentPage.Length)
            {
                dialogueTextLabel.text += currentPage[currentStringIndex];
                currentStringIndex++;
            }
        }

        textTimer -= charsToShow * textDelay;
    }
}
