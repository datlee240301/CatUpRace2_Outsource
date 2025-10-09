using System.Collections;
using System.Collections.Generic;
using DucLV;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private ScoreManager scoreManager;
    public Player player;
    public BlockSpawner blockSpawner;

    public int GetScore()
    {
        return scoreManager.GetScore();
    }

    public void PlayGame(bool playGame)
    {
        player.gameObject.SetActive(playGame);
        blockSpawner.gameObject.SetActive(playGame);
    }
}
