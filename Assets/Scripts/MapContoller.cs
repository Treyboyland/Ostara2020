using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapContoller : MonoBehaviour
{
    [SerializeField]
    RoomGeneratorAbstract generator = null;

    [SerializeField]
    Player player = null;

    [SerializeField]
    GameObject map = null;

    [SerializeField]
    TextMeshProUGUI textBox = null;

    [SerializeField]
    GameTimer timer = null;

    bool canShow = true;

    // Start is called before the first frame update
    void Start()
    {
        player.OnPlayerWinsGame.AddListener(() => canShow = false);
        timer.OnEndTimeReached.AddListener(() => canShow = false);
        generator.OnNewRoomDiscovered.AddListener((grid, discovered) =>
        {
            textBox.text = /*grid.AsMap(discovered);*/ grid.AsMap();
        });
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShow)
        {
            if (Input.GetButtonDown("Map"))
            {
                map.SetActive(!map.activeInHierarchy);
            }
        }
        else
        {
            map.SetActive(false);
        }
    }
}
