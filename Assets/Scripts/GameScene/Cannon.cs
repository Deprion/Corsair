using UnityEngine;

public class Cannon : MonoBehaviour
{
    private GameObject ship;
    [SerializeField] private GameObject prefabMis;

    [SerializeField] private AudioSource source;

    private float interval = 1.5f;
    private float left = 1.5f;

    public void Setup(GameObject go)
    {
        Events.Level.AddListener(Level);

        ship = go;
    }

    private void Level()
    {
        interval -= 0.05f;
    }

    private void Update()
    {
        if (ship == null) return;

        left -= Time.deltaTime;

        if (left <= 0)
        {
            Vector2 pos = ship.GetComponent<ShipMovement>().Next();

            left = interval;

            if (PlayerPrefs.GetInt("muteS") == 0)
                source.Play();

            var mis = Instantiate(prefabMis);
            mis.GetComponent<Missile>().Setup(pos);
        }
    }

    private void OnDestroy()
    {
        Events.Level.RemoveListener(Level);
    }
}
