using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIScript : MonoBehaviour
{
    [SerializeField] private WaveSystem _mWaveSystem;
    [SerializeField] private Text _mWaveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mWaveText.text = "Wave " + (_mWaveSystem.GetCurrentWave() + 1);
    }
}
