using UnityEngine;

/// <summary>
/// �� ���� �帣�� � ������ ����ִ��� Ȯ���մϴ�.
/// </summary>

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "Puzzle/PuzzleData")]
public class PuzzleSO : ScriptableObject
{
    public AudioClip BGM;         //�ش� ������ �����
    public Sprite SampleImage; //���ȭ��
    public GameObject Puzzle;     //�ϼ��� ����
}
