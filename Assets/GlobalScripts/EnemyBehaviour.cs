using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour 
{

    public enum EnemyState
    {
        Moving,
        Idle, 
        Selected,
        SelectMove,
        ViewAttackRange,
        Attacking
    };
    private EnemyState state;

    private Map map;
    private Tile[] movementRange;
    private Tile[] meleeRange;
    private Tile selectedTile;
    private Tile selectedUnitTile;
    private bool attacking;
    Color movementHighlight = new Color(0, 0, 1f, .3f);
    Color attackHighlight = new Color(1f, 0, 0, .3f);
    private const int MOVEMENT_RANGE = 4;

    private List<Path> possiblePaths;
    private Path currentPath; //Current path for move state

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float currentLerpTime = 0;

    private float timer; // For initial wait 

    int posX, posY;

    void Awake()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        posX = 3;
        posY = 3;
        //move(posX, posY);
        movementRange = map.getMovementRangeTiles(3, 3, MOVEMENT_RANGE);
        //map.highlightTiles(movementRange, movementHighlight);

        possiblePaths = new List<Path>();

        state = EnemyState.Idle;
        currentPath = null;
        attacking = false;

        timer = 0;
    }
	// Use this for initialization
	void Start () 
    {
        Tile tileOn = map.getTile(posX, posY);
        tileOn.setCollideable(true);
        tileOn.enemyOnTile = this;
    }

    public EnemyState getState()
    {
        return state;
    }

    public void setState(EnemyState es)
    {
        state = es;
        if (state == EnemyState.Selected)
        {
            movementRange = map.getMovementRangeTiles(posX, posY, MOVEMENT_RANGE);
            map.highlightTiles(movementRange, movementHighlight);
        }
    }

    // Update is called once per frame
    void Update () 
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case EnemyState.Selected: serviceSelectedState(); break;
            case EnemyState.SelectMove: serviceSelectMove(); break;
            case EnemyState.Moving: serviceMoveState(); break;
            case EnemyState.ViewAttackRange: serviceViewAttackRangeState(); break;
            case EnemyState.Attacking: serviceAttackState(); break;
            case EnemyState.Idle: break;
        }
	}

    private void serviceSelectedState()
    {
        if (timer > 2f)
        {

            Tile tileOn = map.getTile(posX, posY);
            tileOn.setCollideable(false);
            tileOn.enemyOnTile = null;

            //map.clearHighlights(movementRange);
            movementRange = map.getMovementRangeTiles(posX, posY, MOVEMENT_RANGE);
            map.highlightTiles(movementRange, movementHighlight);
            state = EnemyState.SelectMove;
            timer = 0;
        }
    }

    private void serviceSelectMove()
    {
        if (timer > 2f)
        {
            meleeRange = map.getMeleeRange(posX, posY, MOVEMENT_RANGE);
            map.highlightTiles(meleeRange, attackHighlight);
            if(meleeRange.Length > 0)
            {
                List<Tile> moveTiles = new List<Tile>(movementRange);
                selectedUnitTile = meleeRange[0];

                selectedTile = map.getTile(meleeRange[0].x - 1, meleeRange[0].y);
                if(moveTiles.Contains(selectedTile))
                {
                    currentPath = buildPathToTile(selectedTile.x, selectedTile.y, movementRange);
                    setStartAndEnd();
                    state = EnemyState.Moving;
                    attacking = true;
                    return;
                }

                selectedTile = map.getTile(meleeRange[0].x, meleeRange[0].y - 1);
                if (moveTiles.Contains(selectedTile))
                {
                    currentPath = buildPathToTile(selectedTile.x, selectedTile.y, movementRange);
                    setStartAndEnd();
                    state = EnemyState.Moving;
                    attacking = true;
                    return;
                }

                selectedTile = map.getTile(meleeRange[0].x + 1, meleeRange[0].y);
                if (moveTiles.Contains(selectedTile))
                {
                    currentPath = buildPathToTile(selectedTile.x, selectedTile.y, movementRange);
                    setStartAndEnd();
                    state = EnemyState.Moving;
                    attacking = true;
                    return;
                }

                selectedTile = map.getTile(meleeRange[0].x, meleeRange[0].y + 1);
                if (moveTiles.Contains(selectedTile))
                {
                    currentPath = buildPathToTile(selectedTile.x, selectedTile.y, movementRange);
                    setStartAndEnd();
                    state = EnemyState.Moving;
                    attacking = true;
                    return;
                }
            }
            else
            {
                selectedTile = movementRange[(int)Random.Range(0, movementRange.Length)];
                currentPath = buildPathToTile(selectedTile.x, selectedTile.y, movementRange);
                setStartAndEnd();
                state = EnemyState.Moving;
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
            if (currentPath.getPathStep() < currentPath.getNumSteps()) 
            {
                setStartAndEnd();
            } 
            else
            {
                if (attacking)
                {
                    state = EnemyState.ViewAttackRange;
                }
                else
                {
                    //state = EnemyState.Selected;
                    state = EnemyState.Idle;
                }
                timer = 0;
                selectedTile = null;
                map.clearHighlights(movementRange);
                map.clearHighlights(meleeRange);
                Tile tileOn = map.getTile(posX, posY);
                tileOn.setCollideable(true);
                tileOn.enemyOnTile = this;
            }
        }
    }

    private void serviceViewAttackRangeState()
    {
        if (timer > 1f)
        {
            meleeRange = map.getRangeTiles(posX, posY, 1); // Melee range of 1
            map.highlightTiles(meleeRange, attackHighlight);
            state = EnemyState.Attacking;
            timer = 0;
        }
    }

    private void serviceAttackState()
    {
        if (timer > 1f)
        {
            selectedUnitTile.hasUnit = false;
            map.setTileColor(selectedUnitTile, Color.black);
            map.clearHighlights(meleeRange);
            selectedUnitTile = null;
            attacking = false;
            state = EnemyState.Selected;
            timer = 0;
        }
    }


    #region Pathfinding
    //Set the beginning and ending points for one segment of a path
    private void setStartAndEnd()
    {
        startPosition = transform.position;
        Vector2 currentStep = currentPath.getStep();

        if (currentStep.x == Path.VERTICAL) //Vertical step
        {
            endPosition = map.getTile(posX, posY + (int)currentStep.y).transform.position;
            //Debug.Log("Subsequent: " + startPosition + " " + endPosition);
            //Debug.Log("Position: " + posY);
            //Debug.Log("CurrentStep.y: " + (int)currentStep.y);
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

    // The following assumes no obsticles, and makes a basic path to the tile
    private Path buildPathToTile(int tileX, int tileY)
    {
        Path path = (Path)ScriptableObject.CreateInstance(typeof(Path));
        int xDiff = tileX - posX;
        int yDiff = tileY - posY;

        path.addStep(Path.HORIZONTAL, xDiff);
        path.addStep(Path.VERTICAL, yDiff);

        return path;
    }

    // The following uses the tileList and takes into account obsticles, avoiding them
    //  as it builds a path to the tile
    private Path buildPathToTile(int tileX, int tileY, Tile[] tileList)
    {
        Path path = (Path)ScriptableObject.CreateInstance(typeof(Path));
        Vector2 dstPos = new Vector2(tileX, tileY);
        List<Tile> openTiles = new List<Tile>(tileList);
        List<Tile> closedTiles = new List<Tile>();
        Tile throwawayTile = movementRange[0];
        path = pathFind(posX, posY, dstPos, openTiles, closedTiles, path, throwawayTile, 0, 99);

        if (path == null)
        {
            Debug.Log("Null path!");
            return buildPathToTile(tileX, tileY);
        }

        Debug.Log("Path: " + path.ToString());

        return path;
    }

    // The following is a recursive method to help the tileList-based buildPath method
    private Path pathFind(int tileX, int tileY, Vector2 dstPos, List<Tile> openTiles, List<Tile> closedTiles, Path p, Tile t, int dist, int bestDist)
    {
        if(tileX == dstPos.x && tileY == dstPos.y)
        {
            return p;
        }

        int newBest = 0;
        List<int> bestTypes = new List<int>();
        Path originalPath = p.clone();

        t = map.getTile(tileX - 1, tileY);
        if (openTiles.Contains(t))
        {
            newBest = (int)(Mathf.Abs(t.x - dstPos.x) + Mathf.Abs(t.y - dstPos.y) + dist);
            if(newBest <= bestDist)
            {
                if (newBest != bestDist)
                    bestTypes.Clear();
                bestDist = newBest;
                bestTypes.Add(1);
            }
        }

        t = map.getTile(tileX, tileY - 1);
        if (openTiles.Contains(t))
        {
            newBest = (int)(Mathf.Abs(t.x - dstPos.x) + Mathf.Abs(t.y - dstPos.y) + dist);
            if (newBest <= bestDist)
            {
                if (newBest != bestDist)
                    bestTypes.Clear();
                bestDist = newBest;
                bestTypes.Add(2);
            }
        }

        t = map.getTile(tileX + 1, tileY);
        if (openTiles.Contains(t))
        {
            newBest = (int)(Mathf.Abs(t.x - dstPos.x) + Mathf.Abs(t.y - dstPos.y) + dist);
            if (newBest <= bestDist)
            {
                if (newBest != bestDist)
                    bestTypes.Clear();
                bestDist = newBest;
                bestTypes.Add(3);
            }
        }

        t = map.getTile(tileX, tileY + 1);
        if (openTiles.Contains(t))
        {
            newBest = (int)(Mathf.Abs(t.x - dstPos.x) + Mathf.Abs(t.y - dstPos.y) + dist);
            if (newBest <= bestDist)
            {
                if (newBest != bestDist)
                    bestTypes.Clear();
                bestDist = newBest;
                bestTypes.Add(4);
            }
        }
        int bestType = 0;
        for(int i = 0; i < bestTypes.Count; i++)
        {
            bestType = bestTypes[i];
            
            p = originalPath.clone();

            switch (bestType)
            {
                case 1:
                    t = map.getTile(tileX - 1, tileY);
                    openTiles.Remove(t);
                    closedTiles.Add(t);
                    p.addStep(Path.HORIZONTAL, -1);
                    p = pathFind(tileX - 1, tileY, dstPos, openTiles, closedTiles, p, t, dist++, bestDist);
                    if (p != null)
                    {
                        return p;
                    }
                    break;
                case 2:
                    t = map.getTile(tileX, tileY - 1);
                    openTiles.Remove(t);
                    closedTiles.Add(t);
                    p.addStep(Path.VERTICAL, -1);
                    p = pathFind(tileX, tileY - 1, dstPos, openTiles, closedTiles, p, t, dist++, bestDist);
                    if (p != null)
                    {
                        return p;
                    }
                    break;
                case 3:
                    t = map.getTile(tileX + 1, tileY);
                    openTiles.Remove(t);
                    closedTiles.Add(t);
                    p.addStep(Path.HORIZONTAL, 1);
                    p = pathFind(tileX + 1, tileY, dstPos, openTiles, closedTiles, p, t, dist++, bestDist);
                    if (p != null)
                    {
                        return p;
                    }
                    break;
                case 4:
                    t = map.getTile(tileX, tileY + 1);
                    openTiles.Remove(t);
                    closedTiles.Add(t);
                    p.addStep(Path.VERTICAL, 1);
                    p = pathFind(tileX, tileY + 1, dstPos, openTiles, closedTiles, p, t, dist++, bestDist);
                    if (p != null)
                    {
                        return p;
                    }
                    break;
            }
        }
        return null;
    }
    #endregion
}
