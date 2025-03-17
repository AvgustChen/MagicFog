using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text amountDamageText;
    [SerializeField] private Canvas canvas;
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();

        SetHealthBar();
    }

    public void GetHit(int amountDamage)
    {
            SetHealthBar();
            TMP_Text hitText = Instantiate(amountDamageText, amountDamageText.transform.position, Quaternion.identity);
            //hitText.transform.parent = canvas.transform;
            hitText.gameObject.SetActive(true);
            hitText.text = Mathf.Round(amountDamage).ToString();
            hitText.transform.localScale = new Vector3(0f, 0f, 0f);
            hitText.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => Destroy(hitText));

    }

    public void Die()
    {
        canvas.gameObject.SetActive(false);
    }

    private void SetHealthBar()
    {
        healthBar.fillAmount = enemyAI.GetHealth() / enemyAI.GetHealthMax();
    }
}
