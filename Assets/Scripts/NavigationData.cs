using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SaveStrings
{
    HIGHSCORE,
    MUSIC,
    SFX,
    GAMETIME,
    SPAWNRATE,
    FIRSTUSE
}
public class NavigationData : MonoBehaviour
{
    public static NavigationData nData;

    [SerializeField] OptionsValues optionsValues;

    [SerializeField] int _targetFrameRate;

    float _gameTime, _spawnTime, _volume, _sfx;
    public float GameTime { get { return _gameTime; } }
    public float SpawnTime { get { return _spawnTime; } }
    public float VolumeBgm { get { return _volume; } }
    public float VolumeSfx { get { return _sfx; } }

    static int _highScore;
    public int HighScore { get { return _highScore; } }

    GameObject[] Datas;

    private void Awake()
    {
        nData = this;

        DontDestroyOnLoad(this.gameObject);

        Datas = GameObject.FindGameObjectsWithTag("NAVIGATION_DATA");

        if (Datas.Length > 1)
        {
            Destroy(Datas[1]);
        }

        if (PlayerPrefs.HasKey(SaveStrings.FIRSTUSE.ToString()))
        {
            _gameTime = PlayerPrefs.GetFloat(SaveStrings.GAMETIME.ToString());
            _spawnTime = PlayerPrefs.GetFloat(SaveStrings.SPAWNRATE.ToString());
            _highScore = PlayerPrefs.GetInt(SaveStrings.HIGHSCORE.ToString());
            _volume = PlayerPrefs.GetFloat(SaveStrings.MUSIC.ToString());
            _sfx = PlayerPrefs.GetFloat(SaveStrings.SFX.ToString());
        }
        else
        {
            _gameTime = optionsValues.GameSessionTime;
            _spawnTime = optionsValues.EnemySpawnTime;

            _highScore = optionsValues.HighScore;

            _volume = 1;
            _sfx = 1;

            PlayerPrefs.SetInt(SaveStrings.FIRSTUSE.ToString(), 1);
        }
    }

    private void Start()
    {     
        Application.targetFrameRate = _targetFrameRate;
    }

    public void ChangingGameTime(float v)
    {
        _gameTime = v;

        optionsValues.GameSessionTime = _gameTime;

        PlayerPrefs.SetFloat(SaveStrings.GAMETIME.ToString(), _gameTime);
    }
    public void ChangingSpawnTime(float v)
    {
        _spawnTime = v;

        optionsValues.EnemySpawnTime = _spawnTime;

        PlayerPrefs.SetFloat(SaveStrings.SPAWNRATE.ToString(), _spawnTime);
    }

    public static void SetHighScore(int value)
    {
        _highScore = value;

        PlayerPrefs.SetInt(SaveStrings.HIGHSCORE.ToString(), _highScore);
    }

    public static int GetHighScore()
    {
        return _highScore;
    }

    public void SetSoundVolume(float bgm)
    {
        _volume = bgm;

        AudioSource audio = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        if (audio != null) audio.volume = _volume;

        PlayerPrefs.SetFloat(SaveStrings.MUSIC.ToString(), _volume);
    }

    public void SetSfxVolume(float sfx)
    {
        _sfx = sfx;

        AudioSource audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        if (audio == null)
            return;
        
        audio.volume = _sfx;

        PlayerPrefs.SetFloat(SaveStrings.SFX.ToString(), _sfx);
    }
}
