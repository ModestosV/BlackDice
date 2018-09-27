using UnityEngine;

public class SelectionController : MonoBehaviour {

    [SerializeField] StatPanel statPanel;
    private Character selectedCharacter;

	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
            {
                statPanel.gameObject.SetActive(false);
                return;
            }

            if (hit.collider.gameObject.tag == "Player")
            {
                statPanel.gameObject.SetActive(true);
                selectedCharacter = hit.collider.gameObject.GetComponentInParent<Character>();
                statPanel.SetStats(selectedCharacter.controller.Health, selectedCharacter.controller.Damage);
                statPanel.UpdateStatValues();
            }
        }
    }
}
