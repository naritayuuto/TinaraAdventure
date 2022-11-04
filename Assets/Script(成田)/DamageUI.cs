using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    float fadeOutSpeed = 1f;
    float moveTime = 0.5f;
    TextMeshProUGUI text = null;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void LateUpdate()
    {
        transform.position += Vector3.up * moveTime * Time.deltaTime;
        text.color = Color.Lerp(text.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);
        if(text.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
