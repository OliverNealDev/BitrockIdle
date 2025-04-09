using UnityEngine;
using System.Collections;

public class mineableRockController : MonoBehaviour
{
    [SerializeField] private float originalScale = 1;
    [SerializeField] private float hoverScale = 2;
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    [SerializeField] private float updateInterval = 0.05f;

    [SerializeField] private float maxHP = 10;
    [SerializeField] private float HP = 10;

    [SerializeField] private float miningStrength = 1;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    void Start()
    {
        originalPosition = transform.localPosition;
        transform.localScale = new Vector2(originalScale, originalScale);
    }

    void OnMouseOver()
    {
        transform.localScale = new Vector2(hoverScale, hoverScale);
        if (Input.GetMouseButtonDown(0))
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                transform.localPosition = originalPosition;
            }
            shakeCoroutine = StartCoroutine(Shake());

            HitRock();
        }
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector2(originalScale, originalScale);
    }

    void HitRock()
    {
        HP -= miningStrength;
        if (HP <= 0)
        {
            onRockMined();
        }
    }

    void onRockMined()
    {
        gameManager.Instance.PickAndAddOre();
        HP = maxHP;
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;
        Vector3 currentOffset = Vector3.zero;
        while (elapsed < shakeDuration)
        {
            Vector3 targetOffset = Random.insideUnitSphere * shakeMagnitude;
            targetOffset.z = 0f;
            float localTimer = 0f;
            while (localTimer < updateInterval && elapsed < shakeDuration)
            {
                float dt = Time.deltaTime;
                localTimer += dt;
                elapsed += dt;
                float t = localTimer / updateInterval;
                transform.localPosition = originalPosition + Vector3.Lerp(currentOffset, targetOffset, t);
                yield return null;
            }
            currentOffset = targetOffset;
        }
        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}