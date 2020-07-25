using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlast : MonoBehaviour
{
    [SerializeField]
    float secondsToStayActive = 0;

    public static PlayerSpeedModifier SpeedModifier = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (SpeedModifier != null)
            {
                SpeedModifier.OnBoostPlayerSpeed.Invoke();
            }
            StartCoroutine(WaitThenDeactivate());
        }
    }

    IEnumerator WaitThenDeactivate()
    {
        yield return new WaitForSeconds(secondsToStayActive);
        gameObject.SetActive(false);
    }

}
