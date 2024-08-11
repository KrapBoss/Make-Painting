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
        CustomDebug.PrintW("�̱� �÷��̸� ���� ������ �ҷ��ɴϴ�");

        var so = SoManager.Instance.GetSoPuzzles();

        foreach(var s in so) 
        {
            TitlePuzzleButton b = Instantiate(puzzleButton, parent);
            b.name = s.name;

            b.Init(s, puzzleStartButton);
        }
    }
}
