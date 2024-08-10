using UnityEngine;

/// <summary>
/// 각 퍼즐에 장르와 어떤 퍼즐이 들어있는지 확인합니다.
/// </summary>

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "Puzzle/PuzzleData")]
public class PuzzleSO : ScriptableObject
{
    public AudioClip BGM;         //해당 퍼즐의 배경음
    public Sprite SampleImage; //배경화면
    public GameObject Puzzle;     //완성된 퍼즐
}
