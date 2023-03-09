
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public BlueGuy player;
    public Transform fruits;

    public Text gameOverText;
    public Text scoreText;
    public Text liveText;
    private GameObject back;
    private GameObject again;

    public int EnemyMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int live { get; private set; }

    private void Start()
    {
        NewGame();
        again = GameObject.FindWithTag("Again button");
        again.SetActive(false);
        back = GameObject.FindWithTag("Back button");
        back.SetActive(false);
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
        gameOverText.enabled = false;
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
            
            enemies[i].ResetState();
        }

        player.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);

        }

        player.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score; 
        scoreText.text = score.ToString().PadLeft(2,'0');
    }

    private void SetLive(int live)
    {
        this.live = live;
        liveText.text = live.ToString();
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
            int index = SceneManager.GetActiveScene().buildIndex;
            if(index == 0)
            {
                Invoke("ChangeScene",3);
            }
            else if(index == 1)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].gameObject.SetActive(false);
                }
                again.SetActive(true);
                back.SetActive(true);
            }
        } 
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
    }
        
    public void OrangeEaten(Orange orange)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].weak.Enable(orange.duration);
        }

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

    public void AgainOnClick()
    {
        SceneManager.LoadScene("Pixel Adventure 2", LoadSceneMode.Single);
    }

    public void BackOnClick()
    {
        SceneManager.LoadScene("Main Menu",LoadSceneMode.Single);
    }

    private void ResetMultiplier()
    {
        EnemyMultiplier = 1;
    }
}
