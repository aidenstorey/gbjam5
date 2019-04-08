using UnityEngine;
using System.Collections;

public class art_manager : MonoBehaviour
{
	static art_manager am;

	public Sprite num_null;
	public Sprite num_0;
	public Sprite num_1;
	public Sprite num_2;
	public Sprite num_3;
	public Sprite num_4;
	public Sprite num_5;
	public Sprite num_6;
	public Sprite num_7;
	public Sprite num_8;
	public Sprite num_9;

	public Sprite class_text_null;
	public Sprite class_text_warrior;
	public Sprite class_text_magician;
	public Sprite class_text_thief;
	public Sprite class_text_bowman;

	public Sprite level_1_background;
	public Sprite level_1_arrow_left;
	public Sprite level_1_arrow_right;
	public Sprite level_1_dice;

	public static art_manager instance
	{
		get { return am; }
	}

	public Sprite get_number( int number )
	{
		switch ( number )
		{
			case 0:
				return this.num_0;
			case 1:
				return this.num_1;
			case 2:
				return this.num_2;
			case 3:
				return this.num_3;
			case 4:
				return this.num_4;
			case 5:
				return this.num_5;
			case 6:
				return this.num_6;
			case 7:
				return this.num_7;
			case 8:
				return this.num_8;
			case 9:
				return this.num_9;
			default:
				return this.num_null;
		}
	}

	public Sprite get_class_text( game_class gc )
	{
		switch ( gc )
		{
			case game_class.warrior:
				return this.class_text_warrior;
            case game_class.magician:
				return this.class_text_magician;
            case game_class.thief:
				return this.class_text_thief;
            case game_class.bowman:
				return this.class_text_bowman;
            default:
				return this.class_text_null;
        }
	}

	void Awake()
	{
		if ( am == null )
		{
			am = this;
		}
		else
		{
			DestroyImmediate( this.gameObject );
		}
	}
}
