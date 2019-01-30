using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball_collision : MonoBehaviour {

    private bool win_flag = false;
    private int pickup_count;
    public Text count_text;
    public Text win_text;

	// Use this for initialization
	void Start () {
        pickup_count = 2;
        win_text.text = "";
        set_pickup_text();
    }
	
	// Update is called once per frame
	void Update () {
		if (win_flag)
        {
            transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
        
        if (transform.position.y < -20)
        {
            win_text.text = "You lose!";
            count_text.text = "";
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "particle_system" && (pickup_count <= 0))
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            win_flag = true;
            win_text.text = "You win!";
            count_text.text = "";
        }

        if (other.gameObject.CompareTag("pick_up"))
        {
            other.gameObject.SetActive(false);
            pickup_count--;
            set_pickup_text();
        }
    }

    void set_pickup_text()
    {
        count_text.text = "Pickups Remaining: " + pickup_count.ToString();

        if (pickup_count <= 0)
        {
            count_text.text = "Done! Proceed to goal";
        }
    }
}
