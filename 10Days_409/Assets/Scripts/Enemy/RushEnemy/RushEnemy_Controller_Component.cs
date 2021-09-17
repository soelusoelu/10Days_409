using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RushEnemy_Controller_Component : MonoBehaviour
{
    private Level _mLevel;
    private RushEnemy_Level3_Move_Component _mLevel3MoveComponent = null;
    private RushEnemy_Level2_Move_Component _mLevel2MoveComponent = null;
    private RushEnemy_Level1_Move_Component _mLevel1MoveComponent = null;

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

            LevelChange();
        }
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
        }
        else if (currentLevel == 2)
        {
            _mLevel2MoveComponent = gameObject.AddComponent<RushEnemy_Level2_Move_Component>();
            ChangeLevel2();
        }
        else
        {
            _mLevel1MoveComponent = gameObject.AddComponent<RushEnemy_Level1_Move_Component>();
            ChangeLevel1();
        }
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
