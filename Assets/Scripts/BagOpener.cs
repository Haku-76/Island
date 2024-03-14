using UnityEngine;

public class BagOpener : MonoBehaviour
{
    public GameObject myBag;
    public bool isBagOpen;

    void Start()
    {
        
    }

    
    void Update()
    {
        OpenMyBag();
    }

    private void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBagOpen = !isBagOpen;
            myBag.SetActive(isBagOpen);
        }
    }
}
