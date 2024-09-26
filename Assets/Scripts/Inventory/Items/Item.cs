using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] ItemIndex itemIndex;
    [SerializeField] Sprite sprite;
    [SerializeField] int maxStack;
    [SerializeField] string description;

    public ItemIndex ItemIndex => itemIndex;
    public Sprite Sprite => sprite;
    public string Name => name;
    public int MaxStack => maxStack;
    public string Description => description;
}
