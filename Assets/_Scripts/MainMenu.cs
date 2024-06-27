using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text textCoins;

    public TMP_Text lives;
    public TMP_Text livesCost;
    
    public TMP_Text damage;
    public TMP_Text damageCost;
    
    public TMP_Text XP;
    public TMP_Text XPCost;


    private void Awake()
    {

        YandexGame.FullscreenShow();
    }

    private void Start()
    {
        textCoins.text = YandexGame.savesData.coinsSum.ToString();

        lives.text = $"{YandexGame.savesData.lives}->{YandexGame.savesData.lives+1}";
        livesCost.text = $"{YandexGame.savesData.Costlives}";

        damage.text = $"{YandexGame.savesData.damageMod}->{YandexGame.savesData.damageMod+1}";
        damageCost.text = $"{YandexGame.savesData.CostdamageMod}";

        XP.text = $"{YandexGame.savesData.XPmod}->{YandexGame.savesData.XPmod+1}";
        XPCost.text = $"{YandexGame.savesData.CostXPmod}";
    }

    private void Update()
    {
        textCoins.text = YandexGame.savesData.coinsSum.ToString();

        lives.text = $"{YandexGame.savesData.lives}->{YandexGame.savesData.lives + 1}";
        livesCost.text = $"{YandexGame.savesData.Costlives}";

        damage.text = $"{YandexGame.savesData.damageMod}->{YandexGame.savesData.damageMod + 1}";
        damageCost.text = $"{YandexGame.savesData.CostdamageMod}";

        XP.text = $"{YandexGame.savesData.XPmod}->{YandexGame.savesData.XPmod + 1}";
        XPCost.text = $"{YandexGame.savesData.CostXPmod}";
    }
    public void BuyLives()
    {
        if (YandexGame.savesData.coinsSum >= YandexGame.savesData.Costlives)
        {
            YandexGame.savesData.lives++;
            YandexGame.savesData.coinsSum -= YandexGame.savesData.Costlives;
            YandexGame.savesData.Costlives *= 2;
        }
        YandexGame.SaveProgress();
    }

    public void BuyDamage()
    {
        if (YandexGame.savesData.coinsSum >= YandexGame.savesData.CostdamageMod)
        {
            YandexGame.savesData.damageMod++;
            YandexGame.savesData.coinsSum -= YandexGame.savesData.CostdamageMod;
            YandexGame.savesData.CostdamageMod *= 2;
        }
        YandexGame.SaveProgress();
    }

    public void BuyXP()
    {
        if (YandexGame.savesData.coinsSum >= YandexGame.savesData.CostXPmod)
        {
            YandexGame.savesData.XPmod++;
            YandexGame.savesData.coinsSum -= YandexGame.savesData.CostXPmod;
            YandexGame.savesData.CostXPmod *= 2;
        }
        YandexGame.SaveProgress();
    }
}
