using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBehaviour : MonoBehaviour 
{

    protected enum CharacterState
    {
        Moving,
        Idle, 
        Selected
    };
    private CharacterState state;

    private Map map;
    private Tile[] movementRange;
    Color movementHighlight = new Color(0, 1f, 1f, .3f);

    private List<Path> possiblePaths;
    private Path currentPath; //Current path for move state

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float currentLerpTime = 0;

    int posX, posY;
	// Use this for initialization
	void Start () 
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        posX = 3;
        posY = 3;
        move(posX, posY);
        movementRange = map.getRangeTiles(3, 3, 3);

        possiblePaths = new List<Path>();

        state = CharacterState.Selected;
        currentPath = null; 

        /*Path testPath = (Path)ScriptableObject.CreateInstance(typeof(Path));
        testPath.addStep(Path.UP, 2);
        testPath.addStep(Path.LEFT, 1);*/
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	    switch(state)
        {
            case CharacterState.Selected: serviceSelectedState(); break;
            case CharacterState.Moving: serviceMoveState(); break;
            case CharacterState.Idle: break;
        }
	}

    private void serviceSelectedState()
    {
        if (map.selectedTile != null)
        {
            for (int i = 0; i < movementRange.Length; i++)
            {
                if (movementRange[i] == map.selectedTile)
                {
                    map.clearHighlights(movementRange);
                    //move(map.selectedTile.x, map.selectedTile.y);
                    movementRange = map.getRangeTiles(posX, posY, 3);
                    map.highlightTiles(movementRange, movementHighlight);
                    currentPath = buildPathToTile(map.selectedTile.x, map.selectedTile.y); 
                    setStartAndEnd();
                    state = CharacterState.Moving;
                }
            }

            
            
        }
    }

    //Current path should be defined if you're in move state
    private void serviceMoveState()
    {
        if (currentPath != null)
        {
            followCurrentPath();
        }
    }

    public void followCurrentPath()
    {
        currentLerpTime += Time.deltaTime;
        transform.position = Vector3.MoveTowards(startPosition, endPosition, 5.0f * currentLerpTime);
        
        if (transform.position.Equals(endPosition))
        {

            currentPath.incrementPathStep();
            if (currentPath.getPathStep() < currentPath.getNumSteps()) {
                setStartAndEnd();
                Debug.Log("Current path step: " + currentPath.getStep().ToString());
                //Debug.Log("Subsequent: " + startPosition + " " + endPosition);
            } else
            {
                state = CharacterState.Selected;
                map.selectedTile = null;
                map.clearHighlights(movementRange);
                movementRange = map.getRangeTiles(posX, posY, 3);
                map.highlightTiles(movementRange, movementHighlight);
            }
        }
    }

    //Set the beginning and ending points for one segment of a path
    private void setStartAndEnd()
    {
        startPosition = transform.position;
        Vector2 currentStep = currentPath.getStep();

        if (currentStep.x == Path.VERTICAL) //Vertical step
        {
            endPosition = map.getTile(posX, posY + (int)currentStep.y).transform.position;
            Debug.Log("Subsequent: " + startPosition + " " + endPosition);
            Debug.Log("Position: " + posY);
            Debug.Log("CurrentStep.y: " + (int)currentStep.y);
            posY = posY + (int)currentStep.y;
        }
        else if (currentStep.x == Path.HORIZONTAL) //Horizontal step
        {
            endPosition = map.getTile(posX + (int)currentStep.y, posY).transform.position;
            posX = posX + (int)currentStep.y;
        }
        currentLerpTime = 0;
    }

    public void move(int x, int y)
    {
        posX = x;
        posY = y;
        transform.position = map.getTile(x, y).transform.position;
    }

    private Path buildPathToTile(int tileX, int tileY)
    {
        Path path = (Path)ScriptableObject.CreateInstance(typeof(Path));
        int xDiff = tileX - posX;
        int yDiff = tileY - posY;

        path.addStep(Path.HORIZONTAL, xDiff);
        path.addStep(Path.VERTICAL, yDiff);

        return path;
    }

    private void pathFind()
    {

    }
}
