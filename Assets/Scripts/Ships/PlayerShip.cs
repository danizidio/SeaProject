using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public void ShipWrecked()
    {
        GetComponent<Animator>().SetTrigger("Sinking");

        GameBehaviour.OnNextGameState.Invoke(GamePlayStates.GAMEOVER);
    }
}
