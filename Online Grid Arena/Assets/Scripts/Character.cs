/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;
using Kryz.CharacterStats;


public class Character : MonoBehaviour {

    // Placeholder stats
    public CharacterStat Health;
    public CharacterStat Damage;

    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(Health, Damage);
        statPanel.UpdateStatValues();
    }

}
