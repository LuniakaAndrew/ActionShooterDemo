using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool IsDetected { get; set; }
    public bool IsDead { get; set; }

    public void Kill()
    {
        IsDead = true;
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        Vector3 hp = new Vector3(0.1f, 0.1f, 0.1f);
        for (int i = 0; i < 10; i++)
        {
            transform.localScale -= hp;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.SetActive(false);
    }
}