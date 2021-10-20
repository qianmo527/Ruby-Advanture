using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get;private set;}

    public Vector2 spawnPoint = new Vector2(0, 0);
    public GameObject playerPrefab;
    [HideInInspector] public int robotNum;
    public bool hasTask = false;
    public bool ifCompleteTask = false;

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

        // 获取剩余机器人数量
        robotNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // StartCoroutine("SearchRobot");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SearchRobot() {
        while (true){
            if (robotNum <= 0) {
                yield return null;
            }

            GameObject[] robots = GameObject.FindGameObjectsWithTag("Enemy");
            robotNum = robots.Length;
            print(robotNum);
            yield return new WaitForSeconds(3);
        }
    }
}
