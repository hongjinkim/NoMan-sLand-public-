using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    private UnityEngine.UI.Button button;
    private TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickBtnSkill(int num)
    {
        var skill = Managers.Game.controller.UseSkill(num);

        StartCoroutine(StartCoolTime(skill.data));
    }

    private IEnumerator StartCoolTime(SkillData data)
    {
        button.interactable = false;

        float elapsedTime = 0f;

        while (elapsedTime < data.coolTime)
        {
            text.text = (data.coolTime - elapsedTime).ToString("F1") + "s";
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.text = data.name;
        button.interactable = true;
    }
}
