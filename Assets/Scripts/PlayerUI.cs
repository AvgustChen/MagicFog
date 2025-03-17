using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    [SerializeField] private Image heathBar;
    [SerializeField] private Text heathText;
    [SerializeField] private Image manaBar;
    [SerializeField] private Text manaText;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text progressText;
    [SerializeField] private Text levelText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetHealth();
        SetMana();
        SetProgress();
        SetLevel();
    }

    public void SetHealth()
    {
        heathText.text = PlayerData.Instance.GetHeath().ToString() + "/" + PlayerData.Instance.GetHeathMax();
        heathBar.fillAmount = PlayerData.Instance.GetHeath() / PlayerData.Instance.GetHeathMax();
        Debug.Log(PlayerData.Instance.GetHeath() / PlayerData.Instance.GetHeathMax());
    }

    public void SetMana()
    {
        manaText.text = PlayerData.Instance.GetMana().ToString() + "/" + PlayerData.Instance.GetManaMax();
        manaBar.fillAmount = PlayerData.Instance.GetMana() / PlayerData.Instance.GetManaMax();
    }

    public void SetProgress()
    {
        progressText.text = PlayerData.Instance.GetProgress().ToString() + "/" + PlayerData.Instance.GetProgressMax();
        progressBar.fillAmount = PlayerData.Instance.GetProgress() / PlayerData.Instance.GetProgressMax();
    }

    public void SetLevel()
    {
        levelText.text = PlayerData.Instance.GetLevel().ToString();
    }
}
