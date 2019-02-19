/*
 * Defines default behaviour methods each unit must implement and perform
 */

using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour
{
    public abstract void Move();
    public abstract void Scale();
    public abstract void Rotate();
    public abstract void OnSelect();
    public abstract void OnDeselect();
}
