/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] public StatDisplay[] statDisplays;
    [SerializeField] public string[] statNames;

    private CharacterStat[] stats;

    public StatPanel(StatDisplay[] statDisplays, string[] statNames)
    {
        this.statDisplays = statDisplays;
        this.statNames = statNames;
    }

    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        if (stats.Length > statDisplays.Length)
        {
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].stat = i < stats.Length ? stats[i] : null;
            statDisplays[i].gameObject.SetActive(i < stats.Length);
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].valueText.text = stats[i].Value.ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].nameText.text = statNames[i];
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }
}
