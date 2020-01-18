using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    ScoreController ScM;
    // Start is called before the first frame update
    void Start()
    {
        ScM = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }

        // Ensures assets are removed from memory once no longer required
    void CheckBounds()
    {
        if (transform.position.x <= -5 || transform.position.x >= 5) 
        {
            if (gameObject.tag == "S_Green") 
            {
                ScM.comboCounter = 0;
                ScM.comboBonus = 1;
            }
            Destroy(gameObject);
        }
    }
}
