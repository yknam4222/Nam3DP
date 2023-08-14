using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get { return instance; } }

    private static StatManager instance;

    public Slider _StBar { get { return StBar; } }

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
        StBar.value = Player.Instance.CurrentST;
    }

    public void UpdatePlayerStats()
    {
        HpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxHP, 50);
        MpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxMP, 50);
        StBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxST, 50);

        StBar.onValueChanged.AddListener(delegate { OnStChanged(); });

        HpBar.maxValue = HpBar_In.maxValue = Player.Instance.MaxHP;
        MpBar.maxValue = MpBar_In.maxValue = Player.Instance.MaxMP;
        StBar.maxValue = StBar_In.maxValue = Player.Instance.MaxST;

        HpBar.value = HpBar_In.value = HpBar.maxValue;
        MpBar.value = MpBar_In.value = MpBar.maxValue;
        StBar.value = StBar_In.value = StBar.maxValue;
    }

    void OnStChanged()
    {
        
    }

    public IEnumerator UpdateValue(Slider UpSlider, Slider UnderSlider)
    {
        float test = 0;
        if (UpSlider.value > UnderSlider.value)
            test = UpSlider.value - UnderSlider.value;
        else
            test = UnderSlider.value = UpSlider.value;
        yield return new WaitForSeconds(1.0f);

        while(UnderSlider.value > UpSlider.value)
        {
            UnderSlider.value -= Time.deltaTime * test;

            yield return null;
        }
    }

    public void Charge()
    {
        StopCoroutine(ChargeCoroutine());
        StartCoroutine(ChargeCoroutine());
    }

    public IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        while(Player.Instance.CurrentST< Player.Instance.MaxST)
        {
            Player.Instance.CurrentST += Time.deltaTime * 50.0f;

            yield return null;
        }
    }
}
