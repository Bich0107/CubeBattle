using UnityEngine;

public class RuneReaderController : MonoBehaviour
{
    [SerializeField] RuneReader[] runeReaders;

    public RuneReader[] RuneReaders => runeReaders;

    public void Read()
    {
        for (int i = 0; i < runeReaders.Length; i++)
        {
            runeReaders[i].ReadRune();
        }
    }
}
