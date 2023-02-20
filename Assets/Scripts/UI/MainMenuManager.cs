using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Slider _gameTime, _spawnTime, _soundBGM, _soundSFX;

    void Start()
    {
        Time.timeScale = 1;

        _spawnTime.GetComponent<Slider>().value = NavigationData.nData.SpawnTime;
        _gameTime.GetComponent<Slider>().value = NavigationData.nData.GameTime;
        _soundBGM.GetComponent<Slider>().value = NavigationData.nData.VolumeBgm;
        _soundSFX.GetComponent<Slider>().value = NavigationData.nData.VolumeSfx;
    }
}
