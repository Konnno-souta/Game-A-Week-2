using UnityEngine;
using UnityEngine.UI;


public class WoodManager : MonoBehaviour
{
    public bool infiniteWood = true;  // ← 無限モードを追加
    public int woodCount = 0;
    public Text woodText;


    //void Start()
    //{
    //    UpdateUI();
    //}


    public bool HasWood() => woodCount > 0;


    public bool ConsumeWood()
    {
        if (infiniteWood)
        {
            return true;   // 無限だから常にOK
        }

        // 本来の処理（必要なら残す）
        if (woodCount > 0)
        {
            woodCount--;
            return true;
        }
        return false;
    }
    public int GetWoodCount()
    {
        if (infiniteWood) return 9999;   // UI用の見た目だけ多くする
        return woodCount;
    }

    public void AddWood(int amount)
    {
        woodCount += amount;
        //UpdateUI();
    }


    //void UpdateUI()
    //{
    //    if (woodText != null) woodText.text = $"Wood: {woodCount}";
    //}
}