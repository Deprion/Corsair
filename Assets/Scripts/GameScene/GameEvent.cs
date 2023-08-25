using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class GameEvent : MonoBehaviour
{
    [SerializeField] private GameObject piratePrefab, tentaclePrefab;
    [SerializeField] private Cannon cannon;

    [SerializeField] private float baseChance, addChance;
    [SerializeField] private float interval;

    private Vector2 spawnPoint = new Vector2(12, 4);

    private float sumChance, leftTime;

    private bool isEventGoing;

    private void Awake()
    {
        isEventGoing = false;
        leftTime = interval;
        sumChance = baseChance;
    }

    private void Update()
    {
        if (isEventGoing) return;

        leftTime -= Time.deltaTime;

        if (leftTime > 0) return;

        RollEvent();

        leftTime = interval;
    }

    private void RollEvent()
    {
        if (Random.value < sumChance)
        {
            isEventGoing = true;
            sumChance = baseChance;

            if (Random.Range(0, 2) == 1) 
            {
                PirateEvent();
            }
            else
            {
                TentacleEvent();
            }
        }
        else 
        {
            sumChance += addChance;
        }
    }

    private void TentacleEvent()
    {
        cannon.ChangeInterval(2);

        int max = Random.Range(2, 4);

        for (int i = 0; i < max; i++)
        {
            var tent = Instantiate(tentaclePrefab);

            if (i == 0) tent.GetComponent<Tentacle>().EventEnd += EventEnd;

            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            tent.transform.localPosition = new Vector2
                (Mathf.Cos(angle) * 5, Mathf.Sin(angle) * 5);
        }
    }

    private void PirateEvent()
    {
        cannon.ChangeInterval(2);

        var pirate = Instantiate(piratePrefab);

        pirate.transform.localPosition = spawnPoint;

        pirate.GetComponent<PirateManager>().EventEnd += EventEnd;

    }

    private void EventEnd()
    {
        isEventGoing = false;
        cannon.ChangeInterval(-2);
    }
}
