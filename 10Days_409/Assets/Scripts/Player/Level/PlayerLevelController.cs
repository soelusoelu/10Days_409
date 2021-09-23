using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    private Level level;
    private OptionShooterManager optionManager;
    private ShieldManager shieldManager;
    private List<Shield> shields;
    private int currentLevel = 0;

    private void Start() {
        level = GetComponent<Level>();
        level.OnUpdateLevel(ChangeLevel);
        currentLevel = level.GetLevel();

        var score = GameObject.Find("Score").GetComponent<Score>();
        level.OnDead(score.DisableUpdate);

        optionManager = transform.GetChild(0).GetComponent<OptionShooterManager>();
        shieldManager = transform.GetChild(1).GetComponent<ShieldManager>();
    }

    private void ChangeLevel() {
        int nextLevel = level.GetLevel();

        if (nextLevel == 1) {
            if (currentLevel == 2) {
                optionManager.EndPerformance();
            }
        }
        
        if (nextLevel == 2) {
            if (currentLevel == 1) {
                optionManager.StartPerformance();
            }
        } 
        
        if (nextLevel == 3) {
            if (currentLevel == 2) {
                shieldManager.StartPerformance();
            }
        }

        currentLevel = nextLevel;
    }
}
