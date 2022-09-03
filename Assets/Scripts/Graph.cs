using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    [SerializeField]
	Transform pointPrefab;

	[SerializeField, Range(1,100)]
	int resolution = 10;

    Transform[] points;

    // Awake is called when the component is awoken
    void Awake() {
        populateArray(resolution);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    void destroyArray() {
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Destroy(point.gameObject);
        }
    }

    void populateArray(int length) {
        float step = 2f / length;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;
        points = new Transform[length];
        for (int i = 0; i < length; i++) {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
            point.SetParent(transform, false);
			point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
    }

    // Update is called once per frame
    void Update() {
        float time = Time.time;
        if (points.Length != resolution) {
            destroyArray();
            populateArray(resolution);
        }
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;
        }
    }
}
