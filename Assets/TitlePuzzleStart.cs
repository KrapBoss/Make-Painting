using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ������ ���� ȭ���� �����ְ�, ������ ���� UI�� ǥ���մϴ�.
/// </summary>
public class TitlePuzzleStart : MonoBehaviour
{
    public Button button_Start;
    public Button button_Close;
    public Image image_Sample;
    public TMP_Text text_Title;

    public void Show(PuzzleSO so)
    {
        gameObject.SetActive(true);

        if (button_Start.onClick.GetPersistentEventCount() == 0) button_Start.onClick.AddListener(StartGame);
        if (button_Close.onClick.GetPersistentEventCount() == 0) button_Close.onClick.AddListener(Close);

        image_Sample.sprite = so.SampleImage;
        text_Title.text = so.Puzzle.name;
    }

    //���� ���� �ҷ��� ������ �����մϴ�.
    public void StartGame()
    {

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
