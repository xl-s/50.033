using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}

public class PowerupManagerEV : MonoBehaviour
{
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

    public AudioSource powerupAudio;
    public AudioSource powerdownAudio;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
            GameManager.OnPlayerDeath += ResetValues;
        }
        else
        {
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }
    
    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    public void CastPowerup(KeyCode k)
    {
        if (k == KeyCode.Z)
        {
            CastSlot(0);
        }
        if (k == KeyCode.X)
        {
            CastSlot(1);
        }
    }

    void CastSlot(int index)
    {
        Powerup powerup = powerupInventory.Get(index);
        if (powerup != null)
        {
            powerupAudio.PlayOneShot(powerupAudio.clip);
            powerupIcons[index].SetActive(false);
            powerupInventory.Remove(index);
            marioJumpSpeed.ApplyChange(powerup.absoluteJumpBooster);
            marioMaxSpeed.ApplyChange(powerup.absoluteSpeedBooster);
            StartCoroutine(RemoveEffect(powerup));
        }
    }

    IEnumerator RemoveEffect(Powerup powerup)
    {
        yield return new WaitForSeconds((float)powerup.duration);
        marioJumpSpeed.ApplyChange(-powerup.absoluteJumpBooster);
        marioMaxSpeed.ApplyChange(-powerup.absoluteSpeedBooster);
        powerdownAudio.PlayOneShot(powerdownAudio.clip);
    }

    public void ResetValues()
    {
        powerupInventory.Clear();
        resetPowerup();
    }

    void OnApplicationQuit()
    {
        ResetValues();
    }
}
