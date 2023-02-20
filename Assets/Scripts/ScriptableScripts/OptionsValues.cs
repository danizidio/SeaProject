using UnityEngine;

[CreateAssetMenu(fileName = "Option Values", menuName = "Add Options Values", order = 0)]
public class OptionsValues : ScriptableObject
{
    [SerializeField] float _gameSessionTime;
    public float GameSessionTime{ get { return _gameSessionTime;}set { _gameSessionTime = value; } }

    [SerializeField] float _enemySpawnTime;
    public float EnemySpawnTime { get { return _enemySpawnTime; } set { _enemySpawnTime = value; } }

    [SerializeField] int _highScore;
    public int HighScore { get { return _highScore; } set { _highScore = value; } }

    [SerializeField] float _soundBGM;
    public float SoundBGM { get { return _soundBGM; } set { _soundBGM = value; } }

    [SerializeField] float _soundSFX;
    public float SoundSFX { get { return _soundSFX; } set { _soundSFX = value; } }
}
