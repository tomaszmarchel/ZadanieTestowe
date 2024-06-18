using UnityEngine;

public class ClearCounter : MonoBehaviour, IArmoryObjectParent
{
    [SerializeField] ArmoryScriptableObject armoryObjectSO;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] ClearCounter secondClearCounter;

    private ArmoryObject armoryObject;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (armoryObjectSO != null)
            {
                armoryObject.SetArmoryObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(Player player)
    {
        if (armoryObject == null)
        {
            Transform armoryObjectTransform = Instantiate(armoryObjectSO.prefab, counterTopPoint);
            armoryObjectTransform.GetComponent<ArmoryObject>().SetArmoryObjectParent(this);
        }
        else
        {
            armoryObject.SetArmoryObjectParent(player);
        }
    }

    public Transform GetCounterTopPoint()
    {
        return counterTopPoint; 
    }

    public void SetArmoryObject(ArmoryObject armoryObject)
    {
        this.armoryObject = armoryObject;
    }

    public ArmoryObject GetArmoryObject()
    {
        return armoryObject;
    }

    public void ClearArmoryObject()
    {
        this.armoryObject = null;
    }

    public bool HasArmoryObject()
    {
        return this.armoryObject != null;
    }
}
