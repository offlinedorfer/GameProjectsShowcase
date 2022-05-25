using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldownBetweenShots = 1f;
    [SerializeField] float burstFireIntervalTime = 0.1f;

    float elapsedTime = 1;

    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        int amountOfShots = 3;
        elapsedTime += Time.deltaTime;
        if (elapsedTime < cooldownBetweenShots) return; 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(BrustFire(amountOfShots));
            elapsedTime = 0;
        }
    }

    IEnumerator BrustFire(int amountOfShots)
    {
        if (amountOfShots <= 0) yield break;
        AudioManager.instance.PlaySound(AudioManager.Sound.PlayerShoot);
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(burstFireIntervalTime);
        amountOfShots--;
        StartCoroutine(BrustFire(amountOfShots));

    }
}
