using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get { return instance; } }

    private static StatManager instance;

    public Slider _StBar { get { return StBar; } }
    public Slider _StBarIn { get { return StBar_In; } }

    [SerializeField] private Slider HpBar;
    [SerializeField] private Slider HpBar_In;
    [SerializeField] private Slider MpBar;
    [SerializeField] private Slider MpBar_In;
    [SerializeField] private Slider StBar;
    [SerializeField] private Slider StBar_In;

    Coroutine co = null;

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
        HpBar.value = Player.Instance.CurrentHP;
        MpBar.value = Player.Instance.CurrentMP;
        StBar.value = Player.Instance.CurrentST;

        if (StBar.value > StBar_In.value)
            StBar_In.value = StBar.value;
    }

    public void UpdatePlayerStats()
    {
        HpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxHP, 50);
        MpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxMP, 50);
        StBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance.MaxST, 50);

        HpBar.maxValue = HpBar_In.maxValue = Player.Instance.MaxHP;
        MpBar.maxValue = MpBar_In.maxValue = Player.Instance.MaxMP;
        StBar.maxValue = StBar_In.maxValue = Player.Instance.MaxST;

        HpBar.value = HpBar_In.value = HpBar.maxValue;
        MpBar.value = MpBar_In.value = MpBar.maxValue;
        StBar.value = StBar_In.value = StBar.maxValue;
    }

    public void onValueChanged()
    {

    }

    public void SliderUpdate(Slider UpSlider, Slider UnderSlider)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(UpdateValue(UpSlider, UnderSlider));
    }

    public IEnumerator UpdateValue(Slider UpSlider, Slider UnderSlider)
    {
        yield return new WaitForSeconds(1.0f);

        float gap = UnderSlider.value - UpSlider.value;

        while (UnderSlider.value > UpSlider.value)
        {
                UnderSlider.value -= Time.deltaTime * gap;
            yield return null;
        }
    }
}
