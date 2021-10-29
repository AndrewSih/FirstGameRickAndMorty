using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door1 : MonoBehaviour
{
    [SerializeField] private GameObject finishCanvas;
    void Start()
    {
        finishCanvas.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Касание");
            finishCanvas.SetActive(true);
            //SceneManager.LoadScene(4);
        }
    }
}
