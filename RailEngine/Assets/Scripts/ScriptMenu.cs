using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject credits;

	void _Play ()
    {
        SceneManager.LoadScene(1);
	}
	
	void _Quit ()
    {
        Application.Quit();
	}

    void _Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    void _MainMenu()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

}
    /* Credits Text to copy into scene
    Marshall R Mason: Lead Programmer
    Gus Catalano: Character and World Design
    Kelsey Brownlee: Level Design and Modeling
    Colton Clark: UI design and Additional Modeling

    Textures:
    Industrial Textures by Arkham Interactive
    Prototype Textures by Dexsoft Games

    */