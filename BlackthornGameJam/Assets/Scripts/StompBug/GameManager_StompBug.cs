using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_StompBug : MiniGameManagerBase {

    public MiniGame game = MiniGame.StompBug;

    public GameObject bugPrefab;
    public Transform leftGroundSpawn;
    public Transform rightGroundSpawn;
    public Transform leftAirSpawn;
    public Transform rightAirSpawn;

    private int bugNum;
    private int bulletNum;
    private List<BugData> bugs;

	private void Start()
	{
        LoadGame();
	}


    public override void StartGame()
    {
        
    }

    public override void EndGame()
    {
        MainGameManager.Instance.UnloadMiniGame("StompBug");
    }

    private void SpawnBugs()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            BugData data = bugs[i];
            StartCoroutine(SpawnBug(bugs[i]));
        }
    }

    IEnumerator SpawnBug(BugData data)
    {
        yield return new WaitForSeconds(data.spawnTime);

        if (!data.flying)
        {
            Transform spawnLocation = data.fromLeft ? leftGroundSpawn : rightGroundSpawn;
            GameObject bug = Instantiate(bugPrefab, spawnLocation.position, Quaternion.identity);
            Bug bugCtrl = bug.GetComponent<Bug>();

        }

    }


    public override void LoadGame()
    {
        int level = MainGameManager.Instance.GetMiniGameLevel(game);

        if (level == 1)
        {
            bugNum = 1;
            bugs = new List<BugData>(bugNum);
            bugs[0] = new BugData(2, 3);
        }
        else if (level == 2)
        {
            bugNum = 2;
            bugs = new List<BugData>(bugNum);
            bugs[0] = new BugData(1, 3);
            bugs[1] = new BugData(1.5f, 3);
        }
        else if (level == 3)
        {
            bugNum = 3;
            bugs = new List<BugData>(bugNum);
            bugs[0] = new BugData(1, 6);
            bugs[1] = new BugData(1.5f, 6);
        }
        else if (level > 3 && level < 8)
        {
            int randomMode = Random.Range(0, 1);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 3);
                bugs = new List<BugData>(bugNum);
                for (int i = 0; i < bugNum; i++)
                    bugs[i] = new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f));
            }
        }
        else if (level >= 8 && level < 15)
        {
            int randomMode = Random.Range(0, 1);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 3);
                bugs = new List<BugData>(bugNum);
                for (int i = 0; i < bugNum; i++)
                    bugs[i] = new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f));
            }
            else if (randomMode == 1)
            {
                bugNum = Random.Range(1, 2);
                bugs = new List<BugData>(bugNum);
                for (int i = 0; i < bugNum; i++)
                    bugs[i] = new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(8, 12), false, false, (Random.value > 0.5f));
            }
        }
        else if (level >= 15)
        {
            int randomMode = Random.Range(0, 1);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 3);
                bugs = new List<BugData>(bugNum);
                for (int i = 0; i < bugNum; i++)
                    bugs[i] = new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f));
            }
            else if (randomMode == 1)
            {
                bugNum = Random.Range(1, 2);
                bugs = new List<BugData>(bugNum);
                for (int i = 0; i < bugNum; i++)
                    bugs[i] = new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(8, 12), false, false, (Random.value > 0.5f));
            }
        }

    }
}
