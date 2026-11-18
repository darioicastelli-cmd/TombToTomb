using UnityEngine;
using TMPro;
using System.Collections;

public class DoorWithRequirement : DoorInteractable
{
    [Header("Requisitos de inventario")]
    [SerializeField] private ItemId requiredItem;
    [SerializeField] private int requiredAmount;
    [SerializeField] private Player_Inventory playerInventory;
    [SerializeField] private ItemIconDatabase iconDatabase;

    [Header("UI en la puerta")]
    [SerializeField] private TextMeshProUGUI requirementText;

    [Header("UI de aviso general")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;

    private Coroutine hideRoutine;

    private void Start()
    {
        UpdateRequirementUI();
        if (playerInventory != null)
            playerInventory.OnInventoryChanged += UpdateRequirementUI;
    }

    private void OnDestroy()
    {
        if (playerInventory != null)
            playerInventory.OnInventoryChanged -= UpdateRequirementUI;
    }

    private string ItemLabel =>
        iconDatabase != null ? iconDatabase.GetIconTag(requiredItem) : requiredItem.ToString();

    public override void Interact()
    {
        if (playerInventory == null)
        {
            Debug.LogError("No se asignó el Player_Inventory en el inspector.");
            return;
        }

        int currentAmount = playerInventory.GetItemCount(requiredItem);

        if (currentAmount >= requiredAmount)
        {
            base.Interact();
        }
        else if (warningPanel != null && warningText != null)
        {
            warningPanel.SetActive(true);
            warningText.text =
                $"No tienes suficiente para abrir esta puerta.\nTienes {currentAmount} de  {ItemLabel}\nNecesitas {requiredAmount}.";

            if (hideRoutine != null) StopCoroutine(hideRoutine);
            hideRoutine = StartCoroutine(HideWarningAfterDelay(3f));
        }
    }

    private void UpdateRequirementUI()
    {
        if (requirementText == null || playerInventory == null) return;

        int currentAmount = playerInventory.GetItemCount(requiredItem);
        requirementText.text = $"Tienes {currentAmount} de  {ItemLabel}\nNecesitas {requiredAmount}";
    }

    private IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningPanel.SetActive(false);
    }
}