using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public GameObject waterPrefad;
    Vector3 startPosition;
    public float speed;
    /*
    public Transform transform
    {
        get { return GetComponent<Transform>(); }
    }
    */
    void Start()
    {
        startPosition = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.position.y);
        print(startPosition.y);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.z - startPosition.z) >= 100f)
        {
            print(transform.position.z - startPosition.z);
            GameObject water = Instantiate(waterPrefad, new Vector3(startPosition.x, startPosition.y, startPosition.z), Quaternion.identity);
            water.name = "Water";
            Destroy(gameObject);
        }
    }
}
