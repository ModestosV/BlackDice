using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] public StatPanel statPanel;
    public GameObject selectionHalo;
    private Character selectedCharacter;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    if (hit.collider.gameObject.transform.parent.tag != "Active_Character")
                    {
                        SelectCharacter(hit);
                    }
                }
                else if (hit.collider.tag == "Ground" && GameObject.FindGameObjectsWithTag("Active_Character").Length != 0)
                {
                    GameObject active = GameObject.FindGameObjectsWithTag("Active_Character")[0];
                    Vector3 newPos = new Vector3(hit.point.x, active.transform.position.y, hit.point.z);
                    active.transform.position = newPos;
                }
            }


        }
        if (Input.GetMouseButtonDown(1) && GameObject.FindGameObjectsWithTag("Active_Character").Length != 0)
        {
            statPanel.gameObject.SetActive(false);
            selectionHalo.gameObject.SetActive(false);

            var deselectedCharacter = GameObject.FindGameObjectsWithTag("Active_Character")[0];
            deselectedCharacter.tag = "Player";

            Debug.Log(string.Format("Deselected {0}", deselectedCharacter.GetComponentInParent<Character>()));
        }
    }
    
    private void SelectCharacter(RaycastHit hit)
    {
        foreach (GameObject focus in GameObject.FindGameObjectsWithTag("Active_Character"))
        {
            focus.tag = "Player";
        }
        hit.collider.gameObject.transform.parent.tag = "Active_Character";
        statPanel.gameObject.SetActive(true);
        selectedCharacter = hit.collider.gameObject.GetComponentInParent<Character>();
        statPanel.SetStats(selectedCharacter.controller.health, selectedCharacter.controller.damage);
        statPanel.UpdateStatValues();

        selectionHalo.gameObject.SetActive(true);
        selectionHalo.gameObject.transform.SetParent(hit.collider.gameObject.transform.parent.gameObject.transform);
        selectionHalo.gameObject.transform.localPosition = new Vector3(0.0f, -0.99f, -1.0f);

        Debug.Log(string.Format("Selected {0}", selectedCharacter));
    }
}
