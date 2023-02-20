using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannons : MonoBehaviour
{
    [SerializeField] GameObject _canonnball;

    [SerializeField] Transform _sideShot,  _frontShot;

    bool _canShoot = true;

    GameBehaviour gb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            if (GameBehaviour.instance.GetCurrentGameState() != GamePlayStates.GAMEPLAY)
                return;
            
            StartCoroutine(CannonShot(_frontShot, 1));
        }
        if (Input.GetMouseButtonDown(1) && _canShoot)
        {
            if (GameBehaviour.instance.GetCurrentGameState() != GamePlayStates.GAMEPLAY)
                return;
     
            StartCoroutine(TripleCannonShot(_sideShot, 3));
        }
    }

    IEnumerator CannonShot( Transform shotSide, float timing)
    {
        _canShoot = false;

        GameObject temp = Instantiate(_canonnball, new Vector2(shotSide.position.x, shotSide.position.y), Quaternion.Euler(shotSide.position.x, shotSide.position.y, shotSide.rotation.z));

        temp.GetComponent<Cannonball>().ShotDirection(shotSide.right);
        
        yield return new WaitForSeconds(timing);

        _canShoot = true;

        StopCoroutine("CannonShot");
    }
    IEnumerator TripleCannonShot(Transform shotSide, float timing)
    {
        _canShoot = false;

        GameObject temp1 = Instantiate(_canonnball, new Vector2(shotSide.position.x, shotSide.position.y), Quaternion.Euler(shotSide.position.x, shotSide.position.y, shotSide.rotation.z));

        temp1.GetComponent<Cannonball>().ShotDirection(shotSide.right);

        yield return new WaitForSeconds(.1f);

        GameObject temp2 = Instantiate(_canonnball, new Vector2(shotSide.position.x, shotSide.position.y), Quaternion.Euler(shotSide.position.x, shotSide.position.y +20f, shotSide.rotation.z));

        temp2.GetComponent<Cannonball>().ShotDirection(shotSide.right);

        yield return new WaitForSeconds(.2f);

        GameObject temp3 = Instantiate(_canonnball, new Vector2(shotSide.position.x, shotSide.position.y), Quaternion.Euler(shotSide.position.x, shotSide.position.y - 20f, shotSide.rotation.z));

        temp3.GetComponent<Cannonball>().ShotDirection(shotSide.right);

        yield return new WaitForSeconds(timing);

        _canShoot = true;

        StopCoroutine("TripleCannonShot");
    }

}
