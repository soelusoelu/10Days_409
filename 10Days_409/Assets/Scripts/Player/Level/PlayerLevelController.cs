using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    private Level level;
    private OptionShooterManager optionManager;
    private int currentLevel = 0;

    private void Start() {
        level = GetComponent<Level>();
        level.OnUpdateLevel(ChangeLevel);
        currentLevel = level.GetLevel();

        optionManager = transform.GetChild(0).GetComponent<OptionShooterManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            level.LevelUp();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            level.LevelDown();
        }
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
            } else if (currentLevel == 3) {

            }
        } 
        
        if (nextLevel == 3) {
            if (currentLevel == 2) {

            }
        }

        currentLevel = nextLevel;
    }
}
