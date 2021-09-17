using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUIScript : MonoBehaviour
{
    [SerializeField] private Level _mPlayerLevel;
    [SerializeField] private Text _mLevelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int level = _mPlayerLevel.GetLevel();

        _mLevelText.text = "LEVEL " + level;
    }
}
