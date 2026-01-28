using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] Transform counterToPoint;
    [SerializeField] Transform plateVisualPrefab;

    List<GameObject> plateVisualGameObjectList = new List<GameObject>();

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        var plateVisualTrnasform = Instantiate(plateVisualPrefab, counterToPoint);

        float plateOffset = .1f;
        plateVisualTrnasform.localPosition = new Vector3(0, plateOffset * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTrnasform.gameObject);
    }


    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        var plate = plateVisualGameObjectList.Last();
        plateVisualGameObjectList.Remove(plate);
        Destroy(plate);
    }
}
