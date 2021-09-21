using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionComponent : MonoBehaviour
{
    private Animator _mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadSceneTransition());
        }
    }

    IEnumerator LoadSceneTransition()
    {
        _mAnimator.SetBool("TransitionOut",true);
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GamePlayScene");
    }
}
