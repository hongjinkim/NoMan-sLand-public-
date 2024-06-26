using UnityEngine;

public abstract class SkillEffect : MonoBehaviour
{
    public string effectName = "";
    public abstract void Apply();
}