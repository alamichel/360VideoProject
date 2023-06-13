using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInSecond;
    public GameObject button;
    private float timePassed;

    // Start is called before the first frame update
    private bool active;
    void Start()
    {
        this.timePassed = 0.0f;
        this.active = false;

        // if (gameObject.activeInHierarchy)
        // {
        //     this.button.SetActive(active);
        // }

        
    }

    // Update is called once per frame
    void Update()
    {
        this.timePassed += Time.deltaTime;
        
        if (this.timePassed >= this.fadeInSecond)
        {
            Debug.Log("Time passed");
            if (active == false)
            {
                active = true;
            }
            else
            {
                active = false;
            }
            
            // this.button.SetActive(active);
            this.timePassed = 0.0f;

        }


    }
}
