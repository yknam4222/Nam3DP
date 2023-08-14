using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get { return instance; } }

    private static StatManager instance;

    [SerializeField] private Slider HpBar; 
    [SerializeField] private Slider HpBar_In; 
    [SerializeField] private Slider MpBar; 
    [SerializeField] private Slider StBar; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            DestroyImmediate(gameObject);
    }
    void Start()
    {
        UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HpBar.value = 0.7f;
        }
        if(Input.GetMouseButtonDown(1))
            StartCoroutine(UpdateHpValue());

    }

    public void UpdatePlayerStats()
    {
        HpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxHP, 50);
        MpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxMP, 50);
        StBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxST, 50);
    }

    public IEnumerator UpdateHpValue()
    {

        while(HpBar_In.value < 0.7f)
        {
                HpBar_In.value -= Time.deltaTime * 5f;

            yield return null;
        }
    }
}
