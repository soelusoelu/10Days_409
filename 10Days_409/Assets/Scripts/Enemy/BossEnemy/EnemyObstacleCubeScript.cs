using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObstacleCubeScript : MonoBehaviour
{
    private Level _mLevel;
    [SerializeField] private float _mMoveSpeed = 0.003f;


    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private GameObject clockLevel3;
    [SerializeField] private GameObject clockLevel2;
    [SerializeField] private GameObject clockLevel1;

    private Image currentClockImage;

    [SerializeField] private GameObject _mDeathParticle;
    [SerializeField] private Animator clockUiAnimator;
    private Enemy_Damage _mEnemyDamage;


    // Start is called before the first frame update
    void Start()
    {
        _mEnemyDamage = GetComponent<Enemy_Damage>();
        currentClockImage = clockLevel1.GetComponent<Image>();

        clockLevel3.SetActive(false);
        clockLevel2.SetActive(false);

        _mLevel = GetComponent<Level>();
        _mLevel.OnDead(Death);

    }

    void UpdateClockUI()
    {
        float value = _mEnemyDamage.GetHP() / _mEnemyDamage.GetMaxHP();
        clockHandTransform.eulerAngles = new Vector3(0, 0, 360.0f - (360 * value));

        currentClockImage.fillAmount = value;
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

        }

        UpdateClockUI();
        Move();
    }

    void Move()
    {
        var vec = -transform.forward;

        var currentPos = transform.position;

        currentPos += vec * _mMoveSpeed;

        transform.position = currentPos;
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
}
