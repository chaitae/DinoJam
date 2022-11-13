using UnityEngine;

public class OnFireSpriteChange: OnFireListener
{
    public SpriteRenderer spriteRenderer;
    public Color fireColor;

    public override void SetOnFire()
    {
        spriteRenderer.color = fireColor;
    }
}
