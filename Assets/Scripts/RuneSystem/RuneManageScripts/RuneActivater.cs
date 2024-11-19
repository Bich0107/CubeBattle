using UnityEngine;
using UnityEngine.UI;

public class RuneActivater : MonoBehaviour
{
    [SerializeField] Transform[] leftRunesActivePos;
    [SerializeField] Transform[] rightRunesActivePos;
    [SerializeField] Transform[] frontRunesActivePos;
    [Space]
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
    }

    void CheckRunes(int _startIndex, Button _button)
    {
        // RuneSO leftRune = runeReaders[_startIndex].Rune;
        // RuneSO rightRune = runeReaders[_startIndex + 1].Rune;
        // RuneSO topRune = runeReaders[_startIndex + 2].Rune;
        // RuneSO bottomRune = runeReaders[_startIndex + 3].Rune;
        // RuneSO middleRune = runeReaders[_startIndex + 4].Rune;
        RuneSO[] activeRunes = new RuneSO[5];
        RuneSO[] passiveRunes = new RuneSO[5];

        int activeCounter = 0;
        int passiveCounter = 0;

        // count and store all active&passive runes to arrays
        for (int i = _startIndex; i < _startIndex + 5; i++)
        {
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
                GameObject passiveRune = Instantiate(passiveRunes[i].RunePrefab, transform);

                // apply each passive rune to all active rune
                for (int j = 0; j < activeCounter; j++)
                {
                    GameObject activeRune = Instantiate(activeRunes[j].RunePrefab, leftRunesActivePos[0].position, Quaternion.identity, transform);
                    activeRuneGOs[j] = activeRune;

                    PassiveToActiveRune(passiveRune, activeRune);
                }
            }

            // activate active runes
            for (int i = 0; i < activeCounter; i++)
            {
                activeRuneGOs[i].GetComponent<ActiveRune>().Activate();
            }
        }
        else
        {
            _button.interactable = false;
        }
    }

    public void Active()
    {
        CheckRunes(0, leftRuneButton);

        foreach (GameObject rune in activeRuneGOs)
        {
            rune.GetComponent<ActiveRune>().Activate();
        }
    }

    void PassiveToActiveRune(GameObject _passive, GameObject _active)
    {
        PassiveRune passiveRune = _passive.GetComponent<PassiveRune>();

        switch (passiveRune.RunePower)
        {
            case RunePower.Pass_EnchanceAttack:
                {
                    IEffectedByAttackPowerEffect target = _active.GetComponent<IEffectedByAttackPowerEffect>();
                    if (target != null)
                        target.Effect_AttackPowerChange(passiveRune.Value);
                    break;
                }
            case RunePower.Pass_EnchanceSize:
                {
                    IEffectedBySizeEffect target = _active.GetComponent<IEffectedBySizeEffect>();
                    if (target != null)
                        target.Effect_SizeChange(passiveRune.Value);
                    break;
                }
            case RunePower.Pass_ReduceCooldown:
                {
                    IEffectedByReduceCDEffect target = _active.GetComponent<IEffectedByReduceCDEffect>();
                    if (target != null)
                        target.Effect_ReduceCD(passiveRune.Value);
                    break;
                }
            case RunePower.Pass_ReduceManaCost:
                {
                    IEffectedByManaCostEffect target = _active.GetComponent<IEffectedByManaCostEffect>();
                    if (target != null)
                        target.Effect_ManaCostChange(passiveRune.Value);
                    break;
                }
            default:
                LogSystem.Instance.Log($"no case for this rune passive: {passiveRune.RunePower}");
                break;
        }
    }
}
