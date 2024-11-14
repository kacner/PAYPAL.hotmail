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
    public bool Done = false;
    public cameraScript camerascript;
    private void Start()
    {
        updateHealthBar();
        wintext.enabled = false;
    }

    public void TakeDamage(float Damage)
    {
        if (!Done)
        {

            if ((CurrentHP - Damage) <= 0)
            {
                die();
                camerascript.StartShake();
            }
            else
            {
                if (Damage > 0)
                {
                    Clonk.Play();
                    CurrentHP--;
                    camerascript.StartShake();
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
                Done = true;
            }

            if (CurrentHP < 6)
                DmgState = 3;
            else if (CurrentHP < 13)
                DmgState = 2;
            else
                DmgState = 1;
        }
    }       

    void die()
    {
        submovement.LooseGame();
        Done = true;
    }

    private void Update()
    {
        CurrentHP = Mathf.Clamp(CurrentHP, -1, HP);
    }

    public void updateHealthBar()
    {
        TakeDamage(0);

        if (Input.GetKeyDown(KeyCode.O))
        {
            CurrentHP++;
            TakeDamage(0);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            CurrentHP--;
            TakeDamage(0);
        }
    }
}
