using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퍼즐을 맞출 때 사용하는 기본 정보를 기록합니다.
/// </summary>
 public static class PuzzleConfig
{
    public static string SoResourcePath = "SinglePuzzles";
    public static readonly float FitRange = 0.3f;
    public static Vector2 PickUpOffest = new Vector2(0, 0);
}
