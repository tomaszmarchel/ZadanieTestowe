using UnityEngine;

public class ArmoryObject : MonoBehaviour
{
    [SerializeField] ArmoryScriptableObject armoryScriptableObject;

    private IArmoryObjectParent armoryObjectParent;

    public ArmoryScriptableObject GetArmoryObjectSO()
    {
        return armoryScriptableObject; 
    }

    public void SetArmoryObjectParent(IArmoryObjectParent armoryObjectParent)
    {
        if (this.armoryObjectParent != null)
        {
            this.armoryObjectParent.ClearArmoryObject();
        }

        this.armoryObjectParent = armoryObjectParent;

        if (armoryObjectParent.HasArmoryObject())
            Debug.LogError("Counter already has an object");

        armoryObjectParent.SetArmoryObject(this);

        transform.parent = armoryObjectParent.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }

    public IArmoryObjectParent GetArmoryObjectParent() 
    {
        return this.armoryObjectParent;
    }

}
