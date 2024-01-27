using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text goalText;

    public string Title
    {
        get => title.text;
        set => title.text = value;
    }

    public string GoalText
    {
        get => goalText.text;
        set => goalText.text = value;
    }

    public void Initialize()
    {
        
    }

    public void UpdateView(string goalString)
    {
        GoalText = goalString;
    }
}
