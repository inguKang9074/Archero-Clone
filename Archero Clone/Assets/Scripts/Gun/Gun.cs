using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private LivingEntity shooter;

    public float fireDistance = 100f; // 무기 사거리
    public float damage;// 데미지

    protected float lastFireTime; // 마지막 발사 시점
    public float timeBetFire = 0.12f; // 연사력

    // Audio 관련 변수
    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    // 이펙트
    // public ParticleSystem muzzleFlashEffect;
    // public ParticleSystem shellEjectEffect;


    public Bullet bullet; // 현재 가진 총알                       
    [SerializeField] private Transform fireTransform; // 총구 위치

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
    }

    public void Setup(LivingEntity holder) // 총 초기화    
    {
        // 총 소유자가 누구인지 설정
        shooter = holder;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    private void OnDisable()
    {

    }

    public bool Fire()
    { 
        // 현재 시간이 마지막 발사시점 + 발사간격을 더한 시간보다 크거나 같으면 발사 가능
        if (Time.time >= lastFireTime + timeBetFire )
        {
            lastFireTime = Time.time; // 발사시간 초기화
            Player.Instance.SetAttackSpeed(timeBetFire);
            // Shot(fireTransform.position, fireDirection);
            GameObject target = Player.Instance.GetTarget();
            Shot(target);
            return true;
        }

        return false;
    }

    private void Shot(GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        if (damageable != null)
        {
            DamageMessage damageMessage;
            damageMessage.damager = Player.Instance.gameObject;
            damageMessage.amount = damage;

            damageable.ApplyDamage(damageMessage);
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 이펙트 재생

        gunAudioPlayer.PlayOneShot(shotClip); // 속도 중첩을 위해 PlayOnShot 메소드 사용

        yield return new WaitForSeconds(0.03f);

    }

    private void Update()
    {

    }
}
