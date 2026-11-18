using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Script_All_HPSelf playerHP;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (playerHP == null || fillImage == null)
            return;

        fillImage.fillAmount =
            (float)playerHP.GetHealthPoints() /
            playerHP.GetMaxHealthPoints();
    }
}