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

    public Character(CharacterStat _health, CharacterStat _damage, StatPanel _statPanel)
    {
        Health = _health;
        Damage = _damage;
        statPanel = _statPanel;
    }

    private void Awake()
    {
        statPanel.SetStats(Health, Damage);
        statPanel.UpdateStatValues();
    }

}
