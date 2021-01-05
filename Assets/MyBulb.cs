using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚舉，千萬注意我的位置!!!!!
public enum STATE
{
	ON,     // 0
	OFF,    // 1
	BROKEN  // 2
}

public class MyBulb : MonoBehaviour
{
	public STATE state;
	public Material onMat;
	public Material offMat;
	public Material brokenMat;

	[SerializeField]
	private int hp = 3;
	private MeshRenderer render;
	private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (state == STATE.ON)
		{
			render.material = onMat;

			if (isClicked)
			{
				state = STATE.OFF;
			}
		}

		else if (state == STATE.OFF)
		{
			render.material = offMat;

			if (isClicked || Input.GetKeyDown("k"))
			{
				if (hp <= 0)
				{
					state = STATE.BROKEN;
					Boom();
				}
				else
				{
					hp = hp - 1;
					state = STATE.ON;
				}
			}

		}

		else if (state == STATE.BROKEN)
		{
			render.material = brokenMat;

			if (Input.GetKeyDown("r"))
			{
				hp = 3;
				state = STATE.OFF;
			}
		}

		isClicked = false;
	}

	public void OnClicked() {

		isClicked = true;

	}

	public void OnBoom() {
		//state = STATE.BROKEN;
		if (hp <= 0)
		{
			state = STATE.BROKEN;
			Boom();
		}
		else
		{
			hp = hp - 1;
			state = STATE.ON;
		}
	}


	private void Boom() {
		Collider[] cols = new Collider[50];
		if (Physics.OverlapSphereNonAlloc(transform.position, 100, cols)>0)
		{
			foreach (Collider col in cols)
			{
				col.SendMessage("OnBoom", SendMessageOptions.DontRequireReceiver);
				print("boom");
			}
		}
	}

}
