
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public BlueGuy player;
    public Transform fruits;

    public int EnemyMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int live { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if ( live <= 0 && Input.anyKeyDown  )
        {
            NewGame();
        }
    }
    private void NewGame()
    {
        SetLive(3);
        SetScore(0);
        NewRound();
    }


    private void NewRound()
    {
        foreach (Transform fruit in fruits)
        {
            fruit.gameObject.SetActive(true);
        }

        ResetState();
    }


    private void ResetState()
    {
        ResetMultiplier();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(true);
        }

        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);

        }

        player.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score; 
    }

    private void SetLive(int live)
    {
        this.live = live;
    }

    public void EnemyKilled(Enemy enemy)
    {
        SetScore(score + enemy.points * EnemyMultiplier);
        EnemyMultiplier++;
    }

    public void PlayerKilled()
    {
        player.gameObject.SetActive(false);
        SetLive(live - 1);
        if (live > 0)
        {
          Invoke(nameof(ResetState),3f);
        }
        else
        {
            GameOver();
        }
    }

    public void CherryEaten(Cherry cherry)
    {
        cherry.gameObject.SetActive(false);
        SetScore(score + cherry.points);
        if (!CheckRemaining())
        {
            player.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        } 
    }

    public void OrangeEaten(Orange orange)
    {
        CherryEaten(orange);
        CancelInvoke();
        Invoke(nameof(ResetMultiplier), orange.duration);
    }

    public bool CheckRemaining()
    {
        foreach (Transform fruit in fruits)
        {
            if (fruit.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetMultiplier()
    {
        EnemyMultiplier = 1;
    }
}
