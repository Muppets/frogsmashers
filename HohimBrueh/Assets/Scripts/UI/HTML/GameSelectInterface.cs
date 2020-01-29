using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUI;
using FreeLives;

public class GameSelectInterface : MonoBehaviour
{
    private WorldUIHelper worldHelper;
    private HtmlDocument document;
    // Start is called before the first frame update
    void Start()
    {
        worldHelper = this.GetComponent<WorldUIHelper>();
          document = worldHelper.document;
    }

    public void sendInput(InputState input) {
       
        var jsInput = inputStateToJS(input);
        if(jsInput != -1) {
           var obj = document.Run("recieveInput",new []{jsInput});
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private int inputStateToJS(InputState input) {

        if(input.left) {
            return 0;
        } else if(input.right) {
            return 1;
        } else if(input.up) {
            return 2;
        } else if(input.down) {
            return 3;
        } else if (input.start) {
            return 4;
        } else {
            return 1;
        }
    }
}
