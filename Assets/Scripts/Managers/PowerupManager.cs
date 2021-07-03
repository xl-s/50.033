using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> powerupIcons;
    
    private AudioSource consumeAudio;
    private List<ConsumableInterface> powerups;

    void Start()
    {
        this.consumeAudio = GetComponent<AudioSource>();
        powerups = new List<ConsumableInterface>();
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
            powerups.Add(null);
        }
    }

    public void addPowerup(Texture texture, int index, ConsumableInterface i)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].GetComponent<RawImage>().texture = texture;
            powerupIcons[index].SetActive(true);
            powerups[index] = i;
        }
    }

    public void removePowerup(int index)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(false);
            powerups[index] = null;
        }
    }

    void cast(int i, GameObject p)
    {
        if (powerups[i] != null)
        {
            powerups[i].consumedBy(p);
            removePowerup(i);
            this.consumeAudio.PlayOneShot(this.consumeAudio.clip);
        }
    }

    public void consumePowerup(KeyCode k, GameObject player)
    {
        switch (k)
        {
            case KeyCode.Z:
                cast(0, player);
                break;
            case KeyCode.X:
                cast(1, player);
                break;
            default:
                break;
        }
    }
}
