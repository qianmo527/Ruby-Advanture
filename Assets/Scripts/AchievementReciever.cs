using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementReciever : MonoBehaviour
{
    public AchievementSender achievementSystem {get; private set;}

    private void Awake() {
        achievementSystem = GameObject.FindGameObjectWithTag("AchievementSender").GetComponent<AchievementSender>();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        if (achievementSystem != null) {
            achievementSystem.achievementEvent.AddListener(OnRecieve);
        }
    }

    public void OnRecieve(string behaviour) {
        print(behaviour);
    }
}
