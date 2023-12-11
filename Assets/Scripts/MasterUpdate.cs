using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUpdate : MonoBehaviour
{
    [SerializeField]
    private StarterAssets.FirstPersonController firstPersonController;

    public List<Enemy> enemyList;


    // Start is called before the first frame update
    void Start()
    {
        if(firstPersonController == null)
        {
            Debug.Log("FPS Controller is empty");
        }
    }

    // Update is called once per frame
    void Update()
    {
        firstPersonController.PlayerUpdate();
        for(int enemy = 0; enemy < enemyList.Count; enemy++)
        {
            enemyList[enemy].EnemyUpdate();
        }
    }
}
