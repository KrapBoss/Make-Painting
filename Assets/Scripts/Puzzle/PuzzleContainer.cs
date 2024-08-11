using Custom;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// 모든 퍼즐의 틀
/// 퍼즐의 완성
/// </summary>
public class PuzzleContainer : MonoBehaviour
{
    //[SerializeField] Piece[] datas;
    [SerializeField] Piece BackGround; // 백그라운드는 퍼즐 조각 목록으로 사용하지 않습니다.
    Capture2D capture;

    //맞춘 퍼즐 조각 수
    public float Perfection = 0;
    //총 퍼즐 조각 수
    public int CountPiece;

    //퍼즐이 맞았을 경우 실행할 동작 저장
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
    }//백그라운드 스프라이트를 반환

    //퍼즐 조각을 사전에 등록함과 동시에 초기화를 진행한다.
    public void Initialize()
    {
        CustomDebug.PrintW($"PuzzleContainer {transform.name} Awake");

        PuzzleDictionary.Instance.Clear();

        transform.position = Vector3.zero;

        //퍼즐 등록
        RegisterPiece();
        
        BackGround.ActivePiece();

        Perfection = 0;
        //퍼즐을 등록 하여 사용자에게 보여준다.
        FindObjectOfType<PuzzlePieceScrollView>()?.RegistPiece();
    }

    void RegisterPiece()
    {
        //참조
        Piece[] datas = GetComponentsInChildren<Piece>();

        if (datas.Length < 1) { Debug.LogWarning($"{name} 에 퍼즐 조각들이 없습니다."); return; }

        //미리보기를 저장
        capture = FindObjectOfType<Capture2D>();
        capture.Capture(BackGround.transform, true);

        //퍼즐 조각 수 초기화
        CountPiece = 0;

        //퍼즐 조각을 초기 세팅
        foreach (Piece data in datas)
        {
            //비활성화됩니다.
            data.Initialize(this);

            //백그라운드는 저장하지 않습니다
            if (data == BackGround) continue;

            CountPiece++;
            PuzzleDictionary.Instance.AddPiece(data.data.PieceName, data);
        }
    }

    //퍼즐 조각의 맞음을 전달받아 정보를 대신 갱신하여 줍니다.
    public void FitThePiece()
    {
        //퍼즐 조각의 수를 전달해줍니다.-------------------------------
        if (Action_Fit != null) Action_Fit(Perfection/CountPiece);

        if (Perfection > CountPiece)
        {
            Debug.Log($"게임 진행 {Perfection}/{CountPiece}");
        }
        else
        {
            Debug.Log($"게임 종료 {Perfection}/{CountPiece}");
        }
    }

    private void OnDestroy()
    {
        //저장한 컨테이너 조각을 제거합니다.
        PuzzleDictionary.Instance.Clear();
    }
}
