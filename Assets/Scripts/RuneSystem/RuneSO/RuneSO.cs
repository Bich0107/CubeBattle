using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune System/RuneSO")]
public class RuneSO : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] new string name;
    [TextArea(3, 5)]
    [SerializeField] string description;
    [SerializeField] Sprite sprite;
    [SerializeField] RuneType runeType;
    [SerializeField] ObjectIndex objIndex;
    [SerializeField] GameObject runePrefab;

    public int Id => id;
    public string Name => name;
    public string Description => description;
    public Sprite Sprite => sprite;
    public RuneType RuneType => runeType;
    public ObjectIndex ObjectIndex => objIndex;
    public GameObject RunePrefab => runePrefab;

    public GameObject RuneGO;
}
