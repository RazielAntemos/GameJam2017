using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartbutton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }




    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                {
                    if (hit.transform.name == "RestartButton")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


                    }

                }
            }
        }
    }
}
