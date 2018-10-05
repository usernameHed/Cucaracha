using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class MenuManager : MonoBehaviour, ILevelLocal
{
    public void InitScene()
    {
        Debug.Log("INIT MenuManager !!");
    }

    public void Play()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    public void Quit()
    {
        GameManager.Instance.SceneManagerLocal.Quit();
    }
}
