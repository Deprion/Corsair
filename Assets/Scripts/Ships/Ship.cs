using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private AudioSource source;

    [SerializeField] private ShipAnim anim;

    private bool vulnerable = true;

    private int HP;

    [SerializeField] private float invTime;
    private float leftTime;

    public void Setup(ShipSO ship)
    {
        HP = DataManager.instance.data.harbor.GetThis().HP;

        image.sprite = ship.Sprite;
    }

    private void Update()
    {
        if (vulnerable) return;

        leftTime -= Time.deltaTime;

        if (leftTime <= 0)
        {
            anim.Stop();
            vulnerable = true;
        }
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
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            if (!vulnerable) return;

            GetDamage();
        }
        else if (collision.CompareTag("EventEnemy"))
        {
            if (!vulnerable) return;

            GetDamage();
        }
    }

    private void GetDamage()
    {
        HP--;

        Events.GetHit.Invoke();

        if (HP <= 0)
        {
            Events.End.Invoke();
            Destroy(gameObject);
        }
        else
        {
            leftTime = invTime;

            vulnerable = false;

            anim.Run();
        }
    }
}
