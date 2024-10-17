using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject turorial;
    public GameObject bigSign;
    public GameObject backBtn;
    public PageController pageController;
    public GameObject referenceBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void leaveGame()
    {
        Application.Quit();
    }

    public void enterTurorial()
    {
        menu.SetActive(false);
        turorial.SetActive(true);
    }


    public void backToMenu()
    {
        pageController.prevPage();
        pageController.prevPage();
        pageController.prevPage();
        pageController.prevPage();

        menu.SetActive(true);
        turorial.SetActive(false);
    }

    public void getReference()
    {
        Application.OpenURL("https://special.moe.gov.tw/signlanguage/basis/detail/7c338ab6-87a9-46cc-a50f-34b82b4dac8a");
    }

    public void backToTutorial()
    {
        turorial.SetActive(true);
        bigSign.SetActive(false);
        backBtn.SetActive(false);
        referenceBtn.SetActive(false);
    }
}
