using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    int page = 0;
    public Text pageText;

    public GameObject prevGreyBtn;
    public GameObject prevBtn;
    public GameObject nextGreyBtn;
    public GameObject nextBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextPage()
    {
        if(page == 3)
        {
            return;
        }

        page++;
        pageText.text = (page+1).ToString();

        pages[0].SetActive(false);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);

        pages[page].SetActive(true);

        if(page == 1)
        {
            prevGreyBtn.SetActive(false);
            prevBtn.SetActive(true);
        }
        else if(page == 3)
        {
            nextGreyBtn.SetActive(true);
            nextBtn.SetActive(false);
        }

    }

    public void prevPage()
    {
        if (page == 0)
        {
            return;
        }

        page--;
        pageText.text = (page+1).ToString();

        pages[0].SetActive(false);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);

        pages[page].SetActive(true);

        if (page == 2)
        {
            nextGreyBtn.SetActive(false);
            nextBtn.SetActive(true);
        }
        else if (page == 0)
        {
            prevGreyBtn.SetActive(true);
            prevBtn.SetActive(false);
        }

    }
}
