using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] Transform swordPrefab;
    [SerializeField] Transform counterTopPoint;

    public void Interact()
    {
        Transform swordTransfornm = Instantiate(swordPrefab, counterTopPoint);
    }
}
