using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] private float MAXHP = 30;
    [SerializeField] private float _mCurrentHP;

    // Start is called before the first frame update
    private void Start()
    {
        _mCurrentHP = MAXHP;
    }

    private void Damage(float damage)
    {
        _mCurrentHP = Mathf.Clamp(_mCurrentHP -= damage, 0, MAXHP);
    }

    public void ResetHP()
    {
        _mCurrentHP = MAXHP;
    }

    public bool IsEmpty()
    {
        return _mCurrentHP <= 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet"
           || collision.gameObject.tag == "Explode")
        {
            var damage = collision.gameObject.GetComponent<Damage>().GetDamage();
            Damage(damage);
        }
    }
}
