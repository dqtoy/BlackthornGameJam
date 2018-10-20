using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_StompBug : MiniGameManagerBase {

    public MiniGame game = MiniGame.StompBug;

    public GameObject groundBugPrefab;
    public GameObject airBugPrefab;
    public Transform leftGroundSpawn;
    public Transform rightGroundSpawn;
    public Transform leftAirSpawn;
    public Transform rightAirSpawn;

    private bool gamePlaying;
    private float gameTime;
    private System.DateTime startTime;
    private int bugNum;
    private int bulletNum;
    private List<BugData> bugs;

	private void OnEnable()
	{
        MiniGameTimerUI.OnTimeUp += OnTimeUp;
	}

	private void OnDisable()
	{
        MiniGameTimerUI.OnTimeUp -= OnTimeUp;
	}

	private void Start()
	{
        gamePlaying = false;
        LoadGame();
        StartGame();
	}

	public override void StartGame()
    {
        gamePlaying = true;
        startTime = System.DateTime.Now;
        MiniGameTimerUI.Instance.StartTimer();

        SpawnBugs();
    }

    public override void EndGame()
    {
        MainGameManager.Instance.FinishMiniGame("StompBug");
    }

    public void OnTimeUp()
    {
        //play effect
        EndGame();
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
            GameObject bug = Instantiate(groundBugPrefab, spawnLocation.position, Quaternion.identity);
            LinearMove movementCtrl = bug.GetComponent<LinearMove>();
            movementCtrl.Init(data.fromLeft, data.speed);
        }
        else
        {
            Transform spawnLocation = data.fromLeft ? leftAirSpawn : rightAirSpawn;
            GameObject bug = Instantiate(airBugPrefab, spawnLocation.position, Quaternion.identity);
            SineMove movementCtrl = bug.GetComponent<SineMove>();
            movementCtrl.Init(data.fromLeft, data.speed, spawnLocation.position.y, -1, -1);
        }

    }

    private void SetupTimer()
    {
        int level = MainGameManager.Instance.GetMiniGameLevel(game);
        float seconds = MainGameManager.Instance.GetMiniGameTime(level);
        MiniGameTimerUI.Instance.ResetTimer(seconds);
    }

    public override void LoadGame()
    {
        int level = MainGameManager.Instance.GetMiniGameLevel(game);
        SetupTimer();

        if (level == 1)
        {
            bugNum = 1;
            bugs = new List<BugData>();
            bugs.Add(new BugData(2, 3));
        }
        else if (level == 2)
        {
            bugNum = 2;
            bugs = new List<BugData>();
            bugs.Add(new BugData(1, 3));
            bugs.Add(new BugData(1.5f, 3));
        }
        else if (level == 3)
        {
            bugNum = 3;
            bugs = new List<BugData>();
            bugs.Add(new BugData(1, 5));
            bugs.Add(new BugData(1.5f, 5));
            bugs.Add(new BugData(2f, 5));
        }
        else if (level > 3 && level < 8)
        {
            int randomMode = Random.Range(0, 2);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 4);
                bugs = new List<BugData>();
                for (int i = 0; i < bugNum; i++)
                    bugs.Add(new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f)));
            }
        }
        else if (level >= 8 && level < 15)
        {
            int randomMode = Random.Range(0, 2);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 4);
                bugs = new List<BugData>();
                for (int i = 0; i < bugNum; i++)
                    bugs.Add(new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f)));
            }
            else if (randomMode == 1)
            {
                bugNum = Random.Range(1, 3);
                bugs = new List<BugData>();
                for (int i = 0; i < bugNum; i++)
                    bugs.Add(new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(8, 12), false, false, (Random.value > 0.5f)));
            }
        }
        else if (level >= 15)
        {
            int randomMode = Random.Range(0, 2);
            if (randomMode == 0)
            {
                bugNum = Random.Range(2, 4);
                bugs = new List<BugData>();
                for (int i = 0; i < bugNum; i++)
                    bugs.Add(new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(3, 8), false, false, (Random.value > 0.5f)));
            }
            else if (randomMode == 1)
            {
                bugNum = Random.Range(1, 3);
                bugs = new List<BugData>();
                for (int i = 0; i < bugNum; i++)
                    bugs.Add(new BugData(1.0f + Random.Range(0, 2.0f), Random.Range(8, 12), false, false, (Random.value > 0.5f)));
            }
        }

    }
}
