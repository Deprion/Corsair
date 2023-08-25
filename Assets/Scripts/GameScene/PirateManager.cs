using System;
using UnityEngine;

public class PirateManager : MonoBehaviour
{
    public Action EventEnd;

    [SerializeField] private Cannon cannon;

    public void Awake()
    {
        cannon.Setup(GameObject.FindGameObjectWithTag("Player"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            EventEnd?.Invoke();

            Destroy(gameObject);
        }
    }
}
