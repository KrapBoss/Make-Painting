using Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  SO 퍼즐 정보를 모두 가지고 있습니다.
///  퍼즐을 지속적으로 불러오지 않기 위해 사용합니다.
/// </summary>
public class SoManager
{
    static SoManager instance;
    public static SoManager Instance
    {
        get
        {
            if(instance == null) instance = new SoManager();
            return instance;
        }
    }

    PuzzleSO[] puzzleSo;

    //현재 싱글모드 퍼즐
    PuzzleSO SingleSo;

    SoManager()
    {
        puzzleSo = Resources.LoadAll<PuzzleSO>(PuzzleConfig.SoResourcePath);
    }

    public PuzzleSO[] GetSoPuzzles()
    {
        if(puzzleSo == null)
        {
            CustomDebug.Exeption($"SoManager 퍼즐이 존재하지 않습니다.");
        }
        return puzzleSo;
    }
}
