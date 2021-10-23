using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AchievementEvent: UnityEvent<string> {

}

public class AchievementSender : MonoBehaviour
{
    public AchievementEvent achievementEvent;

    private void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (achievementEvent == null) {
            achievementEvent = new AchievementEvent();
        }
        // achievementEvent.Invoke("Send an event");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
