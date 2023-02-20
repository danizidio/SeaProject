
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour instance;
    public delegate GamePlayStates _onNextGameState(GamePlayStates gameStates);
    public static _onNextGameState OnNextGameState;

    public delegate int _onEarningPoints();
    public static _onEarningPoints OnEarningPoints;

    GamePlayStates _gamePlayPreviousState;
    public GamePlayStates GamePlayPreviousState { get { return _gamePlayPreviousState; } }
    
    [SerializeField] GamePlayStates _gamePlayCurrentState;
    public GamePlayStates GamePlayCurrentState { get { return _gamePlayCurrentState; } }
    
    GamePlayStates _gamePlayNextState;
    public GamePlayStates GamePlayNextState { get { return _gamePlayNextState; } }

    [SerializeField] GameObject _pauseMenu, _gameOverMenu, _scoreTxt;

    [SerializeField] GameObject[] _playerShips;
    GameObject _currentPlayerShip;

    [SerializeField] GameObject[] _stages;

    GameObject _currentStage;

    static int _playerScore;

    Timer _timer;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OnNextGameState(GamePlayStates.INITIALIZING);

        OnEarningPoints = EarnPoints;

        _timer = FindObjectOfType<Timer>();

    }

    private void Update()
    {
        StateBehaviour(GamePlayCurrentState);

        UpdateState();
    }

    void StateBehaviour(GamePlayStates state)
    {
        switch (state)
        {
            case GamePlayStates.INITIALIZING:
                {
                    _pauseMenu.SetActive(false);
                    _gameOverMenu.SetActive(false);

                    _playerScore = 0;

                    try
                    {
                        NavigationData.nData.SetSfxVolume(NavigationData.nData.VolumeSfx);
                        NavigationData.nData.SetSoundVolume(NavigationData.nData.VolumeBgm);
                    }
                    catch
                    {
                        AudioSource audio1 = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
                        AudioSource audio2 = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
                        audio1.volume = 1;
                        audio2.volume = 1;
                    }

                    if (_currentStage != null)
                    {
                        Destroy(_currentStage);

                        _currentStage = Instantiate(_stages[Random.Range(0, _stages.Length)]);
                    }
                    else
                    {
                        _currentStage = Instantiate(_stages[Random.Range(0, _stages.Length)]);
                    }

                    if (_currentPlayerShip != null)
                    {
                        Destroy(_currentPlayerShip);

                        _currentPlayerShip = Instantiate(_playerShips[Random.Range(0, _playerShips.Length)], new Vector2(0, 0), Quaternion.identity);
                    }
                    else
                    {
                        _currentPlayerShip = Instantiate(_playerShips[Random.Range(0, _playerShips.Length)], new Vector2(0,0), Quaternion.identity);
                    }

                    OnNextGameState.Invoke(GamePlayStates.START);

                    break;
                }
            case GamePlayStates.START:
                {
                    _timer.IsTicTimer = true;

                    OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

                    break;
                }
            case GamePlayStates.GAMEPLAY:
                {
                    Time.timeScale = 1;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        PauseGame();
                    }

                    break;
                }
            case GamePlayStates.PAUSE:
                {
                    Time.timeScale = 0;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        PauseGame();
                    }

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    _timer.IsTicTimer = false;

                    Destroy(_currentPlayerShip);

                    _gameOverMenu.SetActive(true);

                    Time.timeScale = 0;

                    break;
                }
        }
    }

    public GamePlayStates NextGameStates(GamePlayStates newState)
    {
        _gamePlayPreviousState = _gamePlayCurrentState;
        return _gamePlayNextState = newState;
    }

    public GamePlayStates UpdateState()
    {
        return _gamePlayCurrentState = _gamePlayNextState;
    }

    public GamePlayStates GetCurrentGameState()
    {
        return GamePlayCurrentState;
    }

    void PauseGame()
    {
        if (GetCurrentGameState() != GamePlayStates.PAUSE)
        {
            _timer.IsTicTimer = false;

            _pauseMenu.SetActive(true);
            OnNextGameState?.Invoke(GamePlayStates.PAUSE);
        }
        else
        {
            _timer.IsTicTimer = true;

            _pauseMenu.SetActive(false);
            OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
        }
    }

    public void PauseButton()
    {
        if (GetCurrentGameState() != GamePlayStates.PAUSE)
        {
            _timer.IsTicTimer = false;

            _pauseMenu.SetActive(true);
            OnNextGameState?.Invoke(GamePlayStates.PAUSE);
        }
        else
        {
            _timer.IsTicTimer = false;

            _pauseMenu.SetActive(false);
            OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
        }
    }

    int EarnPoints()
    {
        _playerScore++;

        _scoreTxt.GetComponent<TMPro.TMP_Text>().text = _playerScore.ToString();
        
        if(_playerScore > NavigationData.GetHighScore())
        {
            NavigationData.SetHighScore(_playerScore);

            SetHighScore.OnSetHighScore?.Invoke();
        }

       return _playerScore;
    }

    public static int GetScore()
    {
        return _playerScore;
    }

    private void OnEnable()
    {
        OnNextGameState += NextGameStates;
    }
    private void OnDisable()
    {
        OnNextGameState -= NextGameStates;
    }
}

