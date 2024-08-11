using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransferMode
{
    Move, Stop
}

/// <summary>
/// ���������� �������� ����մϴ�.
/// </summary>
public class PieceTransfer : MonoBehaviour
{
    Vector2 _offset;

    //�̵� ���� ���¸� ��Ÿ���ϴ�.
    public bool IsTranfer;
    public TransferMode TransferMode;

    Transform myTransform;

    private void Update()
    {
        if (IsTranfer)
        {
            ProgressTransfer();
        }
    }

    public void Initialize(Transform tf, Vector2 offset)
    {
        myTransform = tf;
        IsTranfer = false;

        _offset = offset;
    }

    public void StartTransfer()
    {
        IsTranfer = true;
        TransferMode = TransferMode.Move;
    }

    public void ProgressTransfer()
    {
        //��ġ�Է��� �����
        if (InputManager.Instance.input.touchState == InputData.TouchState.Up)
        {
            StopTransfer();
            return;
        }

        Vector3 pos = InputManager.Instance.input.S2WTouchPosition + -_offset - PuzzleConfig.PickUpOffest;
        myTransform.position = pos;
    }

    public void StopTransfer()
    {
        TransferMode = TransferMode.Stop;
    }
}
