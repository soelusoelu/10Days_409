using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    private Level level;
    private OptionShooterManager optionManager;

    private void Start() {
        level = GetComponent<Level>();
        level.OnUpdateLevel(ChangeLevel);

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
            optionManager.EndPerformance();
        } else if (nextLevel == 2) {
            optionManager.StartPerformance();
        } else if (nextLevel == 3) {

        } else {
            Debug.Assert(false);
        }
    }
}
