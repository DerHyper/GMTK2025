using UnityEngine;

public class SeightGO : MonoBehaviour
{
    public float speed;
    public float deleteHeight = -30;


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);

        if (gameObject.transform.position.y <= deleteHeight)
        {
            Destroy(gameObject);
        }
    }
}
