using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform MainPOV;
    [SerializeField] private Transform ForestPOV;
    [SerializeField] private Transform TurretPov;
    [SerializeField] private bool isMainPov = true;
    public PlayerMovement playerMovement;
    public float followSpeed = 5f;

    [Space(10)]

    [Header("ScreenShake")]
    public AnimationCurve animationcurve;
    float shakeDuration = 0.5f;

    public Light2D PlayerLight;

    void Start()
    {
        transform.position = MainPOV.position;
    }
    public void Switch()
    {
        if (!isMainPov)
        {
            StopAllCoroutines();
            StartCoroutine(SwitchTo(MainPOV));
            isMainPov = true;
            StartCoroutine(changelight(0.5f));
        }
        else if (isMainPov)
        {
            StopAllCoroutines();
            StartCoroutine(SwitchTo(ForestPOV));
            StartCoroutine(changelight(1.2f));
            isMainPov = false;
        }
    }

    public void ChangeToTurretPOV()
    {
        StopAllCoroutines();
        StartCoroutine(SwitchTo(TurretPov));
        playerMovement.ShouldCameraFollow = false;
    }

    public void ChangeAwayFromTurretPov()
    {
        StopAllCoroutines();
        playerMovement.ShouldCameraFollow = true;
    }

    IEnumerator SwitchTo(Transform MoveToTransform)
    {
        float duration = 2f;
        float time = 0f;
        
        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, MoveToTransform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = MoveToTransform.position;
    }

    private void Update()
    {
        if (playerMovement.ShouldCameraFollow)
        {
            //transform.position = new Vector3(transform.position.x, playerMovement.transform.position.y, -10);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, playerMovement.transform.position.y, followSpeed * Time.deltaTime), -10);
        }
    }

    IEnumerator Shake()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strenght = animationcurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startPos + Random.insideUnitSphere * strenght;
            yield return null;
        }

        transform.position = startPos;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator changelight(float intensity)
    {
        float timer = 0;
        float duration = 3;

        while (timer < duration)
        {
            PlayerLight.intensity = Mathf.Lerp(PlayerLight.intensity, intensity, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }

        PlayerLight.intensity = intensity;

    }
}
