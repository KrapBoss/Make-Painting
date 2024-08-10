using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePuzzleButton : MonoBehaviour
{
    public Image imageSample;

    [Header("Ȯ�ο�")]
    [SerializeField] PuzzleSO mySo;
    [SerializeField] TitlePuzzleStart myStarter;

    public void Init(PuzzleSO so, TitlePuzzleStart t)
    {
        if(so == null)throw new System.NullReferenceException();
        mySo = so;
        myStarter = t;

        imageSample.sprite = mySo.SampleImage;

        GetComponent<Button>().onClick.AddListener(ShowPuzzleMenu);
    }

    //������ ������ �� �ִ� �г��� �����ݴϴ�.
    public void ShowPuzzleMenu()
    {
        myStarter.Show(mySo);
    }
}
