using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//���� ���� ������ ������ �ִ´�.
public class SceneInformation
{
    public string SceneName = null;          //�ε��� ���� �̸�
    public Action SceneEndAction = null;     //�� �ε� �� ���� �� ��
    public bool SceneLoading = false;         //�� �ε� �Ϸ� ����
}

public class SceneLoader : MonoBehaviour
{
    public static SceneInformation sceneInformation = new SceneInformation();

    public Image image_Progress1;
    public Image image_Progress2;

    public Animation anim_Loading;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(SceneLoad());

        Debug.LogWarning("���� ��ȯ�Ǿ� ���� ü������ �������");
    }

    IEnumerator SceneLoad()
    {
        Debug.LogWarning("���� �ҷ����� ��.....");

        ////ȭ�� ���̱�
        //Fade.FadeSetting(false,1.0f,Color.white);
        ////ȭ�� ���̴� ���� ���
        //yield return new WaitUntil(() => Fade.b_faded);

        anim_Loading.Play();

        //�� �ε��� ���� �Լ�
        AsyncOperation _operation = SceneManager.LoadSceneAsync(sceneInformation.SceneName);
        _operation.allowSceneActivation = false;

        //�ε� ���൵ ǥ��
        float _progress = 0.0f;
        while (_progress < 0.89f)
        {
            if (_progress > _operation.progress) { continue; }

            _progress += Time.unscaledDeltaTime * 0.333f;

            image_Progress1.fillAmount = _progress;
            image_Progress2.fillAmount = _progress;

            yield return null;
        }
        
        ////1�� �����Ѵ�.
        //float time = 0;
        //while (time < 1.0f)
        //{
        //    time += Time.unscaledDeltaTime;
        //    img_progress.fillAmount = Mathf.Lerp(0.9f, 1.0f, time);
        //}
        //���� �ε��Ų��.

        _operation.allowSceneActivation = true;
        //�۾��� ���������� ��� ��Ų��.
        while (!_operation.isDone)
        {
            Debug.Log("���� �ҷ����� �ֽ��ϴ�.");
            yield return null;
        }

        //�� �ϸ鼭 ������ ������ �����մϴ�.
        if (sceneInformation.SceneEndAction != null)
        {
            sceneInformation.SceneEndAction();
            sceneInformation.SceneEndAction = null;
        }

        Debug.Log("���� �ε忡 �����߽��ϴ�. : " + sceneInformation.SceneName);

        //���Ḧ �˸�
        sceneInformation.SceneLoading = false;
        Time.timeScale = 1.0f;

        Fade.FadeSetting(true,1.0f,Color.white);

        //���� �ε� ������Ʈ ����
        Destroy(gameObject);
    }


    //�� �ε��� ���� �����Լ�
    public static void LoadLoaderScene(string _name, Action _action=null)
    {
        SceneManager.LoadScene("LoadScene");
        SceneLoader.sceneInformation.SceneName = _name;
        SceneLoader.sceneInformation.SceneEndAction = _action;
        SceneLoader.sceneInformation.SceneLoading = false;
    }
}