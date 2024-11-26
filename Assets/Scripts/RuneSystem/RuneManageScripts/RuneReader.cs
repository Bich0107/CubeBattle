using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Read <see langword="and"/> store rune info <see langword="from"/> rune face</summary>
public class RuneReader : MonoBehaviour
{
    static readonly float s_ray_distance = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] LayerMask runeLayer;
    [SerializeField] Image runeImage;
    [SerializeField] RuneSO rune;
    RaycastHit hit;
    RuneFace runeFace;

    public RuneSO Rune => rune;

    public void ReadRune()
    {
        if (Physics.Raycast(transform.position, direction, out hit, s_ray_distance, runeLayer, QueryTriggerInteraction.Collide))
        {
            runeFace = hit.collider.gameObject.GetComponent<RuneFace>();
            rune = runeFace.GetRune();
            runeImage.sprite = rune.Sprite;
        }
        else
        {
            rune = null;
            runeImage.sprite = null;
        }
    }
}
