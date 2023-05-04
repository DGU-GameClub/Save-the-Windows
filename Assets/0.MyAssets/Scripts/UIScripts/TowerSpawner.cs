using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject realTower;
    private List<GameObject> towerList;
    void Start()
    {
        towerList = new List<GameObject>();
    }

    public void AddTower(GameObject tower){
        GameObject towerInstance = Instantiate(tower, realTower.transform.position, Quaternion.identity);
        towerInstance.transform.SetParent(realTower.transform);
        towerList.Add(towerInstance);
    }

    //뭘 반환해야하지..?
    public GameObject FindTowerWithSprite(Sprite target)
    {
        foreach (GameObject tower in towerList)
        {
            SpriteRenderer towerSpriteRenderer = tower.GetComponentInChildren<SpriteRenderer>();
            // Sprite towerSprite = tower.GetComponentInChildren<Image>().sprite;
            Debug.Log(towerSpriteRenderer);
            if (towerSpriteRenderer.sprite == target)
            {
                Debug.Log(tower);
                return tower; // 일치하는 스프라이트가 있는 타워 오브젝트를 반환합니다.
            }
        }
        return null; // 일치하는 스프라이트가 없으면 null을 반환합니다.
    }

    public void DestroyRealTower(Sprite target){
        GameObject tower = FindTowerWithSprite(target);
        if (tower)
            Destroy(tower);
    }

}
