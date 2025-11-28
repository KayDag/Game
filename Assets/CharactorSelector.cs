using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharactorSelector : MonoBehaviour
{
    public int index;
    public GameObject[] charactor;
    public static TextMeshProUGUI charactorName;
    public static GameObject selectedChar;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        SelectCharactor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btnPrev()
    {
        if (index != 0)
        {
            index--;
        }
        else
        {
            index = charactor.Length - 1;
        }
        SelectCharactor();
    }
    public void btnNext()
    {
        if (index < charactor.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        SelectCharactor();
    }
    public void SelectCharactor()
    {
        for (int i = 0; i < charactor.Length; i++)
        {
            if (i == index)
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.white;
                selectedChar = charactor[i];
                charactorName.text = charactor[i].name;
            }
            else
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
}
