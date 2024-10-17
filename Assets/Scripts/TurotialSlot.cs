using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurotialSlot : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject backBtn;
    public GameObject bigSign;
    public GameObject referenceBtn;
    public Sprite mySign;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterThisSign()
    {
        tutorial.SetActive(false);
        backBtn.SetActive(true);
        bigSign.SetActive(true);
        referenceBtn.SetActive(true);
        bigSign.GetComponent<Image>().sprite = mySign;
    }

}
