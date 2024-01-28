// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class PointLight : MonoBehaviour
{
    [SerializeField] private Color color;

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public Color GetColor()
    {
        return this.color;
    }
}