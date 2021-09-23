using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private string SceneName = "";
    [SerializeField] private GameObject obj;
    private Judgement judge;
    private FadeController _Fade;

    // Start is called before the first frame update
    void Start()
    {
        judge = obj.GetComponent<Judgement>();
        _Fade = GetComponent<FadeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Fade.GetFadeFlag()) return;

        if (judge.GetJudge())
        {
            if (_Fade.GetFadeIn())
            {
                SceneManager.LoadScene(SceneName);
            }
            else _Fade.SetFadeFlag(true);
        }
        
    }
}
