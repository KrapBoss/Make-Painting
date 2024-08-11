using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 각 퍼즐 조각을 위한 데이터
/// </summary>
[System.Serializable]
public class PieceData
{
    public string PieceName;
    public Sprite Sprite;
    public Vector2 FitPosition;
    public int Sorting;
    public bool Activation;
    public float ScaleX;// 각 이미지 별로 크기를 상이하게 하기 위해
}

/// <summary>
/// 퍼즐 조각은 반드시 상속을 받아서 사용
/// </summary>
public class Piece : MonoBehaviour
{
    public PieceData data;                      //현재 퍼즐 데이터
    public SpriteRenderer[] spriteRenderer;     //퍼즐 조각의 그림을 모두 가진다.
    protected PuzzleContainer _container;       //퍼즐의 컨테이너를 통한 판단을 위함
    protected IPieceAction _pieceAction;        //각 퍼즐들의 고유 동작을 위함
    protected PieceTransfer _pieceTransfer;     //퍼즐의 움직임을 담당합니다.

    Action _fit, _notFit;

    //데이터를 초기화받습니다.
    public virtual void Initialize(PuzzleContainer container)
    {
        if (container == null) throw new Exception($"퍼즐 조각의 컨테이너가 존재하지 않습니다 => {transform.name}");

        _container = container;

        //고유의 동작의 정의를 찾습니다.
        _pieceAction = GetComponent<IPieceAction>();

        //현재 조각의 정보를 지정합니다.
        SetData();


        //퍼즐 조각의 움직임 담당자를 찾습니다.
        _pieceTransfer = GetComponent<PieceTransfer>();
        if (_pieceTransfer == null)
        {
            _pieceTransfer = new PieceTransfer();
        }
        _pieceTransfer.Initialize(transform, new Vector2(0, spriteRenderer[0].bounds.extents.y / 2));

        //퍼즐 조각이 최상단에 위치하도록 합니다.
        foreach (var sprite in spriteRenderer) sprite.sortingOrder = 1;

        //조각을 숨깁니다.
        HidePiece();
    }

    //퍼즐 조각의 데이터를 담습니다.
    void SetData()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        data.Sorting = spriteRenderer[0].sortingOrder;
        data.Sprite = spriteRenderer[0].sprite;
        data.PieceName = gameObject.name;
        data.FitPosition = transform.localPosition;
        data.Activation = false;
        data.ScaleX = transform.localScale.x;
    }

    //움직임의 시작을 알립니다.0...0
    public virtual void StartTransition(Action notFit, Action fit)
    {
        if (_pieceAction != null) _pieceAction.StartTransfer();

        gameObject.SetActive(true);

        _fit = fit;
        _notFit = notFit;
    }

    //퍼즐 조각을 맞취봅니다.
    public virtual bool Fit(Vector2 fitPos)
    {
        //퍼즐 이름에 대응하는 모든 조각들을 가져옵니다.
        Piece[] piecesWithName = PuzzleDictionary.Instance.GetPieceWithName(data.PieceName);

        if(piecesWithName == null){ throw new Exception($"현재 퍼즐 이름 \"{data.PieceName}\"에 대응하는 조각들이 없습니다."); }

        foreach(Piece piece in piecesWithName)
        {
            //퍼즐 조각에 위치가 맞는지 판단합니다.
            if (piece.IsFit(fitPos))
            {
                _container.FitThePiece();
                piece.ActivePiece();
                if (_fit != null) _fit();

                return true;
            }
        }

        //퍼즐 조각이 하나도 맞지 않는 경우
        if (_notFit != null) _notFit();
        HidePiece();
        return false;
    }

    //현재 데이터와 퍼즐 조각의 위치가 맞는지 확인만 합니다.
    public bool IsFit(Vector2 fitPos)
    {
        if ((fitPos - data.FitPosition).magnitude <= PuzzleConfig.FitRange)
            return true;
        else
            return false;
    }

    //퍼즐 조각의 위치를 맞추고 활성화합니다.
    public void ActivePiece()
    {
        gameObject.SetActive(true);
        transform.localPosition = data.FitPosition;
        data.Activation = true;
        foreach (var sprite in spriteRenderer) sprite.sortingOrder = data.Sorting;

        if (_pieceAction != null) _pieceAction.Fit();
    }

    //퍼즐 조각을 숨깁니다.
    void HidePiece()
    {
        //퍼즐 조각의 활성화 시 깜빡거리는 문제를 해결합니다.
        transform.localPosition += new Vector3(100, 100, 0);
        _fit = null; _notFit = null;
        gameObject.SetActive(false);
    }
}
