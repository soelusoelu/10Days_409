using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{

    private bool Judge;

    // Start is called before the first frame update
    void Start()
    {
        Judge = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetJudge()
    {
        return Judge;
    }

    public void setJudge(bool j)
    {
        Judge = j;
    }
}
