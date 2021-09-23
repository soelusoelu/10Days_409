using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10.0f)] private float _FadeTime = 3.0f;

    [SerializeField] private Image _FadeImage;

    private float _FadeSpeed;
    private bool _FadeFlag;
    private bool _FadeIn, _FadeOut;

    //private Image _FadeImage;
    private float _Red, _Green, _Blue, _Alpha;

    // Start is called before the first frame update
    void Start()
    {
        _Red = _FadeImage.color.r;
        _Green = _FadeImage.color.g;
        _Blue= _FadeImage.color.b;
        _Alpha = _FadeImage.color.a;

        _FadeImage.enabled = true;
        _FadeFlag = true;//最初は暗転からスタートのため
        _FadeIn = true;//
        _FadeOut = false;//
        _FadeSpeed = _FadeTime / 255.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_FadeFlag) return;

        if(_FadeIn)
        {
            FadeIn();
        }
        else if(_FadeOut)
        {
            FadeOut();
        }
    }

    public bool GetFadeFlag()
    {
        return _FadeFlag;
    }

    public bool GetFadeIn()
    {
        return _FadeIn;
    }

    public bool GetFadeOut()
    {
        return _FadeOut;
    }

    public void SetFadeFlag(bool flag)
    {
        _FadeFlag = flag;
    }

    //明るくなる
    private void FadeIn()
    {
        _Alpha -= _FadeSpeed;
        setAlpha();
        if(_Alpha <= 0.0f)
        {
            BoolChange();

            _FadeImage.enabled = false;
        }
    }

    //暗くなる
    private void FadeOut()
    {
        _FadeImage.enabled = true;
        _Alpha += _FadeSpeed;
        setAlpha();
        if(_Alpha >= 1.0f)
        {
            BoolChange();
        }
    }

    private void setAlpha()
    {
        _FadeImage.color = new Color(_Red, _Green, _Blue, _Alpha);
    }

    private void BoolChange()
    {
        _FadeIn = !_FadeIn;
        _FadeOut = !_FadeOut;
        _FadeFlag = !_FadeFlag;
    }

}
