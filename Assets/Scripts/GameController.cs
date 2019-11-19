using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text scoreText;                        //A reference to the UI text component that displays the player's score.
    public GameObject gameOvertext;                //A reference to the object that displays the text which appears when the player dies.

    private int score = 0;                        //The player's score.
    public bool gameOver = false;                //Is the game over?
    public float scrollSpeed = -1.5f;

    public AudioClip audioScore;
    public AudioClip audioGameOver;
    public AudioClip audioHit;
    private AudioSource MediaPlayerScore;
    private AudioSource MediaPlayerGameOver;
    private AudioSource MediaPlayerGameOverHit;
    void Awake()
    {
        MediaPlayerScore = gameObject.AddComponent<AudioSource>();
        MediaPlayerScore.clip = audioScore;

        MediaPlayerGameOver = gameObject.AddComponent<AudioSource>();
        MediaPlayerGameOver.clip = audioGameOver;

        MediaPlayerGameOverHit = gameObject.AddComponent<AudioSource>();
        MediaPlayerGameOverHit.clip = audioHit;

        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    void Update()
    {
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        MediaPlayerScore.Play();
        //The bird can't score if the game is over.
        if (gameOver)
            return;
        //If the game is not over, increase the score...
        score++;
        //...and adjust the score text.
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        MediaPlayerGameOverHit.Play();
        MediaPlayerGameOver.Play();
        //Activate the game over text.
        gameOvertext.SetActive(true);
        //Set the game to be over.
        gameOver = true;
    }

}
