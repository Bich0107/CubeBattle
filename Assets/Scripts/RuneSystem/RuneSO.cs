using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune System/RuneSO")]
public class RuneSO : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] new string name;
    [TextArea(3, 5)]
    [SerializeField] string description;
    [SerializeField] Sprite sprite;

    public int Id => id;
    public string Name => name;
    public string Description => description;
    public Sprite Sprite => sprite;
}
