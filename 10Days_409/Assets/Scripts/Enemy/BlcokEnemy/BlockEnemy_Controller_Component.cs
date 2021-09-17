using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }


    void LevelChange()
    {

        int currentLevel = _mLevel.GetLevel();

        // 効率悪いやり方かも
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
        }
        else if (currentLevel == 2)
        {
            _mLevel2MoveComponent = gameObject.AddComponent<BlockEnemy_Level2_Move_Component>();
            ChangeLevel2();
        }
        else
        {
            _mLevel1MoveComponent = gameObject.AddComponent<BlockEnemy_Level1_Move_Component>();
            ChangeLevel1();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
        var particle = GameObject.Instantiate(_mDeathParticle);
        particle.transform.position = transform.position;
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
