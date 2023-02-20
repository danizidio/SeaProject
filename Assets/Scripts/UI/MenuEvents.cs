using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{

    public void OnMenu()
    {
        this.GetComponent<Animator>().SetTrigger("OnMenu");
    }
}
