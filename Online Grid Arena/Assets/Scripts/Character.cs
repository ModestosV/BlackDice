/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;


public class Character : MonoBehaviour
{
    // Placeholder stats
    public CharacterStat Health;
    public CharacterStat Damage;

    [SerializeField] StatPanel statPanel;

    public Character(CharacterStat _Health, CharacterStat _Damage, StatPanel _statPanel)
    {
        this.Health = _Health;
        this.Damage = _Damage;
        this.statPanel = _statPanel;
    }

    private void Awake()
    {
        statPanel.SetStats(Health, Damage);
        statPanel.UpdateStatValues();
    }

}
