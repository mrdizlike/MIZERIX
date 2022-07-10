using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotSound;
    private GameObject currentBullet;
    public Camera FPSCam;
    public Transform AttackPoint;
    public GameObject muzzleFlash;
    public GameObject Crosshair;
    public GameObject DMGText_Text;
    public Text BulletsLeft_Text;
    public Text BulletsInMagazine_Text;

    public Animator GunAnim;

    public AudioSource Weapon_Shot;
    public SoundScript SoundScript;

    public float ShootForce;    
    public float UpwardForce;
    public float TimeBetweenShooting, Spread, ReloadTime, TimeBetweenShots;

    public float MagazineSize, BulletsPerTap;
    public float BulletsLeft, BulletsShot;

    public bool AllowButtonHold;
    public bool AllowInvoke = true;
    public bool blockUsing;
    public bool IsInspect;
    public bool shooting, readyToShoot, reloading;

    private void Awake()
    {
        BulletsLeft = MagazineSize;
        readyToShoot = true;
        BulletsInMagazine_Text.text = MagazineSize.ToString();
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine && !GetComponent<PlayerSTAT>().Dead && !GetComponent<Player_MAIN>().SafeZone)
        {
            MyInput();
        }
    }

    private void MyInput()
    {
        if (AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        BulletsLeft_Text.text = BulletsLeft.ToString();

        if(TimeBetweenShooting <= 0)
        {
            TimeBetweenShooting = 0.1f;
        }

        if(Input.GetKeyDown(KeyCode.N) && !blockUsing)
        {
            GetComponent<PhotonView>().RPC("GunAnimationNetwork", RpcTarget.All, 2);
            IsInspect = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < MagazineSize && !reloading && !blockUsing)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && BulletsLeft <= 0 && !blockUsing)
        {
            Reload();
        }

        //Стрельба
        if(readyToShoot && shooting && !reloading && BulletsLeft > 0 && !blockUsing)
        {
            BulletsShot = 0;
            GetComponent<PhotonView>().RPC("GunAnimationNetwork", RpcTarget.All, 0);
            GetComponent<PhotonView>().RPC("Shoot", RpcTarget.All);
        }

        if(Spread > 0)
        {
            Spread = 0;
        }
    }

    [PunRPC]
    private void Shoot()
    {
        readyToShoot = false;
        IsInspect = false;

        Ray ray = FPSCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;

        //Отдача

        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        currentBullet = Instantiate(bullet, AttackPoint.position, Quaternion.identity);
        Instantiate(shotSound, AttackPoint.position, Quaternion.identity);
        currentBullet.GetComponent<BulletScript>().Photon_Player = GetComponent<PhotonView>();
        currentBullet.GetComponent<BulletScript>().ExplosionDamage = GetComponent<PlayerSTAT>().DMG_Amount;
        foreach (Buff i in GetComponent<PlayerSTAT>().Buffs)
        {
            if(i.BuffType == Buff.BuffID.Poison_Buff)
            {
                currentBullet.GetComponent<BulletScript>().Buff.Add(BuffList.PoisonDeBuff);
            }

            if (i.BuffType == Buff.BuffID.Bleed_Buff)
            {
                currentBullet.GetComponent<BulletScript>().Buff.Add(BuffList.BleedDebuff);
            }

            if (i.BuffType == Buff.BuffID.ElectricCrown_Buff)
            {
                currentBullet.GetComponent<BulletScript>().Buff.Add(BuffList.ElectricCrownBuff);
            }
        }

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * ShootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(FPSCam.transform.up * UpwardForce, ForceMode.Impulse);

        Instantiate(muzzleFlash, AttackPoint.position, Quaternion.identity);

        BulletsLeft--;
        BulletsShot++;

        if (AllowInvoke)
        {
            Invoke("ResetShot", TimeBetweenShooting);
            AllowInvoke = false;
        }

        if(BulletsShot < BulletsPerTap && BulletsLeft > 0)
        {
            Invoke("Shoot", TimeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        AllowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        IsInspect = false;
        GetComponent<PhotonView>().RPC("GunAnimationNetwork", RpcTarget.All, 1);
        Invoke("ReloadFinished", ReloadTime);
    }

    public void ReloadFinished()
    {
        if (!IsInspect)
        {
            BulletsLeft = MagazineSize;
        }
        reloading = false;
    }

    public void DmgTextRelease(float dmg)
    {
        GameObject DmgTEXT = Instantiate(DMGText_Text, Crosshair.transform.position, Quaternion.identity);
        DmgTEXT.transform.SetParent(Crosshair.gameObject.transform);
        DmgTEXT.GetComponent<Text>().text = dmg.ToString();
    }

    [PunRPC]
    private void GunAnimationNetwork(int AnimationNumber)
    {
        if(AnimationNumber == 0)
        {
            GunAnim.Play("Fire");
        }

        if(AnimationNumber == 1)
        {
            GunAnim.Play("Reload");
        }

        if (AnimationNumber == 2)
        {
            GunAnim.Play("Inspection");
        }
    }
}
