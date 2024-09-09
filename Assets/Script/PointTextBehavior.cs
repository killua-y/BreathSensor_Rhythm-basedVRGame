using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointTextBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI pointText;
    private int score;

    void Start()
    {
        pointText = this.GetComponent<TextMeshProUGUI>();
        score = 0;

    }

    public void OnBulletHit(int hardness)
    {
        score += hardness;
        pointText.text = "Score:" + score;
    }
}
