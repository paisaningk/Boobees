using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this Script use in Start Game Scene
public class StarGame : MonoBehaviour
{
    void Update()
    {
        //Press mouse 0 button to skip movie.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("MainMenu_PC");
        }
    }

    void A()
    {
        //if movie end load scene main menu
        SceneManager.LoadScene("MainMenu_PC");
    }
}
