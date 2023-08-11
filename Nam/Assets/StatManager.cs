using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    [SerializeField]
    private Slider HpBar;
    [SerializeField]
    private Slider MpBar;
    [SerializeField]
    private Slider StBar;
    private void Awake()
    {
    }
    void Start()
    {
        HpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxHP, 50);
        MpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxMP, 50);
        StBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxST, 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
