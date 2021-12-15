
using UnityEngine;


//\=================================================================================
//\   When the thought bubble prefab is instantiated into the scene, it will randomly 
//\   choose an item to display and be shadowed to the NPC's transform whilst locking rotation
//\==================================================================================

//reworked this script to work with BubbleSpriteManager.cs so that the list of sprites isnt copied into every character
//Original script by Scott.
public class ThoughtBubbles : MonoBehaviour
{
   Transform Target;
   SpriteRenderer BubbleRenderer;
    SpriteRenderer ChildRenderer;
   private void Awake()
   {
        BubbleRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChildRenderer =  gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Target = Camera.main.gameObject.transform;
   }

    public void Update()
    {
         // Moving the sprites position above the character
        transform.localPosition = BubbleSpriteManager.BubbleOffset;
        //ensure bubble always faces camera 
        gameObject.transform.LookAt(Target,Vector3.up);
    }

    private void SetRendererAndChildRendererEnabled(bool b)
    {
        BubbleRenderer.enabled = b; 
        ChildRenderer.enabled = b;
    }

    public void Hide()
    {
        //hide bubble & child icon 
        SetRendererAndChildRendererEnabled(false);
    }

    public void Show(int i)
    {
        //show requested icon
        if (i >= 0 && i < BubbleSpriteManager.ShopItemSprites.Count) 
        {
             ChildRenderer.sprite = BubbleSpriteManager.ShopItemSprites[i];
             SetRendererAndChildRendererEnabled(true);   
        }
        else
        {
            Debug.Log("Invalid Index Passed for Bubble");
            SetRendererAndChildRendererEnabled(false);
        }
    }
}
