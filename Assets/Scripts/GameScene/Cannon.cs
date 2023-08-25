using UnityEngine;

public class Cannon : MonoBehaviour
{
    private GameObject ship;
    [SerializeField] private GameObject prefabMis;

    [SerializeField] private AudioSource source;

    private float interval = 2;
    private float left = 2;

    public void Setup(GameObject go)
    {
        Events.Level.AddListener(Level);

        ship = go;
    }

    public void ChangeInterval(float val)
    { 
        interval += val;
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

            pos.x += Random.Range(-1f, 1f);
            pos.y += Random.Range(-1f, 1f);

            left = interval;

            if (PlayerPrefs.GetInt("muteS") == 0)
                source.Play();

            var mis = Instantiate(prefabMis);
            mis.transform.localPosition = transform.position;
            mis.GetComponent<Missile>().Setup(pos);
        }
    }

    private void OnDestroy()
    {
        Events.Level.RemoveListener(Level);
    }
}
