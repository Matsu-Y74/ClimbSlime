using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
# if UNITY_EDITOR
using UnityEditor;
# endif

//https://docs.unity3d.com/ja/current/Manual/Tilemap-ScriptableTiles-Example.html
public class InteractiveTile : Tile 
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;
	
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int yd = -1; yd <= 1; yd++){
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (HasTile(tilemap, position))
                    tilemap.RefreshTile(position);
            }
		}
    }
	
	int indexof(Vector3Int l, ITilemap t){
        int mask_x = 0, mask_y = 0;
		mask_x += HasTile(t, l + new Vector3Int(-1,  1, 0)) || (HasTile(t, l + new Vector3Int(-1, 0, 0)) && HasTile(t, l + new Vector3Int( 0,  1, 0))) ? 0b0001 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 0,  1, 0)) ? 0b0010 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 1,  1, 0)) || (HasTile(t, l + new Vector3Int( 1, 0, 0)) && HasTile(t, l + new Vector3Int( 0,  1, 0))) ? 0b0100 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 1,  0, 0)) ? 0b1000 : 0;

		mask_y += HasTile(t, l + new Vector3Int( 1, -1, 0)) || (HasTile(t, l + new Vector3Int( 1, 0, 0)) && HasTile(t, l + new Vector3Int( 0, -1, 0))) ? 0b0001 : 0;
		mask_y += HasTile(t, l + new Vector3Int( 0, -1, 0)) ? 0b0010 : 0;
		mask_y += HasTile(t, l + new Vector3Int(-1, -1, 0)) || (HasTile(t, l + new Vector3Int(-1, 0, 0)) && HasTile(t, l + new Vector3Int( 0, -1, 0))) ? 0b0100 : 0;
		mask_y += HasTile(t, l + new Vector3Int(-1,  0, 0)) ? 0b1000 : 0;

		return mask_y * 16 + mask_x;
	}

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        int index = indexof(location, tilemap);
        if (index >= 0 && index < m_Sprites.Length)
        {
            tileData.sprite = m_Sprites[index];
            tileData.color = Color.white;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.None;
        }
        else
        {
        Debug.LogWarning("Not enough sprites in InteractiveTile instance");
}
    }
	
    private bool HasTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }
# if UNITY_EDITOR
// 以下はメニュー項目を加えて InteractiveTile アセットを作るヘルパーです
    [MenuItem("Assets/Create/InteractiveTile")]
    public static void CreateInteractiveTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save InteractiveTile", "New InteractiveTile", "Asset", "Save InteractiveTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<InteractiveTile>(), path);
    }
# endif
}