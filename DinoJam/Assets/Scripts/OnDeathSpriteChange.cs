using UnityEngine;

public class OnDeathSpriteChange : OnDeathListener
{
    public SpriteRenderer spriteRenderer;
    public Color deathColor;

    public override void Killed()
    {
        spriteRenderer.color = deathColor;
    }
}
