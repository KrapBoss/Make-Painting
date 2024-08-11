using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransferMode
{
    Move, Stop
}

/// <summary>
/// 퍼즐조각의 움직임을 담당합니다.
/// </summary>
public class PieceTransfer : MonoBehaviour
{
    Vector2 _offset;

    //이동 중인 상태를 나타냅니다.
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
        //터치입력이 종료됨
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
