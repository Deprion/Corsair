using System;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public Action EventEnd;

    [SerializeField] private GameObject tentacle;
    [SerializeField] private float aliveTime, spawnTime;

    private float timeLeft;
    private bool spawned;

    private void Awake()
    {
        timeLeft = aliveTime;
    }

    private void Update()
    {
        if (!spawned)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0) tentacle.SetActive(true);
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft > 0) return;

        EventEnd?.Invoke();

        Destroy(gameObject);
    }
}
