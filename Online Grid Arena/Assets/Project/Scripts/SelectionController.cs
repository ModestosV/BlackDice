using UnityEngine;

public class SelectionController : MonoBehaviour {

    [SerializeField] StatPanel statPanel;
    public GameObject selectionHalo;
    private Character selectedCharacter;

	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject.tag != "Player")
            {
                statPanel.gameObject.SetActive(false);
                selectionHalo.gameObject.SetActive(false);
                return;
            }

            statPanel.gameObject.SetActive(true);
            selectedCharacter = hit.collider.gameObject.GetComponentInParent<Character>();
            statPanel.SetStats(selectedCharacter.controller.Health, selectedCharacter.controller.Damage);
            statPanel.UpdateStatValues();

            selectionHalo.gameObject.SetActive(true);
            selectionHalo.gameObject.transform.SetParent(hit.collider.gameObject.transform.parent.gameObject.transform);
            selectionHalo.gameObject.transform.localPosition = new Vector3(0.0f, -0.99f, -1.0f);
        }
    }
}
