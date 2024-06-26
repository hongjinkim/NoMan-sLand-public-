public class Wire : Obstacle
{
    private float dmg;
    public float DMG { get => dmg; }
    private void Awake()
    {
        dmg = 1f;
    }
}