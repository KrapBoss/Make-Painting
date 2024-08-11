using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �� ���� ������ ���� ������
/// </summary>
[System.Serializable]
public class PieceData
{
    public string PieceName;
    public Sprite Sprite;
    public Vector2 FitPosition;
    public int Sorting;
    public bool Activation;
    public float ScaleX;// �� �̹��� ���� ũ�⸦ �����ϰ� �ϱ� ����
}

/// <summary>
/// ���� ������ �ݵ�� ����� �޾Ƽ� ���
/// </summary>
public class Piece : MonoBehaviour
{
    public PieceData data;                      //���� ���� ������
    public SpriteRenderer[] spriteRenderer;     //���� ������ �׸��� ��� ������.
    protected PuzzleContainer _container;       //������ �����̳ʸ� ���� �Ǵ��� ����
    protected IPieceAction _pieceAction;        //�� ������� ���� ������ ����
    protected PieceTransfer _pieceTransfer;     //������ �������� ����մϴ�.

    Action _fit, _notFit;

    //�����͸� �ʱ�ȭ�޽��ϴ�.
    public virtual void Initialize(PuzzleContainer container)
    {
        if (container == null) throw new Exception($"���� ������ �����̳ʰ� �������� �ʽ��ϴ� => {transform.name}");

        _container = container;

        //������ ������ ���Ǹ� ã���ϴ�.
        _pieceAction = GetComponent<IPieceAction>();

        //���� ������ ������ �����մϴ�.
        SetData();


        //���� ������ ������ ����ڸ� ã���ϴ�.
        _pieceTransfer = GetComponent<PieceTransfer>();
        if (_pieceTransfer == null)
        {
            _pieceTransfer = new PieceTransfer();
        }
        _pieceTransfer.Initialize(transform, new Vector2(0, spriteRenderer[0].bounds.extents.y / 2));

        //���� ������ �ֻ�ܿ� ��ġ�ϵ��� �մϴ�.
        foreach (var sprite in spriteRenderer) sprite.sortingOrder = 1;

        //������ ����ϴ�.
        HidePiece();
    }

    //���� ������ �����͸� ����ϴ�.
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

    //�������� ������ �˸��ϴ�.0...0
    public virtual void StartTransition(Action notFit, Action fit)
    {
        if (_pieceAction != null) _pieceAction.StartTransfer();

        gameObject.SetActive(true);

        _fit = fit;
        _notFit = notFit;
    }

    //���� ������ ���뺾�ϴ�.
    public virtual bool Fit(Vector2 fitPos)
    {
        //���� �̸��� �����ϴ� ��� �������� �����ɴϴ�.
        Piece[] piecesWithName = PuzzleDictionary.Instance.GetPieceWithName(data.PieceName);

        if(piecesWithName == null){ throw new Exception($"���� ���� �̸� \"{data.PieceName}\"�� �����ϴ� �������� �����ϴ�."); }

        foreach(Piece piece in piecesWithName)
        {
            //���� ������ ��ġ�� �´��� �Ǵ��մϴ�.
            if (piece.IsFit(fitPos))
            {
                _container.FitThePiece();
                piece.ActivePiece();
                if (_fit != null) _fit();

                return true;
            }
        }

        //���� ������ �ϳ��� ���� �ʴ� ���
        if (_notFit != null) _notFit();
        HidePiece();
        return false;
    }

    //���� �����Ϳ� ���� ������ ��ġ�� �´��� Ȯ�θ� �մϴ�.
    public bool IsFit(Vector2 fitPos)
    {
        if ((fitPos - data.FitPosition).magnitude <= PuzzleConfig.FitRange)
            return true;
        else
            return false;
    }

    //���� ������ ��ġ�� ���߰� Ȱ��ȭ�մϴ�.
    public void ActivePiece()
    {
        gameObject.SetActive(true);
        transform.localPosition = data.FitPosition;
        data.Activation = true;
        foreach (var sprite in spriteRenderer) sprite.sortingOrder = data.Sorting;

        if (_pieceAction != null) _pieceAction.Fit();
    }

    //���� ������ ����ϴ�.
    void HidePiece()
    {
        //���� ������ Ȱ��ȭ �� �����Ÿ��� ������ �ذ��մϴ�.
        transform.localPosition += new Vector3(100, 100, 0);
        _fit = null; _notFit = null;
        gameObject.SetActive(false);
    }
}
