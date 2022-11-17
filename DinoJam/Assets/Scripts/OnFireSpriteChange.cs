using UnityEngine;

public class OnFireSpriteChange: OnFireListener
{
    public SpriteRenderer spriteRenderer;
    public Color fireColor;

    public override void SetOnFire(bool givePoints = true) 
    {
        spriteRenderer.color = fireColor;
    }
}
