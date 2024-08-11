using Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퍼즐 버튼의 생성과 정보를 넘겨주는 역할을 담당
/// </summary>
public class TitlePuzzleContainer : MonoBehaviour, IOnOff
{
    public string resourcePath;

    public TitlePuzzleStart puzzleStartButton;
    public TitlePuzzleButton puzzleButton;
    public Transform parent;
    #region ----------OnOff 버튼 명령-------------
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

    //퍼즐 플레이를 위한 버튼을 생성합니다.
    void LoadPuzzle()
    {
        CustomDebug.PrintW("싱글 플레이를 위한 퍼즐을 불러옵니다");

        var so = SoManager.Instance.GetSoPuzzles();

        foreach(var s in so) 
        {
            TitlePuzzleButton b = Instantiate(puzzleButton, parent);
            b.name = s.name;

            b.Init(s, puzzleStartButton);
        }
    }
}
