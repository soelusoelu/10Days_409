using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionShieldComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            GameObject.Destroy(coll.gameObject);
        }

        // TODO::���ƂŃA�j���[�V����
    }
}
