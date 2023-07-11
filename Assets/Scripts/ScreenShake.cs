using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public System.Collections.IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 first_pos = transform.localPosition;
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, first_pos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = first_pos;
    }
}
