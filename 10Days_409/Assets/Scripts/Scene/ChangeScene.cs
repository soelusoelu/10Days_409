using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private string SceneName = "";
    [SerializeField] private GameObject obj;
    private Judgement judge;

    // Start is called before the first frame update
    void Start()
    {
        judge = obj.GetComponent<Judgement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (judge.GetJudge())
        {
            SceneManager.LoadScene(SceneName);
        }
        
    }
}
