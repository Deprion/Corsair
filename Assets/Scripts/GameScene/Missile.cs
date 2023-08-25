using UnityEngine;

public class Missile : MonoBehaviour
{
    private float leftTime = 10;
    private float speed = 10;

    private Vector3 dir;

    private void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime <= 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.localPosition += dir * Time.fixedDeltaTime * speed;
    }

    public void Setup(Vector3 pos)
    {
        dir = pos - transform.localPosition;

        if (dir.magnitude > 1)
            dir.Normalize();
    }
}
