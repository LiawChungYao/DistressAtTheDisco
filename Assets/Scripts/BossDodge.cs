using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BossDodge : MonoBehaviour
{

    public float dodgeOffset = -2f;
    public float dodgeSpeed = 2f;

    private CapsuleCollider capsuleCollider;
    private Vector3 originalCentre;
    public Transform spriteTransform;
    private bool isDodging = false;

    private void Start() {

        Debug.Log("Start dodge called");
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalCentre = capsuleCollider.center;

    }   

    public void Dodge() {
        Debug.Log("Outside dodge");
        if (!isDodging) {
            Debug.Log("Inside dodge");
            StartCoroutine(DodgeRoutine());
        }
    } 

    private IEnumerator DodgeRoutine() {
        isDodging = true;

        while (capsuleCollider.center.y > originalCentre.y + dodgeOffset) {
            capsuleCollider.center -= new Vector3(0, dodgeSpeed * Time.deltaTime, 0);
            Vector3 spriteNewPosition = capsuleCollider.transform.position;
            spriteNewPosition.y += capsuleCollider.center.y; 
            spriteTransform.position = spriteNewPosition;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (capsuleCollider.center.y < originalCentre.y) {
            capsuleCollider.center += new Vector3(0, dodgeSpeed * Time.deltaTime, 0);
            Vector3 spriteNewPosition = capsuleCollider.transform.position;
            spriteNewPosition.y += capsuleCollider.center.y; 
            spriteTransform.position = spriteNewPosition;
            yield return null;
        }

        isDodging = false;
    }
}
