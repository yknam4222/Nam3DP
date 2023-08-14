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
    [SerializeField] private Slider MpBar_In; 
    [SerializeField] private Slider StBar;
    [SerializeField] private Slider StBar_In; 

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
       
    }

    public void UpdatePlayerStats()
    {
        HpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.maxHp, 50);
        MpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.maxMp, 50);
        StBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.maxSt, 50);

        HpBar.maxValue = HpBar_In.maxValue = Player.Instance.maxHp;
        MpBar.maxValue = MpBar_In.maxValue = Player.Instance.maxMp;
        StBar.maxValue = StBar_In.maxValue = Player.Instance.maxSt;
    }

    public IEnumerator UpdateHpValue()
    {
        yield return new WaitForSeconds(1.0f);

        while(HpBar_In.value > HpBar.value)
        {
            HpBar_In.value -= Time.deltaTime * 5f;

            yield return null;
        }
    }
}
