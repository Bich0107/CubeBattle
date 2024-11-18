using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneDisplayer : MonoBehaviour
{
    [Header("Left runes")]
    [SerializeField] Image leftRuneTop;
    [SerializeField] Image leftRuneBottom;
    [SerializeField] Image leftRuneLeft;
    [SerializeField] Image leftRuneRight;
    [SerializeField] Image leftRuneMiddle;
    [Header("Right runes")]
    [SerializeField] Image rightRuneTop;
    [SerializeField] Image rightRuneBottom;
    [SerializeField] Image rightRuneLeft;
    [SerializeField] Image rightRuneRight;
    [SerializeField] Image rightRuneMiddle;
    [Header("Front runes")]
    [SerializeField] Image frontRuneTop;
    [SerializeField] Image frontRuneBottom;
    [SerializeField] Image frontRuneLeft;
    [SerializeField] Image frontRuneRight;
    [SerializeField] Image frontRuneMiddle;
    [Header("Rune setters")]
    [SerializeField] RuneSetter frontRuneSetter;
    [SerializeField] RuneSetter backRuneSetter;
    [SerializeField] RuneSetter topRuneSetter;
    [SerializeField] RuneSetter bottomRuneSetter;
    bool lastTurnDirectionIsLeft = false;
    bool isFacingForward = true;


    void Start()
    {
        Display();
    }

    public void Display()
    {
        if (isFacingForward)
        {
            leftRuneTop.sprite = topRuneSetter.GetRune(PartFace.Left).Sprite;
            leftRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Left).Sprite;
            leftRuneLeft.sprite = frontRuneSetter.GetRune(PartFace.Left).Sprite;
            leftRuneRight.sprite = backRuneSetter.GetRune(PartFace.Left).Sprite;
            leftRuneMiddle.sprite = null;

            rightRuneTop.sprite = topRuneSetter.GetRune(PartFace.Right).Sprite;
            rightRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Right).Sprite;
            rightRuneLeft.sprite = frontRuneSetter.GetRune(PartFace.Right).Sprite;
            rightRuneRight.sprite = backRuneSetter.GetRune(PartFace.Right).Sprite;
            rightRuneMiddle.sprite = null;

            frontRuneTop.sprite = topRuneSetter.GetRune(PartFace.Front).Sprite;
            frontRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Back).Sprite;
            frontRuneMiddle.sprite = frontRuneSetter.GetRune(PartFace.Top).Sprite;
            frontRuneLeft.sprite = null;
            frontRuneRight.sprite = null;
        }
        else
        {
            leftRuneTop.sprite = topRuneSetter.GetRune(PartFace.Front).Sprite;
            leftRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Back).Sprite;
            leftRuneMiddle.sprite = frontRuneSetter.GetRune(PartFace.Top).Sprite;
            leftRuneLeft.sprite = null;
            leftRuneRight.sprite = null;

            rightRuneTop.sprite = topRuneSetter.GetRune(PartFace.Back).Sprite;
            rightRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Front).Sprite;
            rightRuneMiddle.sprite = backRuneSetter.GetRune(PartFace.Top).Sprite;
            rightRuneLeft.sprite = null;
            rightRuneRight.sprite = null;

            frontRuneTop.sprite = topRuneSetter.GetRune(PartFace.Front).Sprite;
            frontRuneBottom.sprite = bottomRuneSetter.GetRune(PartFace.Back).Sprite;
            frontRuneLeft.sprite = frontRuneSetter.GetRune(PartFace.Right).Sprite;
            frontRuneRight.sprite = backRuneSetter.GetRune(PartFace.Right).Sprite;
            frontRuneMiddle.sprite = null;
        }
    }

    public void RotateForward()
    {
        RuneSetter temp = frontRuneSetter;
        frontRuneSetter = topRuneSetter;
        topRuneSetter = backRuneSetter;
        backRuneSetter = bottomRuneSetter;
        bottomRuneSetter = temp;
        Display();
    }

    public void RotateBackward()
    {
        RuneSetter temp = frontRuneSetter;
        frontRuneSetter = bottomRuneSetter;
        bottomRuneSetter = backRuneSetter;
        backRuneSetter = topRuneSetter;
        topRuneSetter = temp;
        Display();
    }

    public void TurnLeft()
    {
        Debug.Log("tunr left");
        if (!lastTurnDirectionIsLeft)
        {
            lastTurnDirectionIsLeft = true;
        }
        else
        {
            RuneSetter temp = frontRuneSetter;
            frontRuneSetter = backRuneSetter;
            backRuneSetter = temp;
        }

        topRuneSetter.TurnRight();
        bottomRuneSetter.TurnLeft();
        isFacingForward = !isFacingForward;

        Display();
    }

    public void TurnRight()
    {
        Debug.Log("tunr right");
        if (lastTurnDirectionIsLeft)
        {
            lastTurnDirectionIsLeft = false;
        }
        else
        {
            RuneSetter temp = frontRuneSetter;
            frontRuneSetter = backRuneSetter;
            backRuneSetter = temp;
        }

        topRuneSetter.TurnLeft();
        bottomRuneSetter.TurnRight();
        isFacingForward = !isFacingForward;

        Display();
    }
}
