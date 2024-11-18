using UnityEngine;

public class RuneReaderController : MonoBehaviour
{
    [SerializeField] RuneReader[] runeReaders;

    void Start()
    {
        Read();
    }

    public void Read()
    {
        for (int i = 0; i < runeReaders.Length; i++)
        {
            runeReaders[i].ReadRune();
        }
    }
}
