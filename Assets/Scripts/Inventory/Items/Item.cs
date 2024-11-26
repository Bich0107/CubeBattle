using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] ItemIndex itemIndex;
    [SerializeField] RuneSO rune;
    [SerializeField] int maxStack;

    public ItemIndex ItemIndex => itemIndex;
    public Sprite Sprite => rune.Sprite;
    public RuneSO Rune => rune;
    public string Name => rune.Name;
    public int MaxStack => maxStack;
    public string Description => rune.Description;
}
