using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] RuneSO rune;
    [SerializeField] int maxStack;

    public ObjectIndex ObjectIndex => rune.ObjectIndex;
    public Sprite Sprite => rune.Sprite;
    public RuneSO Rune => rune;
    public string Name => rune.Name;
    public int MaxStack => maxStack;
    public string Description => rune.Description;
}
