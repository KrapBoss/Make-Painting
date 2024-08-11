using Custom;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��� ������ Ʋ
/// ������ �ϼ�
/// </summary>
public class PuzzleContainer : MonoBehaviour
{
    //[SerializeField] Piece[] datas;
    [SerializeField] Piece BackGround; // ��׶���� ���� ���� ������� ������� �ʽ��ϴ�.
    Capture2D capture;

    //���� ���� ���� ��
    public float Perfection = 0;
    //�� ���� ���� ��
    public int CountPiece;

    //������ �¾��� ��� ������ ���� ����
    public Action<float> Action_Fit;

    public Sprite BackGroundSprite
    {
        get
        {
            if (BackGround.data.Sprite == null)
            {
                BackGround.Initialize(this);
            }
            return BackGround.data.Sprite;
        }
    }//��׶��� ��������Ʈ�� ��ȯ

    //���� ������ ������ ����԰� ���ÿ� �ʱ�ȭ�� �����Ѵ�.
    public void Initialize()
    {
        CustomDebug.PrintW($"PuzzleContainer {transform.name} Awake");

        PuzzleDictionary.Instance.Clear();

        transform.position = Vector3.zero;

        //���� ���
        RegisterPiece();
        
        BackGround.ActivePiece();

        Perfection = 0;
        //������ ��� �Ͽ� ����ڿ��� �����ش�.
        FindObjectOfType<PuzzlePieceScrollView>()?.RegistPiece();
    }

    void RegisterPiece()
    {
        //����
        Piece[] datas = GetComponentsInChildren<Piece>();

        if (datas.Length < 1) { Debug.LogWarning($"{name} �� ���� �������� �����ϴ�."); return; }

        //�̸����⸦ ����
        capture = FindObjectOfType<Capture2D>();
        capture.Capture(BackGround.transform, true);

        //���� ���� �� �ʱ�ȭ
        CountPiece = 0;

        //���� ������ �ʱ� ����
        foreach (Piece data in datas)
        {
            //��Ȱ��ȭ�˴ϴ�.
            data.Initialize(this);

            //��׶���� �������� �ʽ��ϴ�
            if (data == BackGround) continue;

            CountPiece++;
            PuzzleDictionary.Instance.AddPiece(data.data.PieceName, data);
        }
    }

    //���� ������ ������ ���޹޾� ������ ��� �����Ͽ� �ݴϴ�.
    public void FitThePiece()
    {
        //���� ������ ���� �������ݴϴ�.-------------------------------
        if (Action_Fit != null) Action_Fit(Perfection/CountPiece);

        if (Perfection > CountPiece)
        {
            Debug.Log($"���� ���� {Perfection}/{CountPiece}");
        }
        else
        {
            Debug.Log($"���� ���� {Perfection}/{CountPiece}");
        }
    }

    private void OnDestroy()
    {
        //������ �����̳� ������ �����մϴ�.
        PuzzleDictionary.Instance.Clear();
    }
}
