using Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  SO ���� ������ ��� ������ �ֽ��ϴ�.
///  ������ ���������� �ҷ����� �ʱ� ���� ����մϴ�.
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

    //���� �̱۸�� ����
    PuzzleSO SingleSo;

    SoManager()
    {
        puzzleSo = Resources.LoadAll<PuzzleSO>(PuzzleConfig.SoResourcePath);
    }

    public PuzzleSO[] GetSoPuzzles()
    {
        if(puzzleSo == null)
        {
            CustomDebug.Exeption($"SoManager ������ �������� �ʽ��ϴ�.");
        }
        return puzzleSo;
    }
}
