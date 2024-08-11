using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//씬에 대한 정보를 가지고 있는다.
public class SceneInformation
{
    public string SceneName = null;          //로딩할 씬의 이름
    public Action SceneEndAction = null;     //씬 로딩 후 실행 할 것
    public bool SceneLoading = false;         //씬 로딩 완료 여부
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

        Debug.LogWarning("씬이 전환되어 사운드 체인저를 비워줬음");
    }

    IEnumerator SceneLoad()
    {
        Debug.LogWarning("씬을 불러오는 중.....");

        ////화면 보이기
        //Fade.FadeSetting(false,1.0f,Color.white);
        ////화면 보이는 동안 대기
        //yield return new WaitUntil(() => Fade.b_faded);

        anim_Loading.Play();

        //씬 로딩을 위한 함수
        AsyncOperation _operation = SceneManager.LoadSceneAsync(sceneInformation.SceneName);
        _operation.allowSceneActivation = false;

        //로딩 진행도 표시
        float _progress = 0.0f;
        while (_progress < 0.89f)
        {
            if (_progress > _operation.progress) { continue; }

            _progress += Time.unscaledDeltaTime * 0.333f;

            image_Progress1.fillAmount = _progress;
            image_Progress2.fillAmount = _progress;

            yield return null;
        }
        
        ////1초 지연한다.
        //float time = 0;
        //while (time < 1.0f)
        //{
        //    time += Time.unscaledDeltaTime;
        //    img_progress.fillAmount = Mathf.Lerp(0.9f, 1.0f, time);
        //}
        //씬을 로드시킨다.

        _operation.allowSceneActivation = true;
        //작업이 끝날때까지 대기 시킨다.
        while (!_operation.isDone)
        {
            Debug.Log("맵을 불러오고 있습니다.");
            yield return null;
        }

        //씬 하면서 지정된 동작을 수행합니다.
        if (sceneInformation.SceneEndAction != null)
        {
            sceneInformation.SceneEndAction();
            sceneInformation.SceneEndAction = null;
        }

        Debug.Log("씬을 로드에 성공했습니다. : " + sceneInformation.SceneName);

        //종료를 알림
        sceneInformation.SceneLoading = false;
        Time.timeScale = 1.0f;

        Fade.FadeSetting(true,1.0f,Color.white);

        //현재 로딩 오브젝트 제거
        Destroy(gameObject);
    }


    //씬 로딩을 위한 전역함수
    public static void LoadLoaderScene(string _name, Action _action=null)
    {
        SceneManager.LoadScene("LoadScene");
        SceneLoader.sceneInformation.SceneName = _name;
        SceneLoader.sceneInformation.SceneEndAction = _action;
        SceneLoader.sceneInformation.SceneLoading = false;
    }
}