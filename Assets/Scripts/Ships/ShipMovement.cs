using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 5f;
    private float angle = 0;
    private float x = 0, y = 0;

    private int dir = -1;

    private void Awake()
    {
        speed = DataManager.instance.data.harbor.GetThis().Speed;
        Events.Click.AddListener(ChangeDir);
    }

    private void ChangeDir()
    {
        dir *= -1;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir *= -1;
        }
    }
#endif

    public Vector2 Next()
    {
        var temp = angle + (Time.fixedDeltaTime + 0.5f) * speed * dir;

        x = Mathf.Cos(temp) * radius;
        y = Mathf.Sin(temp) * radius;

        return new Vector2(x, y);
    }

    public Vector2 NextTest()
    {
        var temp = angle + (Time.fixedDeltaTime + 0.1f) * speed * dir;

        x = Mathf.Cos(temp) * radius;
        y = Mathf.Sin(temp) * radius;

        return new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        angle += Time.fixedDeltaTime * speed * dir;

        x = Mathf.Cos(angle) * radius;
        y = Mathf.Sin(angle) * radius;

        transform.localPosition = new Vector2(x, y);
    }

    private void OnDestroy()
    {
        Events.Click.RemoveListener(ChangeDir);
    }
}
