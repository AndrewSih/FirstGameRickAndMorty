using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Awake()
    {
        if (!player)
           // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().transform;
        player = FindObjectOfType<Hero>().transform;
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().transform;
        pos = player.position;
       
        pos.z = -10f;
        pos.y += 2f;
        pos.x += 3f;
        transform.position = Vector3.Lerp(transform.position , pos, Time.deltaTime);
    }
}
