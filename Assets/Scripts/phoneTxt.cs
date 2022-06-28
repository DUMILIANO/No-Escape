using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class phoneTxt : MonoBehaviour
{
    public GameObject phoneDropTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(TextOff());
    }

    IEnumerator TextOff()
    {
        yield return new WaitForSeconds(1f);
        phoneDropTxt.SetActive(true);
        yield return new WaitForSeconds(3f);
        phoneDropTxt.SetActive(false);

    }
}
