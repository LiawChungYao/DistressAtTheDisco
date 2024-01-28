using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 

public class GunImageSwitching : MonoBehaviour
{
    public Image weaponImage; 
    public WeaponSwitching weaponSwitchingScript; 

    // A dictionary to hold the weapon index and corresponding Gun images
    public Dictionary<int, Sprite> weaponSprites = new Dictionary<int, Sprite>();

    public Sprite akmSprite; 
    public Sprite m47Sprite; 

    void Start()
    {
        weaponSprites.Add(0, akmSprite);
        weaponSprites.Add(1, m47Sprite);

        // Set the weapon image
        UpdateWeaponImage(weaponSwitchingScript.selectedWeapon);
    }

    void Update()
    {
        UpdateWeaponImage(weaponSwitchingScript.selectedWeapon);
    }

    void UpdateWeaponImage(int selectedWeapon)
    {
        if (weaponSprites.ContainsKey(selectedWeapon))
        {
            weaponImage.sprite = weaponSprites[selectedWeapon];
        }
    }
}
