using UnityEngine;

public class ShipAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;

    [SerializeField] private Color color, colorOrigin;

    private float interval = 0.15f, leftTime;

    private bool run;

    public void Run()
    { 
        run = true;
    }

    public void Stop()
    {
        run = false;
        image.color = colorOrigin;
    }

    private void Update()
    {
        if (!run) return;

        leftTime -= Time.deltaTime;

        if (leftTime > 0) return;

        leftTime = interval;

        if (image.color == color) image.color = colorOrigin;
        else image.color = color;
    }
}
