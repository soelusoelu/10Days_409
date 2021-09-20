using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingScript : MonoBehaviour
{
    [SerializeField] private GameObject _mPlayer;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cameraPos = transform.position;
        var playerPos = _mPlayer.transform.position;

        transform.position = Vector3.Lerp(cameraPos, playerPos + offset, 0.01f);
    }
}
