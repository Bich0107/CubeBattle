using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune System/RuneSO")]
public class RuneSO : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] new string name;
    [TextArea(3, 5)]
    [SerializeField] string description;
    [SerializeField] float value;
    [SerializeField] Sprite sprite;
    [SerializeField] RuneType runeType;
    [SerializeField] RunePower runePower;
    [SerializeField] GameObject runePrefab;

    public int Id => id;
    public string Name => name;
    public string Description => description;
    public float Value => value;
    public Sprite Sprite => sprite;
    public RuneType RuneType => runeType;
    public RunePower RunePower => runePower;
    public GameObject RunePrefab => runePrefab;
}
