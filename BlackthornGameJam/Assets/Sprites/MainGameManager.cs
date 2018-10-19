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

    private const int miniGameNum = 3;
    private int[] miniLevels;

	private void Start()
	{
        miniLevels = new int[miniGameNum];
        System.Array.Clear(miniLevels, 0, miniLevels.Length);
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
        int miniGameIndex = UnityEngine.Random.Range(0, 2);
        miniLevels[miniGameIndex]++;
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

    public void UnloadMiniGame(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
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
            UnloadMiniGame("StompBug");
        }
    }






    public int GetMiniGameLevel(MiniGame game)
    {
        return miniLevels[(int)game];
    }
}
