using Custom;
using System;
using UnityEngine;
using static Fusion.Sockets.NetBitBuffer;

/// <summary>
/// �⺻���� ������ ������ ����մϴ�.
/// </summary>
public class DefaultPuzzle : Piece
{
    private void Update()
    {
        if (data.Activation) return;

        if (_pieceTransfer.IsTranfer)
        {
            if(_pieceTransfer.TransferMode == TransferMode.Stop)
            {
                IsFit(transform.localPosition);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(PuzzleConfig.PickUpOffest.x, PuzzleConfig.PickUpOffest.y, 0) + transform.position, PuzzleConfig.FitRange);
    }
}
