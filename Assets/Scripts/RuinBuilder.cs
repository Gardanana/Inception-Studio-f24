using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

[Serializable]
public class Row
{
    public List<Tile> tiles;
}

[Serializable]
public class Grid
{
    public List<Row> rows;
    int rowIndex = 0;
    int colIndex = 0;

    public Tile GetNextTile()
    {
        if(rowIndex < rows.Count)
        {
            if(colIndex < rows.Count)
            {
                Tile nextTile = rows[rowIndex].tiles[colIndex];
                colIndex++;
                return nextTile;
            }
            else
            {
                colIndex = 0;
                rowIndex++;
                if(rowIndex < rows.Count)
                {
                    Tile nextTile = rows[rowIndex].tiles[colIndex];
                    colIndex++;
                    return nextTile;
                }
                else
                {
                    return null;
                }
                
            }
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public class MultiGrid
{
    public List<Grid> grids;
    public int gridIndex;

    public Tile GetNextTile()
    {
        if (gridIndex < grids.Count)
        {
            Tile nextTile = grids[gridIndex].GetNextTile();

            if (nextTile != null)
            {
                return nextTile;
            }
            else
            {
                gridIndex++;

                if(gridIndex < grids.Count)
                {
                    Tile followingTile = grids[gridIndex].GetNextTile();

                    if (followingTile != null)
                    {
                        return followingTile;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                

            }
        }
        else
        {
            return null;
        }
        
    }
}



public class RuinBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject rockrockPart;

    [SerializeField]
    GameObject rockbushPart;

    [SerializeField]
    GameObject bushbushPart;

    [SerializeField]
    MultiGrid grid;
    public enum ReactionType
    {
        RockRock,
        RockBush,
        BushBush
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void BuildRuin(ReactionType type)
    {
        GameObject ruinPiece = null;
        switch(type)
        {
            case ReactionType.RockRock:
                ruinPiece = rockrockPart;
                break;
            case ReactionType.RockBush:
                ruinPiece = rockbushPart;
                break;
            case ReactionType.BushBush:
                ruinPiece = bushbushPart;
                break;
        }


        Tile nextTile = grid.GetNextTile();

        if(nextTile == null)
        {
            Debug.Log("No more tiles ;-;");
            return;
        }

        GameObject ruinObject = Instantiate(ruinPiece, nextTile.transform.position, Quaternion.identity);

        nextTile.PlaceObject(ruinObject);

        ruinObject.transform.parent = nextTile.transform;


        ruinObject.transform.position = nextTile.transform.position + (nextTile.transform.localScale / 2);
    }

    public void PackageBuilding()
    {
        foreach(Grid grids in grid.grids)
        {
            foreach(Row row in grids.rows)
            {
                foreach(Tile tile in row.tiles)
                {
                    tile.SetVisibility(false);
                }
            }
        }

        RuinBuilder clone = Instantiate(this, transform.position + new Vector3(30f, 0f, 30f), Quaternion.identity);
        clone.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

}
