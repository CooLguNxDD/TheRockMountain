using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class PlayerHurtVisual : MonoBehaviour
{
    // Start is called before the first frame update
    public UnitStates unitStates;
    public Color originalColor;
    
    void Start()
    {
        unitStates.unitHurt += performHurtVisual;
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame

    public void performHurtVisual(object sender, EventArgs args){
        StartCoroutine(hurtAnimation());
    }

    IEnumerator hurtAnimation(){
        SpriteRenderer objectColor = gameObject.GetComponent<SpriteRenderer>();

        objectColor.DOColor(Color.red, 0.1f);
        yield return new WaitForSeconds(0.1f);
        objectColor.DOColor(originalColor, 0.1f);
        yield return new WaitForSeconds(0.1f);

        objectColor.DOColor(Color.red, 0.1f);
        yield return new WaitForSeconds(0.1f);
        objectColor.DOColor(originalColor, 0.1f);

        yield return null;
        
    }
    void Update()
    {
        
    }
}
