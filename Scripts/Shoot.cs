using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Shoot : MonoBehaviour
{
    public Hero playerHero;
    public EventTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        playerHero = FindObjectOfType<Hero>();
       // playerHero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) => { playerHero.Shoot(); });

        /* EventTrigger.Entry entry1 = new EventTrigger.Entry();
         entry1.eventID = EventTriggerType.PointerUp;
         entry1.callback.AddListener((eventData) => { playerHero.OnButtonUp(); });*/

        trigger.triggers.Add(entry);
        //trigger.triggers.Add(entry1);
    }
}
