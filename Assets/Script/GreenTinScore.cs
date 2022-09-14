using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreenTinScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _myGreenScoretext;

    private int _myGreenTin;
    private int _myGScore;



    // Start is called before the first frame update
    void Start()
    {
        _myGreenTin = 0;
        _myGScore = 0;
        _myGreenScoretext.text = "0";
    }

    private void OnTriggerEnter2D(Collider2D Ale)
    {
        if (Ale.tag == "GreenTin")
        {
            _myGreenTin += 1;
            Destroy(Ale.gameObject);
            _myGreenScoretext.text = _myGreenTin.ToString();

            Debug.Log("drink");
        }


    }
    private void OnTriggerEnter2D(Collider collider)
    {
        if (collider.attachedRigidbody.tag == "GreenTin")

            _myGScore += 5;
        Destroy(collider.gameObject);
        _myGreenScoretext.text = _myGScore.ToString();

        Debug.Log("drink");
    }

}
