using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RushEnemy_Controller_Component : MonoBehaviour
{
    private Level _mLevel;
    private RushEnemy_Level3_Move_Component _mLevel3MoveComponent = null;
    private RushEnemy_Level2_Move_Component _mLevel2MoveComponent = null;
    private RushEnemy_Level1_Move_Component _mLevel1MoveComponent = null;

    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private GameObject clockLevel3;
    [SerializeField] private GameObject clockLevel2;
    [SerializeField] private GameObject clockLevel1;

    [SerializeField] private GameObject _explosionSoundGameObject;

    private Image currentClockImage;

    [SerializeField] private Animator clockUiAnimator;

    [SerializeField] private GameObject _mLevel2Body;
    [SerializeField] private GameObject _mLevel3Body;
    [SerializeField] private Animator _mFaceAnimator;
    [SerializeField] private GameObject _mDeathParticle;

    private Enemy_Damage _mEnemyDamage;


    // Start is called before the first frame update
    void Start()
    {
        _mLevel = GetComponent<Level>();
        _mLevel.OnUpdateLevel(LevelChange);

        GameObject face = transform.Find("Face").gameObject;
        _mFaceAnimator = face.GetComponent<Animator>();

        _mEnemyDamage = GetComponent<Enemy_Damage>();

        _mLevel.OnDead(Death);

        int isVariation = Random.Range(0, 30);

        // 変異型
        if (isVariation == 0)
        {
            int downLevel = Random.Range(1, 2);

            for (int i = 0; i < downLevel; ++i)
            {
                _mLevel.LevelDown();
            }
        }


        LevelChange();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    _mLevel.LevelDown();
        //}

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    _mLevel.LevelUp();
        //}

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

        // 効率悪いやり方かも
        if (_mLevel3MoveComponent != null)
        {
            Destroy(this.GetComponent<RushEnemy_Level3_Move_Component>());
            _mLevel3MoveComponent = null;
        }

        if (_mLevel2MoveComponent != null)
        {
            Destroy(this.GetComponent<RushEnemy_Level2_Move_Component>());
            _mLevel2MoveComponent = null;
        }


        if (_mLevel1MoveComponent != null)
        {
            Destroy(this.GetComponent<RushEnemy_Level1_Move_Component>());
            _mLevel1MoveComponent = null;
        }

        if (currentLevel == 3)
        {
            _mLevel3MoveComponent = gameObject.AddComponent<RushEnemy_Level3_Move_Component>();
            ChangeLevel3();

            currentClockImage = clockLevel3.GetComponent<Image>();

            clockLevel3.SetActive(true);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else if (currentLevel == 2)
        {
            _mLevel2MoveComponent = gameObject.AddComponent<RushEnemy_Level2_Move_Component>();
            ChangeLevel2();

            currentClockImage = clockLevel2.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else
        {
            _mLevel1MoveComponent = gameObject.AddComponent<RushEnemy_Level1_Move_Component>();
            ChangeLevel1();

            currentClockImage = clockLevel1.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(false);
            clockLevel1.SetActive(true);
        }


        clockUiAnimator.Play("ClockUI_LevelChange_Animation");
    }

    void Death()
    {
        EnemyDestroyer.DestroyEnemy(gameObject);
        CreateParticle();
    }

    void CreateParticle() {
        var particle = GameObject.Instantiate(_mDeathParticle);
        particle.transform.position = transform.position;
        Destroy(particle, 3.0f);

        var expSound = Instantiate(_explosionSoundGameObject);
        Destroy(expSound, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemyDestroyer.DestroyEnemy(gameObject, true);
            CreateParticle();
        }

        if (other.gameObject.tag == "LevelUpItem")
        {
            _mLevel.LevelUp();
            LevelChange();
        }
    }

    void ChangeLevel2()
    {
        _mLevel3Body.SetActive(false);
        _mLevel2Body.SetActive(true);

        _mFaceAnimator.SetLayerWeight(1, 0.0f);
        _mFaceAnimator.SetLayerWeight(2, 1.0f);
        _mFaceAnimator.SetLayerWeight(3, 0.0f);
    }

    void ChangeLevel3()
    {
        _mLevel3Body.SetActive(true);
        _mLevel2Body.SetActive(true);

        _mFaceAnimator.SetLayerWeight(1, 1.0f);
        _mFaceAnimator.SetLayerWeight(2, 0.0f);
        _mFaceAnimator.SetLayerWeight(3, 0.0f);

    }

    void ChangeLevel1()
    {
        _mLevel3Body.SetActive(false);
        _mLevel2Body.SetActive(false);

        _mFaceAnimator.SetLayerWeight(1, 0.0f);
        _mFaceAnimator.SetLayerWeight(2, 0.0f);
        _mFaceAnimator.SetLayerWeight(3, 1.0f);
    }

}
