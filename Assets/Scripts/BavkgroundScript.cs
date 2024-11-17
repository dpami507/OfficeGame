using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BavkgroundScript : MonoBehaviour
{
    public GameObject backgroundPrefab;

    public Camera backCamera;

    public Sprite[] backgroundSprites;

    public int delay;

    public int speed;

    public Transform TopCollider;
    public Transform BottomCollider;
    public Transform LeftCollider;
    public Transform RightCollider;

    private void Start()
    {
        StartCoroutine(SpawnPrefab());
        CamBounds();
    }

    void CamBounds()
    {
        float vertExtent = backCamera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        TopCollider.transform.position = new Vector2 (0, vertExtent);
        TopCollider.transform.localScale = new Vector2(horzExtent * 2, .1f);

        BottomCollider.transform.position = new Vector2 (0, -vertExtent);
        BottomCollider.transform.localScale = new Vector2(horzExtent * 2, .1f);

        LeftCollider.transform.position = new Vector2 (-horzExtent, 0);
        LeftCollider.transform.localScale = new Vector2(.1f, vertExtent * 2);

        RightCollider.transform.position = new Vector2 (horzExtent, 0);
        RightCollider.transform.localScale = new Vector2(.1f, vertExtent * 2);
    }

    IEnumerator SpawnPrefab()
    {
        yield return new WaitForSeconds(delay);

        GameObject prefab = Instantiate(backgroundPrefab, transform.position, Quaternion.identity);

        prefab.GetComponent<SpriteRenderer>().sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        prefab.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));

        prefab.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-speed, speed) * 5);

        StartCoroutine(SpawnPrefab());
    }
}
