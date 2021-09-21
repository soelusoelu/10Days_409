using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManagerComponent : MonoBehaviour
{
    [SerializeField]  GameObject _mPlayer;
    [SerializeField]  GameObject _mGamePlayUIs;
    [SerializeField]  GameObject _mResultUIs;

    private Judgement _mJudgement;

    private bool _mIsShowResult;


    // Start is called before the first frame update
    void Start()
    {
        _mIsShowResult = false;

        _mJudgement = GetComponent<Judgement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_mIsShowResult)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mJudgement.setJudge(true);
            }

            return;
        }


        if (IsDestroyPlayer())
        {
            _mGamePlayUIs.SetActive(false);
            _mResultUIs.SetActive(true);
            _mIsShowResult = true;
            return;
        }

        _mGamePlayUIs.SetActive(true);
        _mResultUIs.SetActive(false);
    }

    bool IsDestroyPlayer()
    {
        return _mPlayer == null;
    }
}
