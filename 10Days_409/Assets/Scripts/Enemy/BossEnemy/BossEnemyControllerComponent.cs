using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyControllerComponent : MonoBehaviour
{
    private Level _mLevel;
    private BossEnemy_Level3_Move_Component _mLevel3MoveComponent = null;
    private BossEnemy_Level2_Move_Component _mLevel2MoveComponent = null;
    private BossEnemy_Level1_Move_Component _mLevel1MoveComponent = null;

    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private GameObject clockLevel3;
    [SerializeField] private GameObject clockLevel2;
    [SerializeField] private GameObject clockLevel1;

    [SerializeField] private GameObject _mEnemyObstacleCubePrefab;
    [SerializeField] private GameObject _mEnemyBulletPrefab;
    [SerializeField] private GameObject _mBossGhostPrefab;


    private Image currentClockImage;

    [SerializeField] private Animator clockUiAnimator;

    [SerializeField] private GameObject _mDeathParticle;

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
            Destroy(this.GetComponent<BossEnemy_Level3_Move_Component>());
            _mLevel3MoveComponent = null;
        }

        if (_mLevel2MoveComponent != null)
        {
            Destroy(this.GetComponent<BossEnemy_Level2_Move_Component>());
            _mLevel2MoveComponent = null;
        }


        if (_mLevel1MoveComponent != null)
        {
            Destroy(this.GetComponent<BossEnemy_Level1_Move_Component>());
            _mLevel1MoveComponent = null;
        }

        if (currentLevel == 3)
        {
            ChangeLevel3();
            _mLevel3MoveComponent = gameObject.AddComponent<BossEnemy_Level3_Move_Component>();
            _mLevel3MoveComponent.SetObstacleCubePrefab(_mEnemyObstacleCubePrefab);

            currentClockImage = clockLevel3.GetComponent<Image>();

            clockLevel3.SetActive(true);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else if (currentLevel == 2)
        {
            ChangeLevel2();
            _mLevel2MoveComponent = gameObject.AddComponent<BossEnemy_Level2_Move_Component>();
            _mLevel2MoveComponent.SetBulletPrefab(_mEnemyBulletPrefab);

            currentClockImage = clockLevel2.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(true);
            clockLevel1.SetActive(true);
        }
        else
        {
            ChangeLevel1();
            _mLevel1MoveComponent = gameObject.AddComponent<BossEnemy_Level1_Move_Component>();
            _mLevel1MoveComponent.SetBulletPrefab(_mEnemyBulletPrefab);
            _mLevel1MoveComponent.SetGhostPrefab(_mBossGhostPrefab);


            currentClockImage = clockLevel1.GetComponent<Image>();

            clockLevel3.SetActive(false);
            clockLevel2.SetActive(false);
            clockLevel1.SetActive(true);
        }


        clockUiAnimator.Play("ClockUI_LevelChange_Animation");
    }

    void Death()
    {
        Destroy(this.gameObject);
        var particle = GameObject.Instantiate(_mDeathParticle);
        particle.transform.position = transform.position;
        Destroy(particle, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Death();
        }

        if (other.gameObject.tag == "LevelUpItem")
        {
            _mLevel.LevelUp();
            LevelChange();
        }
    }

    void ChangeLevel2()
    {

    }

    void ChangeLevel3()
    {


    }

    void ChangeLevel1()
    {

    }
}
