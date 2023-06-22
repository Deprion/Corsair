using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private AudioSource source;

    public void Setup(ShipSO ship)
    {
        image.sprite = ship.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Events.AddCoin.Invoke();

            if(PlayerPrefs.GetInt("muteS") == 0)
                source.Play();
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);

            Events.End.Invoke();
            Destroy(gameObject);
        }
    }
}
