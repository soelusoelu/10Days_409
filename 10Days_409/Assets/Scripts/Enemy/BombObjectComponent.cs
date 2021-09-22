using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombObjectComponent : MonoBehaviour
{
    [SerializeField] private float _mMoveSpeed = 0.003f;


    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private GameObject clockLevel3;
    [SerializeField] private GameObject clockLevel2;
    [SerializeField] private GameObject clockLevel1;


    [SerializeField] private Animator clockUiAnimator;
    [SerializeField] private GameObject _mExplosion;



    private Image currentClockImage;
    private Level _mLevel;
    private Enemy_Damage _mEnemyDamage;





    // Start is called before the first frame update
    void Start()
    {
        _mLevel = GetComponent<Level>();
        _mLevel.OnUpdateLevel(LevelChange);



        _mEnemyDamage = GetComponent<Enemy_Damage>();

        _mLevel.OnDead(Death);

        LevelChange();


    }


    void UpdateClockUI()
    {
        float value = _mEnemyDamage.GetHP() / _mEnemyDamage.GetMaxHP();
        clockHandTransform.eulerAngles = new Vector3(0, 0, 360.0f - (360 * value));

        currentClockImage.fillAmount = value;
    }


    void LevelChange()
    {
        Debug.Log("LevelChange!");

        int currentLevel = _mLevel.GetLevel();

        if (currentLevel == 3)
        {


            currentClockImage = clockLevel3.GetComponent<Image>();

            clockLevel3.SetActive(true);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else if (currentLevel == 2)
        {


            currentClockImage = clockLevel2.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else
        {


            currentClockImage = clockLevel1.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(false);
            clockLevel1.SetActive(true);
        }


        clockUiAnimator.Play("ClockUI_LevelChange_Animation");
    }

    void Death()
    {
        Destroy(gameObject);

        var c = Instantiate(_mExplosion);
        c.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelUpItem")
        {
            _mLevel.LevelUp();
            LevelChange();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_mEnemyDamage.IsEmpty())
        {
            _mEnemyDamage.ResetHP();
            _mLevel.LevelDown();

            if (_mLevel.GetLevel() <= 0)
            {
                return;
            }

            // TODO:後でLV変わったアニメーション
            LevelChange();

        }


        UpdateClockUI();

        Move();
    }



    private void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
    }
}
