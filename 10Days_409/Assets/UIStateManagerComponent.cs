using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManagerComponent : MonoBehaviour
{
    [SerializeField]  GameObject _mPlayer;
    [SerializeField]  GameObject _mGamePlayUIs;
    [SerializeField]  GameObject _mResultUIs;
    [SerializeField] private WaveSystem _waveSystem;

    private Judgement _mJudgement;

    private bool _mIsShowResult;
    private bool _mIsEndGame;


    // Start is called before the first frame update
    void Start()
    {
        _mIsShowResult = false;
        _mIsEndGame = false;

        _mJudgement = GetComponent<Judgement>();

        _waveSystem.OnAllEndWave(OnEndGame);
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
            ShowResult();
            return;
        }

        _mGamePlayUIs.SetActive(true);
        _mResultUIs.SetActive(false);
    }

    void ShowResult()
    {
        _mGamePlayUIs.SetActive(false);
        _mResultUIs.SetActive(true);
        _mIsShowResult = true;
    }

    bool IsDestroyPlayer()
    {
        return _mPlayer == null;
    }

    void OnEndGame()
    {
        _mIsEndGame = true;
        ShowResult();
    }
}
