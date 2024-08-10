using Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ư�� ������ ������ �Ѱ��ִ� ������ ���
/// </summary>
public class TitlePuzzleContainer : MonoBehaviour, IOnOff
{
    public string resourcePath;

    public TitlePuzzleStart puzzleStartButton;
    public TitlePuzzleButton puzzleButton;
    public Transform parent;

    //�ҷ��� ������� �ҷ��� ����
    public List<TitlePuzzleButton> puzzles = new List<TitlePuzzleButton>();

    #region ----------OnOff ��ư ���-------------
    public bool Active { get; set; }

    public bool Off()
    {
        Active = false;
        return Active;
    }

    public bool On()
    {
        Active = true;  
        LoadPuzzle();
        puzzleStartButton.Close();

        return Active;
    }
    #endregion

    //���� �÷��̸� ���� ��ư�� �����մϴ�.
    void LoadPuzzle()
    {
        if(puzzles.Count > 0) { return; }

        CustomDebug.PrintW("�̱� �÷��̸� ���� ������ �ҷ��ɴϴ�");

        var so = Resources.LoadAll<PuzzleSO>(resourcePath);

        foreach(var s in so) 
        {
            TitlePuzzleButton b = Instantiate(puzzleButton, parent);
            b.name = s.name;

            b.Init(s, puzzleStartButton);

            puzzles.Add(b);
        }
    }
}
