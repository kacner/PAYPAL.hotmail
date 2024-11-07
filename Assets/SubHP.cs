using UnityEngine;
using UnityEngine.UI;

public class SubHP : MonoBehaviour
{
    public Image Slider;
    [SerializeField] private float HP = 20;
    [SerializeField] public float CurrentHP = 5;
    public float DmgState = 2;
    // Update is called once per frame

    private void Start()
    {
        updateHealthBar();
    }

    public void TakeDamage(float Damage)
    {
        if ((CurrentHP - Damage) <= 0)
        {
            die();
        }
        else
        {
            CurrentHP--;
        }


        Slider.fillAmount = CurrentHP / HP;

        if (Slider.fillAmount == 0.05f)
        {
            Slider.fillAmount = 0;
            die();
        }

        if (CurrentHP >= HP)
        {
            print("youwin");
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
        print("SUB DIDES");
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
