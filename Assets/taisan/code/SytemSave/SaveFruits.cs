using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SaveFruits : MonoBehaviour
{
    [SerializeField] private string Id;

    private bool collected = false;
    public static SaveFruits SF;

    [ContextMenu("Generate New ID")]
    private void PassId()
    {
        Id = System.Guid.NewGuid().ToString();
    }
    private void Awake()
    {
        SF = this;
    }
    public void Save(ref FruitsSaveData Data)
    {
        if (Data.CollectionFruits.ContainsKey(Id))
        {
            Data.CollectionFruits.Remove(Id);
        }
        Data.CollectionFruits.Add(Id, collected);
        Debug.Log($"[SaveFruits] Save Id={Id}, collected={collected}");
    }
    public void Load(FruitsSaveData Data)
    {
        Data.CollectionFruits.TryGetValue(Id, out collected);
        if (collected == true)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collected = true;
            SytemSave.MarkFruitCollected(Id, collected);
        }
    }
}
[System.Serializable]
public class FruitsSaveData// Những dữ liệu lớn thì dùng class
{
    public SerializableDictionary<string, bool> CollectionFruits = new SerializableDictionary<string, bool>();
}
