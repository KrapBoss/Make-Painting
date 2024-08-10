using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 지정된 퍼즐의 샘플 화면을 보여주고, 시작을 위한 UI를 표시합니다.
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

    //다음 씬을 불러와 게임을 시작합니다.
    public void StartGame()
    {

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
