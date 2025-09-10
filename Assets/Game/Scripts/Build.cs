using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Build : MonoBehaviour
{
    [Header("Wood Required")]
    [SerializeField] int woodRequired;
    [SerializeField] TextMeshProUGUI woodText;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        woodText.text = woodRequired.ToString();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        woodText.text = woodRequired.ToString();
    }

    public bool GetMaterial()
    {
        woodRequired -= player.UseWood(woodRequired);
        woodText.text = woodRequired.ToString();
        if (woodRequired == 0) return true;
        else return false;
    }
}
