using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.transform.DOScale(0, .5f);
        StartCoroutine(DestroyObject(.5f));
        OnCollect();
    }

    IEnumerator DestroyObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
    }
    protected virtual void OnCollect()
    {

    }

}
