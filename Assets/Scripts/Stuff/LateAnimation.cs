using UnityEngine;
using UnityEngine.UI;

public class LateAnimation : Animation
{
    [SerializeField] protected float firstWait;

    protected override void SetUp()
    {
        image = GetComponent<SpriteRenderer>();
        image.sprite = sprites[index];
        leftTime = firstWait;
    }

    protected override void Anim()
    {
        leftTime -= Time.deltaTime;

        if (leftTime <= 0)
        {
            index = index + 1 >= sprites.Length ? 0 : index + 1;

            image.sprite = sprites[index];

            if (index == 0) leftTime = firstWait;
            else leftTime = timer;
        }
    }
}
