using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];
    private Hero hero;
    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
         //hero = FindObjectOfType<Hero>();
        for (int i =0; i<hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }
    public void Refresh()
    {
        for (int i=0; i < hearts.Length; i++)
        {
            if (i < hero.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
