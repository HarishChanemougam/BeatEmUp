using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RedTinScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _myRedScoreText;

    private int _myRedTin;

    // Start is called before the first frame update
    void start()
    {
        _myRedTin = 0;
        _myRedScoreText.text = "0";
    }

    private void OnTriggerEnter2D(Collider2D Mutton)
    {
        if (Mutton.tag == "ResTin")
        {
            _myRedTin += 10;
            Destroy(Mutton.gameObject);
            _myRedScoreText.text = _myRedTin.ToString();

            Debug.Log("red drink");
        }
    }

}
