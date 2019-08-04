using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    
    int wait = 0;

    // Update is called once per frame
    void Update()
    {
        wait++;
        if(wait >= 10 && Input.GetKeyDown("space")) SceneManager.LoadScene("Main Menu");
    }
}
