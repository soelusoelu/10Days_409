using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] private float _mMaxHP = 30;
    [SerializeField] private float _mCurrentHP;
    [SerializeField] private Animator _mMainAnimator;
    [SerializeField] private GameObject _mDamageParticle;

    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _DamageAudioClip;

    public float GetHP()
    {
        return _mCurrentHP;
    }

    public float GetMaxHP()
    {
        return _mMaxHP;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _mCurrentHP = _mMaxHP;

        _mMainAnimator = GetComponent<Animator>();

        _AudioSource = GetComponent<AudioSource>();

    }

    private void Damage(float damage)
    {
        _mCurrentHP = Mathf.Clamp(_mCurrentHP -= damage, 0, _mMaxHP);
        _mMainAnimator.Play("EnemyDamageAnimation");
        var particle = GameObject.Instantiate(_mDamageParticle);
        particle.transform.position = transform.position;
        Destroy(particle, 3.0f);
    }

    private void Update()
    {
    }

    public void ResetHP()
    {
        _mCurrentHP = _mMaxHP;
    }

    public bool IsEmpty()
    {
        return _mCurrentHP <= 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            var damage = other.gameObject.GetComponent<Damage>().GetDamage();
            Damage(damage);
            GameObject.Destroy(other.gameObject);

            _AudioSource.PlayOneShot(_DamageAudioClip);

        }

        if (other.gameObject.tag == "Explode")
        {
            var damage = other.gameObject.GetComponent<Damage>().GetDamage();
            Damage(damage);
        }
    }


}
