using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class AF_Skill : MonoBehaviour
{
    protected Player_MAIN _PM;
    protected PlayerSTAT _PS;

    protected GameObject _SkillLock_UI;
    protected GameObject _SkillCoolDown_UI;
    protected GameObject _SkillUpgradeButton_UI;
    protected GameObject[] _SkillLVL_UI;
    protected GameObject _SkillEffectUI;
    protected GameObject _SkillEffect;

    protected AudioClip _SkillAudio;

    protected bool _SkillActive;
    protected int _SkillLevel;
    protected float _CooldownTime;
    protected float _SomeValue;

    protected KeyCode _Key;

    public void SkillUpgrade()
    {
        if (_PS.UpgradeTokenCount == 0)
        {
            _SkillUpgradeButton_UI.SetActive(false);
        }

        if (!Input.GetKey(KeyCode.LeftControl) && !_PS.Debuffs.Contains(BuffList.MirrorBuff))
        {
            _PM.BlockUse = false;
        }

        if (_PS.UpgradeTokenCount > 0)
        {
            if (_SkillLevel != 3)
            {
                _SkillUpgradeButton_UI.SetActive(true);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _PM.BlockUse = true;
                if (Input.GetKeyDown(_Key) && _SkillLevel != 3)
                {
                    _SkillLevel += 1;
                    _SkillCoolDown_UI.GetComponent<Image>().fillAmount = 0;
                    _SkillLVL_UI[_SkillLevel - 1].SetActive(true);
                    _SkillLock_UI.GetComponent<Animator>().Play("LockOut");
                    _SkillUpgradeButton_UI.SetActive(false);
                    _SkillActive = true;

                    _PS.UpgradeTokenCount--;
                }
            }
        }
    }

    protected virtual void CoolDownSys()
    {
        if (!_SkillActive && _SkillLevel > 0)
        {
            _SkillCoolDown_UI.SetActive(true);
            _SkillCoolDown_UI.GetComponent<Image>().fillAmount -= 1 / _CooldownTime * Time.deltaTime;
            if (_SkillCoolDown_UI.GetComponent<Image>().fillAmount <= 0)
            {
                _SkillCoolDown_UI.GetComponent<Image>().fillAmount = 1;
                _SkillCoolDown_UI.SetActive(false);
                _SkillActive = true;
            }
        }
    }

    public abstract void SkillSys();

}
