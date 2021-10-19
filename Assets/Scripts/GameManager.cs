using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get;private set;}

    public Vector2 spawnPoint = new Vector2(0, 0);
    public GameObject playerPrefab;
    [HideInInspector] public int robotNum;
    [HideInInspector] public bool hasTask = false;
    [HideInInspector] public bool ifCompleteTask = false;

    void Awake() {
        // 单例
        if (instance == null) {
            instance = this;
        }else {
            Destroy(gameObject);
        }

        // 生成角色
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player==null) {
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }


        robotNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
