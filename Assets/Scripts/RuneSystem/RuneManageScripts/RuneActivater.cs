using UnityEngine;
using UnityEngine.UI;

/// <summary>Process infor <see langword="from"/> rune reader <see langword="and"/> activate rune combos</summary>
public class RuneActivater : MonoBehaviour
{
    [SerializeField] Button leftRuneButton;
    [SerializeField] Button rightRuneButton;
    [SerializeField] Button frontRuneButton;
    [Space]
    [SerializeField] RuneReaderController runeReaderController;
    RuneReader[] runeReaders => runeReaderController.RuneReaders;
    GameObject[] activeRuneGOs;

    void Start()
    {
        activeRuneGOs = new GameObject[5];
    }

    public void Check()
    {
        // check left
        CheckRunes(0, leftRuneButton);
        // check middle
        CheckRunes(5, rightRuneButton);
        // check right
        CheckRunes(10, frontRuneButton);
    }

    void CheckRunes(int _startIndex, Button _button)
    {
        RuneSO[] activeRunes = new RuneSO[5];
        RuneSO[] passiveRunes = new RuneSO[5];

        int activeCounter = 0;
        int passiveCounter = 0;

        // count and store all active&passive runes to arrays
        for (int i = _startIndex; i < _startIndex + 5; i++)
        {
            if (runeReaders[i].Rune == null) continue;

            if (runeReaders[i].Rune.RuneType == RuneType.Passive)
            {
                passiveRunes[passiveCounter] = runeReaders[i].Rune;
                passiveCounter++;
            }
            else
            {
                activeRunes[activeCounter] = runeReaders[i].Rune;
                activeCounter++;
            }
        }

        // only activate runes if there is at least 1 active rune
        if (activeCounter > 0)
        {
            _button.interactable = true;

            // add effect of passive runes to active runes
            for (int i = 0; i < passiveCounter; i++)
            {
                GameObject passiveRune = passiveRunes[i].RuneGO;

                // apply each passive rune to all active rune
                for (int j = 0; j < activeCounter; j++)
                {
                    activeRuneGOs[j] = activeRunes[j].RuneGO;

                    PassiveToActiveRune(passiveRune, activeRunes[j].RuneGO);
                }
            }
        }
        else
        {
            _button.interactable = false;
        }
    }

    public void Active()
    {
        foreach (GameObject rune in activeRuneGOs)
        {
            rune?.GetComponent<ActiveRune>().Activate();
        }
    }

    void PassiveToActiveRune(GameObject _passive, GameObject _active)
    {
        if (_passive == null || _active == null) return;

        PassiveRune passiveRune = _passive.GetComponent<PassiveRune>();
        passiveRune.Empower(_active);
    }
}
