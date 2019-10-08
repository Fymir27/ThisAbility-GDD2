using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObjective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Controller.Instance.BonusObjectives.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Picked up bonus objective!");
            Destroy(gameObject);
            Score.Instance.IncrementBonusObjective();
        }
    }
}
