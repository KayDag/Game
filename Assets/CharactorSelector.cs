using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSelector : MonoBehaviour
{
    public int index;
    public GameObject[] charactor;
    public GameObject[] charactorPrefab;

    public static GameObject selectedChar;

    void Start()
    {
        index = 0;
        SelectCharactor();
    }

    public void btnPrev()
    {
        index = (index == 0) ? charactor.Length - 1 : index - 1;
        SelectCharactor();
    }

    public void btnNext()
    {
        index = (index == charactor.Length - 1) ? 0 : index + 1;
        SelectCharactor();
    }

    public void SelectCharactor()
    {
        for (int i = 0; i < charactor.Length; i++)
        {
            if (i == index)
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.white;
                selectedChar = charactorPrefab[i];
            }
            else
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
}
