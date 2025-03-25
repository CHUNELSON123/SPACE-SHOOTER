using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _bestScoreText;
    [SerializeField]
    private Image _liveImg;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private GameObject _restartVisualizer, _exitVisualizer;
    public GameObject _controlInstruct, _shootInstruct;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Player _player;
     
    public int playerScore, bestScore;
    public Animator _levelAnim;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _levelAnim = GameObject.Find("Level_panel").GetComponent<Animator>();
        _scoreText.text = "Score: " + 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _bestScoreText.text = "Best: " + bestScore;
        if (_gameManager == null)
        {
            Debug.LogError("GamaManager is Null");
        } 
    }

    private void Update()
    {
        StartCoroutine(InstructionCountDown());
    }
    public void UpdateScore()
    {
        playerScore += 1;
        _scoreText.text = "Score: " + playerScore;
    }


    public void UpdateLives(int currentLive)
    {
        _liveImg.sprite = _liveSprite[currentLive];

        if(currentLive == 0)
        {
            _restartVisualizer.SetActive(true);
            _exitVisualizer.SetActive(true);
            _gameManager.GameOver();
        }
    }

    public void UpdateBestScore()
    {
        if(playerScore >= bestScore)
        {
            bestScore = playerScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            _bestScoreText.text = "Best: " + bestScore;
        }
    }

     IEnumerator InstructionCountDown()
    {
        yield return new WaitForSeconds(10f);
        _controlInstruct.SetActive(false);
        _shootInstruct.SetActive(false);
        _player.Fire();
    }

    public void DisableInstructions()
    {
        _controlInstruct.SetActive(false);
        _shootInstruct.SetActive(false);
    }
}
