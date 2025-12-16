using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Movement _bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] internal GameObject shootEffect;
    [SerializeField] internal RandomSound shootSound;

    [SerializeField] private float _shootCooldown = 1.25f;
    internal float shootTime = 0.15f;

    [SerializeField] internal bool isShooting;
    internal bool canShoot = true;

    private void Update()
    {
        if(Pause.Instance.isPaused) return;
        if(isShooting && canShoot) Shot(); 
        shootTime -= Time.deltaTime;
    }

    private void Shot()
    {
        if(shootTime <= 0)
        {
            shootTime = _shootCooldown;
            InstantiateBullet();
            if(shootSound != null && shootEffect != null) ShootVFX();
        }
    }

    private void ShootVFX()
    {
        shootSound.RandomizePitch(0.9f, 1.2f);
        shootSound.Play();
        Destroy(Instantiate(shootEffect, _bulletSpawn.position, Quaternion.identity, transform), 1f);
    }

    private void InstantiateBullet()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetOwnerLayer(gameObject.layer);
        bullet.Init(_bulletSpeed, transform.up);
    }

    public void ShootState(bool state) => isShooting = state;
}
