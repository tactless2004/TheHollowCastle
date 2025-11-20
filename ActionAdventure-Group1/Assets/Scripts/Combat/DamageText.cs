/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: DamageText.cs
* DESCRIPTION: UI Elements to indicate damage dealt.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/13 | Leyton McKinney | Init
*
************************************************************/

using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{
    public float lifetime = 5.0f;
    public float moveSpeed = 1.0f;
    public float critScale = 1.5f;
    public AnimationCurve scaleCurve;

    private float timer;
    private TextMeshProUGUI text;
    private Color originalColor;
    private bool crit = false;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
    }

    public void Setup(int damage, Color color, bool crit)
    {
        text.color = color;
        originalColor = color;
        this.crit = crit;

        text.text = damage.ToString();
        if (crit) text.text += "!";
    }

    private void Update()
    {
        timer += Time.deltaTime;

        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        float alpha = Mathf.Lerp(1.0f, 0.0f, timer / lifetime);
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        float scale = scaleCurve.Evaluate(timer / lifetime);

        transform.localScale = crit ? Vector3.one * scale * critScale : Vector3.one * scale;

        if (timer >= lifetime) Destroy(gameObject);
    }
}
