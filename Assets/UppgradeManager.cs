using TMPro;
using UnityEngine;

public class UppgradeManager : MonoBehaviour
{
    public int MaxUppgradeAmount = 9; 
    public int Money = 0;

    [Space(40)]

    [SerializeField] public int HealingUpgradeAmount = 0;

    [SerializeField] private int HealingWantToBuyint = 0;

    public TextMeshProUGUI HealingBought;

    [SerializeField] private int HealingCost = 5;

    [Space(40)]

    [SerializeField] public int DamageUpgradeAmount = 0;

    [SerializeField] private int DamageWantToBuyint = 0;

    public TextMeshProUGUI DamageBought;

    [SerializeField] private int DamageCost = 5;


    [Space(40)]

    [SerializeField] public int QuickUpgradeAmount = 0;

    [SerializeField] private int QuickWantToBuyint = 0;

    public TextMeshProUGUI QuickBought;

    [SerializeField] private int QuickCost = 5;

    [Space(40)]

    [SerializeField] public int WalkUpgradeAmount = 0;

    [SerializeField] private int WalkWantToBuyint = 0;

    public TextMeshProUGUI WalkBought;

    [SerializeField] private int WalkCost = 5;

    [Space(40)]

    [SerializeField] public int MineUpgradeAmount = 0;

    [SerializeField] private int MineWantToBuyint = 0;

    public TextMeshProUGUI MineBought;

    [SerializeField] private int MineCost = 5;

    [Space(40)]

    private int FinalCost = 0;
    public TextMeshProUGUI FinalCostText;
    public SubHP subhp;


    private void Start()
    {
        UpdateCost();
    }

    void UpdateCost()
    {
        if (HealingWantToBuyint > 0)
        {
            HealingBought.text = HealingUpgradeAmount.ToString() + "+" + HealingWantToBuyint.ToString();
        }
        else
        {
            HealingBought.text = HealingUpgradeAmount.ToString();
        }


        
        if (DamageWantToBuyint > 0)
        {
            DamageBought.text = DamageUpgradeAmount.ToString() + "+" + DamageWantToBuyint.ToString();
        }
        else
        {
            DamageBought.text = DamageUpgradeAmount.ToString();
        }
        

        
        if (QuickWantToBuyint > 0)
        {
            QuickBought.text = QuickUpgradeAmount.ToString() + "+" + QuickWantToBuyint.ToString();
        }
        else
        {
            QuickBought.text = QuickUpgradeAmount.ToString();
        }
        
        
        if (WalkWantToBuyint > 0)
        {
            WalkBought.text = WalkUpgradeAmount.ToString() + "+" + WalkWantToBuyint.ToString();
        }
        else
        {
            WalkBought.text = WalkUpgradeAmount.ToString();
        }
        
        if (MineWantToBuyint > 0)
        {
            MineBought.text = MineUpgradeAmount.ToString() + "+" + MineWantToBuyint.ToString();
        }
        else
        {
            MineBought.text = MineUpgradeAmount.ToString();
        }


        FinalCost = (HealingWantToBuyint * HealingCost) + (DamageWantToBuyint * DamageCost) + (QuickWantToBuyint * QuickCost) + (WalkWantToBuyint * WalkCost) + (MineWantToBuyint * MineCost);

        FinalCostText.text = "£" + FinalCost.ToString();

    }

    public void AddHealing()
    {
        if (HealingUpgradeAmount + HealingWantToBuyint < MaxUppgradeAmount)
        {
            HealingWantToBuyint++;
        }

        UpdateCost();
        print("Add");
    }

    public void AddDamage()
    {
        if (DamageUpgradeAmount + DamageWantToBuyint < MaxUppgradeAmount)
        {
            DamageWantToBuyint++;
        }

        UpdateCost();
        print("Add");
    }
    
    public void AddQuick()
    {
        if (QuickUpgradeAmount + QuickWantToBuyint < MaxUppgradeAmount)
        {
            QuickWantToBuyint++;
        }

        UpdateCost();
        print("Add");
    }

    public void AddWalk()
    {

        if (WalkUpgradeAmount + WalkWantToBuyint < MaxUppgradeAmount)
        {
            WalkWantToBuyint++;
        }
        UpdateCost();
        print("Add");
    }

    public void AddMine()
    {
        if (MineUpgradeAmount + MineWantToBuyint < MaxUppgradeAmount)
        {
            MineWantToBuyint++;
        }
        UpdateCost();
        print("Add");
    }
    public void RemoveHealing()
    {
        HealingWantToBuyint--;
        HealingWantToBuyint = Mathf.Clamp(HealingWantToBuyint, 0, MaxUppgradeAmount);

        UpdateCost();
        print("Remove");
    }
    public void RemoveDamage()
    {
        DamageWantToBuyint--;
        DamageWantToBuyint = Mathf.Clamp(DamageWantToBuyint, 0, MaxUppgradeAmount);

        UpdateCost();
        print("Remove");
    }
    public void RemoveQuick()
    {
        QuickWantToBuyint--;
        QuickWantToBuyint = Mathf.Clamp(QuickWantToBuyint, 0, MaxUppgradeAmount);


        UpdateCost();
        print("Remove");
    }
    public void RemoveWalk()
    {
        WalkWantToBuyint--;
        WalkWantToBuyint = Mathf.Clamp(WalkWantToBuyint, 0, MaxUppgradeAmount);


        UpdateCost();
        print("Remove");
    }

    public void RemoveMine()
    {
        MineWantToBuyint--;
        MineWantToBuyint = Mathf.Clamp(MineWantToBuyint, 0, MaxUppgradeAmount);


        UpdateCost();
        print("Remove");
    }

    public void Conform()
    {

        if (FinalCost <= Money)
        {
            HealingUpgradeAmount = HealingWantToBuyint + HealingUpgradeAmount;
            HealingUpgradeAmount = Mathf.Clamp(HealingUpgradeAmount, 0, MaxUppgradeAmount);

            subhp.CurrentHP += 5f;
            subhp.updateHealthBar();
            
            
            DamageUpgradeAmount = DamageWantToBuyint + DamageUpgradeAmount;
            DamageUpgradeAmount = Mathf.Clamp(DamageUpgradeAmount, 0, MaxUppgradeAmount);


            QuickUpgradeAmount = QuickWantToBuyint + QuickUpgradeAmount;
            QuickUpgradeAmount = Mathf.Clamp(QuickUpgradeAmount, 0, MaxUppgradeAmount);


            WalkUpgradeAmount = WalkWantToBuyint + WalkUpgradeAmount;
            WalkUpgradeAmount = Mathf.Clamp(WalkUpgradeAmount, 0, MaxUppgradeAmount);
            
            
            MineUpgradeAmount = MineWantToBuyint + MineUpgradeAmount;
            MineUpgradeAmount = Mathf.Clamp(MineUpgradeAmount, 0, MaxUppgradeAmount);


            HealingWantToBuyint = 0;
            DamageWantToBuyint = 0;
            QuickWantToBuyint = 0;
            WalkWantToBuyint = 0;
            MineWantToBuyint = 0;

            Money -= FinalCost;
        }

        UpdateCost();
    }
}
