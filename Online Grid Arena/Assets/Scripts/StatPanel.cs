/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;

public class StatPanel : MonoBehaviour {

    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStat[] _stats;

    public StatPanel(StatDisplay[] statDisplays, string[] statNames)
    {
        this.statDisplays = statDisplays;
        this.statNames = statNames;
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

    public void SetStats(params CharacterStat[] charStats)
    {
        _stats = charStats;

        if (_stats.Length > statDisplays.Length)
        {
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].Stat = i < _stats.Length ? _stats[i] : null;
            statDisplays[i].gameObject.SetActive(i < _stats.Length);
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            statDisplays[i].ValueText.text = _stats[i].Value.ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }
}
