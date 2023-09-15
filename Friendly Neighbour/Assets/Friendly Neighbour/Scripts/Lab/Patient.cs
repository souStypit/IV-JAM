using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] private float speed;

    private Bed bed;
    private List<Transform> path;

    private void Start()
    {
        bed = BunksManager.instance.GetFreeBed(this);
        path = PathCreator.instance.GetPath(bed.bedIndex);

        StartCoroutine(GoToBed());
    }

    private IEnumerator GoToBed()
    {
        transform.position = path[0].position;
        for (int i = 1; i < path.Count; i++)
        {
            while (transform.position != path[i].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].position, speed * Time.deltaTime);
                yield return null;
            }
        }
        LieOnBed();
    }

    private void LieOnBed()
    {
        bed.SetInteractionTrigger();
    }

    public IEnumerator GoToExit()
    {
        for (int i = path.Count - 1; i >= 0; i--)
        {
            while (transform.position != path[i].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].position, speed * Time.deltaTime);
                yield return null;
            }
        }
        Destroy(gameObject);
    }
}
