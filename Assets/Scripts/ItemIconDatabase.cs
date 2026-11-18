using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIconDatabase", menuName = "Game/Item Icon Database")]
public class ItemIconDatabase : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public ItemId item;
        public string spriteName;
    }

    [Header("Ajuste visual del ícono en el texto")]
    [SerializeField] private float sizePercent = 160f;      // tamaño relativo al texto
    [SerializeField] private float verticalOffsetEm = 0.15f; // positivo = sube, negativo = baja

    [SerializeField] private List<Entry> entries = new List<Entry>();

    public string GetIconTag(ItemId id)
    {
        foreach (var e in entries)
        {
            if (e.item.Equals(id) && !string.IsNullOrEmpty(e.spriteName))
            {
                string offset = verticalOffsetEm.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);
                string size = sizePercent.ToString("0.#", System.Globalization.CultureInfo.InvariantCulture);
                return $"<size={size}%><voffset={offset}em><sprite name=\"{e.spriteName}\"></voffset></size>";
            }
        }
        return id.ToString();
    }
}