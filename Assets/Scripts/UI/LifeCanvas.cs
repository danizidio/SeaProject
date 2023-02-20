using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    Transform _shipToFollow;

    [SerializeField] Image _lifebar;

    private void Start()
    {
        this.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if(_shipToFollow != null)
        {
            transform.position = new Vector2(_shipToFollow.position.x, _shipToFollow.position.y);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ShipToFollow(Transform ship)
    {
        _shipToFollow = ship;
    }

    public void UpdateLifeBar(float currentLife, float maxLife)
    {
        float value = currentLife / maxLife;

        _lifebar.fillAmount = value;

        if (currentLife < 0)
        {
            _lifebar.fillAmount = 0;
        }
    }
}
