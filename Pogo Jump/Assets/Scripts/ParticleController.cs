using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private int i = 0;

    public GameObject particle;
    public ParticleSystem particleSystem1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Ground "+ i);
            i++;
            particleSystem1.Play();
            //particleSystem1.GetComponent<ParticleSystem>().Play();
            //particle.SetActive(true);

        }
        else
        {
            particleSystem1.Stop();
            //particleSystem1.GetComponent<ParticleSystem>().Pause();
            //particle.SetActive(false);
        }
    }
}

