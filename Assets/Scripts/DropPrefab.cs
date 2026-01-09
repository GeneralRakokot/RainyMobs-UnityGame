using UnityEngine;

[System.Serializable]
public class DropPrefab
{
    public GameObject prefab;       // Префаб для дропа
    [Range(0, 100)]
    public float chance;            // шанс выпадения этого префаба в %
}