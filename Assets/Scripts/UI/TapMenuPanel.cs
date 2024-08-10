using Custom;
using UnityEngine;

public class TapMenuPanel : MonoBehaviour
{
    IOnOff[] iOnOff;

    public void Active()
    {
        if (gameObject.activeSelf) return;

        //������ �����ϴ� ��ư ������ ���ٸ�?
        if(iOnOff == null) iOnOff = GetComponentsInChildren<IOnOff>();

        gameObject.SetActive(true);

        //��� ������Ʈ ��Ȱ��ȭ
        if (iOnOff != null)
        {
            foreach (var item in iOnOff)
            {
                if (!item.Active) item.On();
            }
        }

        CustomDebug.Print($"{transform.name} Ȱ��ȭ");
    }

    public void DeActive()
    {
        if (!gameObject.activeSelf) return;

        CustomDebug.Print($"{transform.name} ��Ȱ��ȭ");

        //��� ������Ʈ ��Ȱ��ȭ
        if (iOnOff != null)
        {
            foreach (var item in iOnOff)
            {
                if (item.Active) item.Off();
            }
        }

        gameObject.SetActive(false);
    }
}
