using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomGeneratorAbstract : MonoBehaviour
{
    public RoomDiscoveredEvent OnNewRoomDiscovered = new RoomDiscoveredEvent();
}
