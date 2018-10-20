using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title, Intro, Main, Win, Lose
}

public enum MiniGame
{
    StompBug = 0, Pain, Piano
}


public class MainGameManager : SingletonBehaviour<MainGameManager> {
    
    public GameState gameState = GameState.Title;
    public MiniGameManagerBase currentGame;

    private int currentLevel;
    private const int miniGameNum = 3;
    private int[] miniLevels;

	private void Start()
	{
        miniLevels = new int[miniGameNum];
        System.Array.Clear(miniLevels, 0, miniLevels.Length);
        currentLevel = 0;
	}

	private void Update()
	{
        if (gameState == GameState.Title)
        {
            HandleInput_Title();
        }

        if (gameState == GameState.Main)
        {
            HandleInput_MiniGame();
        }
	}



    private void GameStart()
    {
        if (gameState == GameState.Main)
            return;
        
        gameState = GameState.Main;
        PlayMiniGame();
    }

    /// <summary>
    /// play random mini game
    /// </summary>
    private void PlayMiniGame()
    {
        int miniGameIndex = Random.Range(0, 3);
        miniGameIndex = 0;
        miniLevels[miniGameIndex]++;
        currentLevel++;
        switch(miniGameIndex)
        {
            case 0:
                SceneManager.LoadScene("StompBug", LoadSceneMode.Additive);
                break;
            case 1:
                SceneManager.LoadScene("StompBug", LoadSceneMode.Additive);
                break;
            case 2:
                SceneManager.LoadScene("StompBug", LoadSceneMode.Additive);
                break;
        }
    }

    public void FinishMiniGame(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);

        StartCoroutine(PrepareForNextMiniGame());
    }

    IEnumerator PrepareForNextMiniGame()
    {
        yield return new WaitForSeconds(1.0f);

        PlayMiniGame();
    }




    private void HandleInput_Title()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    private void HandleInput_MiniGame()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FinishMiniGame("StompBug");
        }
    }


    public float GetMiniGameTime(int miniGameLevel)
    {
        if (miniGameLevel < 5)
            return 5;
        else if (miniGameLevel < 10)
            return 4.5f;
        else if (miniGameLevel < 15)
            return 4;
        else if (miniGameLevel < 20)
            return 3.5f;
        else if (miniGameLevel < 25)
            return 3;
        else if (miniGameLevel < 30)
            return 2.5f;
        else
            return 2;
    }



    public int GetCurrentLevel()
    {
        return currentLevel;
    }



    public int GetMiniGameLevel(MiniGame game)
    {
        int index = (int)game;
        return miniLevels[index];
    }
}
