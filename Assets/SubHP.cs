using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubHP : MonoBehaviour
{
    public Image Slider;
    public float HP = 20;
    public float CurrentHP = 5;
    public float DmgState = 2;
    // Update is called once per frame
    public AudioSource Clonk;
    public TextMeshProUGUI wintext;

    public SubMovement submovement;
    private void Start()
    {
        updateHealthBar();
        wintext.enabled = false;
    }

    public void TakeDamage(float Damage)
    {
        if ((CurrentHP - Damage) <= 0)
        {
            die();
        }
        else
        {
            if (Damage > 0)
            {
                Clonk.Play();
                CurrentHP--;
            }
        }


        Slider.fillAmount = CurrentHP / HP;

        if (Slider.fillAmount == 0.05f)
        {
            Slider.fillAmount = 0;
            die();
        }

        if (CurrentHP >= HP)
        {
            wintext.enabled = true;
        }

        if (CurrentHP < 6)
            DmgState = 3;
        else if (CurrentHP < 13)
            DmgState = 2;
        else
            DmgState = 1;
    }       

    void die()
    {
        submovement.LooseGame();
    }

    private void Update()
    {
        CurrentHP = Mathf.Clamp(CurrentHP, -1, HP);
    }

    public void updateHealthBar()
    {
        TakeDamage(0);
    }
}
