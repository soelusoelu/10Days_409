using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockEnemy_Controller_Component : MonoBehaviour
{
    private Level _mLevel;
    private BlockEnemy_Level3_Move_Component _mLevel3MoveComponent = null;
    private BlockEnemy_Level2_Move_Component _mLevel2MoveComponent = null;
    private BlockEnemy_Level1_Move_Component _mLevel1MoveComponent = null;

    [SerializeField] private Animator _mFaceAnimator;
    [SerializeField] private GameObject defenceBlockPrefab;
    [SerializeField] private GameObject _mlevel2_Body;
    [SerializeField] private GameObject _mlevel1_Body;
    [SerializeField] private GameObject _mDeathParticle;

    private Image currentClockImage;

    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private Animator clockUiAnimator;
    [SerializeField] private GameObject clockLevel3;
    [SerializeField] private GameObject clockLevel2;
    [SerializeField] private GameObject clockLevel1;

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

        if(_mEnemyDamage.IsEmpty())
        {
            _mEnemyDamage.ResetHP();
            _mLevel.LevelDown();

            if(_mLevel.GetLevel() <= 0)
            {
                return;
            }

            LevelChange();
        }

        UpdateClockUI();

    }

    private void UpdateClockUI()
    {
        float value = _mEnemyDamage.GetHP() / _mEnemyDamage.GetMaxHP();
        clockHandTransform.eulerAngles = new Vector3(0, 0, 360.0f - (360 * value));

        currentClockImage.fillAmount = value;
    }


    void LevelChange()
    {

        int currentLevel = _mLevel.GetLevel();

        // å¯ó¶à´Ç¢Ç‚ÇËï˚Ç©Ç‡
        if (_mLevel3MoveComponent != null)
        {
            Destroy(this.GetComponent<BlockEnemy_Level3_Move_Component>());
            _mLevel3MoveComponent = null;
        }

        if (_mLevel2MoveComponent != null)
        {
            Destroy(this.GetComponent<BlockEnemy_Level2_Move_Component>());
            _mLevel2MoveComponent = null;
        }


        if (_mLevel1MoveComponent != null)
        {
            Destroy(this.GetComponent<BlockEnemy_Level1_Move_Component>());
            _mLevel1MoveComponent = null;
        }

        if (currentLevel == 3)
        {
            _mLevel3MoveComponent = gameObject.AddComponent<BlockEnemy_Level3_Move_Component>();
            _mLevel3MoveComponent.SetDefenceBlockPrefab(defenceBlockPrefab);
            ChangeLevel3();

            // Ç–Ç«Ç¢Ç±Å[Ç«
            currentClockImage = clockLevel3.GetComponent<Image>();

            clockLevel3.SetActive(true);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else if (currentLevel == 2)
        {
            _mLevel2MoveComponent = gameObject.AddComponent<BlockEnemy_Level2_Move_Component>();
            ChangeLevel2();

            currentClockImage = clockLevel2.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else
        {
            _mLevel1MoveComponent = gameObject.AddComponent<BlockEnemy_Level1_Move_Component>();
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
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
        _mlevel2_Body.SetActive(true);
        _mlevel1_Body.SetActive(true);

        _mFaceAnimator.SetLayerWeight(1, 0.0f);
        _mFaceAnimator.SetLayerWeight(2, 1.0f);
        _mFaceAnimator.SetLayerWeight(3, 0.0f);
    }

    void ChangeLevel3()
    {
        _mlevel2_Body.SetActive(true);
        _mlevel1_Body.SetActive(true);

        _mFaceAnimator.SetLayerWeight(1, 1.0f);
        _mFaceAnimator.SetLayerWeight(2, 0.0f);
        _mFaceAnimator.SetLayerWeight(3, 0.0f);

    }

    void ChangeLevel1()
    {
        _mlevel2_Body.SetActive(false);
        _mlevel1_Body.SetActive(true);

        _mFaceAnimator.SetLayerWeight(1, 0.0f);
        _mFaceAnimator.SetLayerWeight(2, 0.0f);
        _mFaceAnimator.SetLayerWeight(3, 1.0f);
    }

}
