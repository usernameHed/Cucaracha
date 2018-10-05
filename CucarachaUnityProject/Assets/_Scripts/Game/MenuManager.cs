using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class MenuManager : MonoBehaviour, ILevelLocal
{
    public void InitScene()
    {
        Debug.Log("INIT MenuManager !!");
    }
}
