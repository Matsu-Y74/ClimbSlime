using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;
# if UNITY_EDITOR
using UnityEditor;
# endif

//https://docs.unity3d.com/ja/current/Manual/Tilemap-ScriptableTiles-Example.html
public class InteractiveTileIndexUnit{
	public int Index {get;}
	public bool Flip {get;}
	public int Rotaiton {get;}
	public InteractiveTileIndexUnit(int index, bool flip, int rotaiton){
		Index = index;
		Flip = flip;
		Rotaiton = rotaiton;
	}
}
static class InteractiveTileIndex{

	static int ImmutableIndex(bool immutable_Flip, bool immutable_Rotation){
		return (immutable_Flip ? 2 : 0) + (immutable_Rotation ? 1 : 0);
	}

	public static InteractiveTileIndexUnit indexof(int mask, bool immutable_Flip, bool immutable_Rotation){
		return indexof(mask,ImmutableIndex(immutable_Flip,immutable_Rotation));
	}
	static int[,] index {get;} = {
		{   0,   1,   2,   3,   4,   5,   6,   7,   8,   9,  10,  11,  12,  13,  14,  15,  16,  17,  18,  19,  20,  21,  22,  23,  24,  25,  26,  27,  28,  29,  30,  31,  32,  33,  34,  35,  36,  37,  38,  39,  40,  41,  42,  43,  44,  45,  46,  47,  48,  49,  50,  51,  52,  53,  54,  55,  56,  57,  58,  59,  60,  61,  62,  63,  64,  65,  66,  67,  68,  69,  70,  71,  72,  73,  74,  75,  76,  77,  78,  79,  80,  81,  82,  83,  84,  85,  86,  87,  88,  89,  90,  91,  92,  93,  94,  95,  96,  97,  98,  99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255},
		{   0,   1,   2,   3,   1,   4,   5,   6,   2,   7,   8,   9,   3,  10,  11,  12,   1,  13,  14,  15,   4,  16,  17,  18,   5,  19,  20,  21,   6,  22,  23,  24,   2,  14,  25,  26,   7,  27,  28,  29,   8,  30,  31,  32,   9,  33,  34,  35,   3,  15,  26,  36,  10,  37,  38,  39,  11,  40,  41,  42,  12,  43,  44,  45,   1,   4,   7,  10,  13,  16,  19,  22,  14,  27,  30,  33,  15,  37,  40,  43,   4,  16,  27,  37,  16,  46,  47,  48,  17,  47,  49,  50,  18,  48,  51,  52,   5,  17,  28,  38,  19,  47,  53,  54,  20,  49,  55,  56,  21,  50,  57,  58,   6,  18,  29,  39,  22,  48,  54,  59,  23,  51,  60,  61,  24,  52,  62,  63,   2,   5,   8,  11,  14,  17,  20,  23,  25,  28,  31,  34,  26,  38,  41,  44,   7,  19,  30,  40,  27,  47,  49,  51,  28,  53,  55,  57,  29,  54,  60,  62,   8,  20,  31,  41,  30,  49,  55,  60,  31,  55,  64,  65,  32,  56,  65,  66,   9,  21,  32,  42,  33,  50,  56,  61,  34,  57,  65,  67,  35,  58,  66,  68,   3,   6,   9,  12,  15,  18,  21,  24,  26,  29,  32,  35,  36,  39,  42,  45,  10,  22,  33,  43,  37,  48,  50,  52,  38,  54,  56,  58,  39,  59,  61,  63,  11,  23,  34,  44,  40,  51,  57,  62,  41,  60,  65,  66,  42,  61,  67,  68,  12,  24,  35,  45,  43,  52,  58,  63,  44,  62,  66,  68,  45,  63,  68,  69},
		{   0,   1,   2,   3,   1,   4,   3,   5,   6,   7,   8,   9,  10,  11,  12,  13,  14,  15,  16,  17,  18,  19,  20,  21,  22,  23,  24,  25,  26,  27,  28,  29,  30,  31,  32,  33,  31,  34,  33,  35,  36,  37,  38,  39,  40,  41,  42,  43,  44,  45,  46,  47,  48,  49,  50,  51,  52,  53,  54,  55,  56,  57,  58,  59,  14,  18,  16,  20,  15,  19,  17,  21,  60,  61,  62,  63,  64,  65,  66,  67,  68,  69,  70,  71,  69,  72,  71,  73,  74,  75,  76,  77,  78,  79,  80,  81,  44,  48,  46,  50,  45,  49,  47,  51,  82,  83,  84,  85,  86,  87,  88,  89,  90,  91,  92,  93,  91,  94,  93,  95,  96,  97,  98,  99, 100, 101, 102, 103,   6,  10,   8,  12,   7,  11,   9,  13, 104, 105, 106, 107, 105, 108, 107, 109,  60,  64,  62,  66,  61,  65,  63,  67, 110, 111, 112, 113, 114, 115, 116, 117,  36,  40,  38,  42,  37,  41,  39,  43, 118, 119, 120, 121, 119, 122, 121, 123,  82,  86,  84,  88,  83,  87,  85,  89, 124, 125, 126, 127, 128, 129, 130, 131,  22,  26,  24,  28,  23,  27,  25,  29, 110, 114, 112, 116, 111, 115, 113, 117,  74,  78,  76,  80,  75,  79,  77,  81, 132, 133, 134, 135, 133, 136, 135, 137,  52,  56,  54,  58,  53,  57,  55,  59, 124, 128, 126, 130, 125, 129, 127, 131,  96, 100,  98, 102,  97, 101,  99, 103, 138, 139, 140, 141, 139, 142, 141, 143},
		{   0,   1,   2,   3,   1,   4,   3,   5,   2,   6,   7,   8,   3,   9,  10,  11,   1,  12,   6,  13,   4,  14,   9,  15,   3,  13,   8,  16,   5,  15,  11,  17,   2,   6,  18,  19,   6,  20,  19,  21,   7,  22,  23,  24,   8,  25,  26,  27,   3,  13,  19,  28,   9,  29,  30,  31,  10,  32,  26,  33,  11,  34,  35,  36,   1,   4,   6,   9,  12,  14,  13,  15,   6,  20,  22,  25,  13,  29,  32,  34,   4,  14,  20,  29,  14,  37,  29,  38,   9,  29,  25,  39,  15,  38,  34,  40,   3,   9,  19,  30,  13,  29,  28,  31,   8,  25,  24,  41,  16,  39,  33,  42,   5,  15,  21,  31,  15,  38,  31,  43,  11,  34,  27,  42,  17,  40,  36,  44,   2,   3,   7,  10,   6,   9,   8,  11,  18,  19,  23,  26,  19,  30,  26,  35,   6,  13,  22,  32,  20,  29,  25,  34,  19,  28,  24,  33,  21,  31,  27,  36,   7,   8,  23,  26,  22,  25,  24,  27,  23,  24,  45,  46,  24,  41,  46,  47,   8,  16,  24,  33,  25,  39,  41,  42,  26,  33,  46,  48,  27,  42,  47,  49,   3,   5,   8,  11,  13,  15,  16,  17,  19,  21,  24,  27,  28,  31,  33,  36,   9,  15,  25,  34,  29,  38,  39,  40,  30,  31,  41,  42,  31,  43,  42,  44,  10,  11,  26,  35,  32,  34,  33,  36,  26,  27,  46,  47,  33,  42,  48,  49,  11,  17,  27,  36,  34,  40,  42,  44,  35,  36,  47,  49,  36,  44,  49,  50}
	};
	static bool[,] flip {get;} = {
		{false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false},
		{false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false},
		{false,false,false,false, true,false, true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false, true,false, true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false, true, true, true, true, true, true, true, true,false,false,false,false,false,false,false,false,false,false,false,false, true,false, true,false,false,false,false,false,false,false,false,false, true, true, true, true, true, true, true, true,false,false,false,false,false,false,false,false,false,false,false,false, true,false, true,false,false,false,false,false,false,false,false,false, true, true, true, true, true, true, true, true,false,false,false,false, true,false, true,false, true, true, true, true, true, true, true, true,false,false,false,false,false,false,false,false, true, true, true, true, true, true, true, true,false,false,false,false, true,false, true,false, true, true, true, true, true, true, true, true,false,false,false,false,false,false,false,false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true,false,false,false,false, true,false, true,false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true,false,false,false,false, true,false, true,false},
		{false,false,false,false,false,false, true,false,false,false,false,false,false,false,false,false,false,false, true,false,false,false, true,false, true, true, true,false,false, true, true,false,false, true,false,false,false,false, true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false, true,false,false,false,false,false,false,false,false,false,false,false, true, true, true,false,false,false,false,false,false,false,false,false,false,false,false,false, true,false, true, true, true,false,false,false, true,false, true, true, true,false, true, true, true, true, true, true, true,false,false,false, true,false,false,false,false,false, true,false, true,false, true, true, true, true,false,false, true,false,false, true,false,false, true, true, true, true,false, true,false,false,false,false, true,false,false, true,false,false,false, true, true, true, true, true, true, true,false, true, true, true,false, true,false, true,false, true, true, true,false, true,false,false,false,false,false,false,false,false,false,false,false,false,false, true,false, true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false,false,false,false, true,false,false, true,false,false,false, true, true, true, true, true,false,false,false, true,false,false,false,false,false,false,false,false,false,false,false, true,false,false,false,false,false,false}
	};
	static int[,] rot {get;} = {
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,3,0,0,0,3,0,0,0,3,0,0,0,2,0,0,0,3,0,0,0,3,0,0,0,3,0,0,0,2,2,0,0,3,0,0,0,3,0,0,0,3,0,0,0,2,2,2,0,3,0,0,0,3,0,0,0,3,0,0,0,1,1,1,1,1,1,1,1,3,1,1,1,3,1,1,1,2,2,2,2,3,0,0,0,3,3,0,0,3,3,0,0,2,2,2,2,3,2,0,0,3,3,0,0,3,3,0,0,2,2,2,2,3,2,2,0,3,3,0,0,3,3,0,0,1,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,2,2,2,2,3,1,1,1,3,1,1,1,3,3,1,1,2,2,2,2,3,2,2,2,3,3,0,0,3,3,3,0,2,2,2,2,3,2,2,2,3,3,2,0,3,3,3,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,3,1,1,1,3,1,1,1,3,1,1,1,2,2,2,2,3,2,2,2,3,3,1,1,3,3,1,1,2,2,2,2,3,2,2,2,3,3,2,2,3,3,3,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,3,0,0,0,3,0,0,0,3,0,0,0,2,0,1,0,3,0,1,0,1,1,1,0,3,1,1,0,2,3,0,0,3,0,0,0,3,0,0,0,3,0,0,0,2,2,2,0,3,0,0,0,3,0,2,0,3,0,0,0,1,1,1,1,1,1,0,0,2,1,1,1,3,1,1,1,2,2,2,2,3,0,2,0,2,3,2,0,3,3,2,0,2,3,2,2,2,0,0,0,2,3,2,0,3,3,2,0,2,2,2,2,2,2,2,0,2,3,2,2,3,3,2,0,1,3,1,1,0,0,0,0,1,3,1,1,3,1,1,1,2,3,2,2,3,1,1,1,1,1,1,1,3,1,1,1,2,3,2,0,3,0,0,0,3,3,0,0,3,3,3,0,2,2,2,2,3,2,2,0,3,3,2,0,3,3,3,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,3,2,2,3,1,1,1,3,3,1,1,3,1,1,1,2,3,2,2,3,0,0,0,3,3,1,1,3,3,1,1,2,2,2,2,3,2,2,2,3,3,2,2,3,3,3,0}
	};

	static InteractiveTileIndexUnit indexof(int mask,int immutableIndex){
		return new InteractiveTileIndexUnit(
			index[immutableIndex,mask],
			flip[immutableIndex,mask],
			rot[immutableIndex,mask]
		);
	}
}

public class InteractiveTile : Tile 
{
	public bool Immutable_Flip;
	public bool Immutable_Rotation;

	public List<TileBase> InteractiveTileClasses = new List<TileBase>();

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
	/*
		indexと画像の関係性
		0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15
		XX XX XX XX FF XX F_ XX _F XX XX XX _F XX XX XX  15
		_F XX ff XX _F XX ff XX _F ff ff XX _F ff ff XX  31
		_F _F XX XX FF XX F_ XX _F XX XX XX _F XX XX XX  47
		_F _F _F XX _F XX XX XX _F XX ff XX _F XX XX XX  63
		FF FF FF FF FF FF FF FF _F _F _F _F _F _F _F _F  79
		_F _F _F _F FF XX F_ XX _F _F ff XX _F _F ff XX  95
		FF FF FF FF FF FF F_ F_ _F _F ff XX _F _F ff XX  111
		_F _F _F _F FF _F FF XX _F _F ff ff _F _F ff XX  127
		FF FF FF FF FF FF FF FF _F _F _F _F FF _F FF _F  143
		FF FF FF FF FF FF FF FF _F _F _F _F _F _F _F _F  159
		FF FF FF FF FF FF FF FF _F _F XX XX FF _F FF XX  175
		FF FF FF FF FF FF FF FF _F _F _F XX _F _F _F XX  191
		FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF  207
		FF FF FF FF FF FF FF FF _F _F _F _F FF _F FF _F  223
		FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF  239
		FF FF FF FF FF FF FF FF _F _F _F _F FF _F FF XX  255
		0~255のうち必要なものは以下となる.
		FF : Immutable_Flip, Immutable_Rotation のどちらもFalseの場合必要
		F_ : Immutable_Flip がFalseの場合必要
		_F : Immutable_Rotation がFalseの場合必要
		ff : Immutable_Flip, Immutable_Rotation の少なくともどちら片方がFalseの場合必要
		XX : 常に必要
	*/
	InteractiveTileIndexUnit indexof(Vector3Int l, ITilemap t){
        int mask_x = 0, mask_y = 0;
		mask_x += HasTile(t, l + new Vector3Int(-1,  1, 0)) ? 0b0001 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 0,  1, 0)) ? 0b0010 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 1,  1, 0)) ? 0b0100 : 0;
		mask_x += HasTile(t, l + new Vector3Int( 1,  0, 0)) ? 0b1000 : 0;

		mask_y += HasTile(t, l + new Vector3Int( 1, -1, 0)) ? 0b0001 : 0;
		mask_y += HasTile(t, l + new Vector3Int( 0, -1, 0)) ? 0b0010 : 0;
		mask_y += HasTile(t, l + new Vector3Int(-1, -1, 0)) ? 0b0100 : 0;
		mask_y += HasTile(t, l + new Vector3Int(-1,  0, 0)) ? 0b1000 : 0;

		int index = mask_y * 16 + mask_x;
		return InteractiveTileIndex.indexof(index,Immutable_Flip,Immutable_Rotation);
	}

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        InteractiveTileIndexUnit index = indexof(location, tilemap);
        if (index.Index >= 0 && index.Index < m_Sprites.Length)
        {
			tileData.sprite = m_Sprites[index.Index];
			tileData.color = Color.white;
			var m = tileData.transform;
			if(index.Flip){
				m.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f * index.Rotaiton),Vector3.one);
				m = new Matrix4x4(
					new Vector4(-1,0,0,0),
					new Vector4( 0,1,0,0),
					new Vector4( 0,0,1,0),
					new Vector4( 0,0,0,1)
					) * m;
			}
			else
				m.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f * index.Rotaiton),Vector3.one);
			tileData.transform = m;

			tileData.flags = TileFlags.LockTransform;
			tileData.colliderType = colliderType;
		}
        else
        {
        	Debug.LogWarning("Not enough sprites in InteractiveTile instance");
		}
    }
	
    private bool HasTile(ITilemap tilemap, Vector3Int position)
    {
		if(tilemap.GetTile(position) == this){
			return true;
		}
		else{
			return InteractiveTileClasses.Contains(tilemap.GetTile(position));
	    }
	}
	
	# if UNITY_EDITOR
    [MenuItem("Assets/Create/InteractiveTile")]
    public static void CreateInteractiveTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save InteractiveTile", "New InteractiveTile", "Asset", "Save InteractiveTile", "Assets/Object/Tilemap/Scriptable");
        if (path == "")
            return;
	    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<InteractiveTile>(), path);
    }
	# endif
}