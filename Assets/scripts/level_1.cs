using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class level_1 : level
{
	Dictionary<string, int> stats = new Dictionary<string, int>()
	{
		{ "str", 0 },
		{ "dex", 0 },
		{ "int", 0 },
		{ "luk", 0 },
	};

	Dictionary<game_class, List<string>> desired = new Dictionary<game_class, List<string>>()
	{
		{game_class.warrior, new List<string>() { "int", "luk" } },
		{game_class.magician, new List<string>() { "str", "dex" } },
		{game_class.thief, new List<string>() { "str", "int" } },
		{game_class.bowman, new List<string>() { "int", "luk" } }
	};

	int stat_distributable = 9;
	int stat_base = 4;
	int stat_max = 9;

	bool jstfg = false;

	public void rotate_class_left()
	{
		game_manager.instance.gc = ( game_class )( ( ( int )game_manager.instance.gc - 1 + ( int )game_class.max ) % (int)game_class.max );
	}

	public void rotate_class_right()
	{
		game_manager.instance.gc = ( game_class )( ( ( int )game_manager.instance.gc + 1 + ( int )game_class.max ) % ( int )game_class.max );
	}

	public void generate_stats()
	{
		List<string> keys = Enumerable.ToList( this.stats.Keys );

		foreach ( string key in keys )
		{
			this.stats[ key ] = this.stat_base;
		}

		for ( int i = 0; i < this.stat_distributable; i++ )
		{
			while ( true )
			{
				string key = keys[ Random.Range( 0, keys.Count ) ];

				if ( this.stats[ key ] < this.stat_max )
				{
					this.stats[ key ]++;
					break;
				}
			}
		}
	}

	public void just_start_the_fucking_game()
	{
		this.jstfg = true;
	}

	public override void initialise()
	{
		this.generate_stats();
	}

	public override bool completed()
	{
		bool completed = true;

		List<string> keys = this.desired[ game_manager.instance.gc ];

		foreach ( string key in keys )
		{
			if ( this.stats[ key ] != 4 )
			{
				completed = false;
				break;
			}
		}

		if ( completed )
		{
			game_manager.instance.perfect_stats = true;
		}

		return completed || this.jstfg;
	}

	public override void handle()
	{
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			this.just_start_the_fucking_game();
		}
	}

	public override void on_gui()
	{
		GUI.DrawTexture( new Rect( 0, 0, 160, 144 ), art_manager.instance.level_1_background.texture );

		var class_text = art_manager.instance.get_class_text( game_manager.instance.gc );
		var rect_class = class_text.rect;
		rect_class.x = 80 - ( ( int )rect_class.width / 2 ) - 1;
		rect_class.y = 20;
		GUI.DrawTexture( rect_class, class_text.texture );

		var num_str = art_manager.instance.get_number( this.stats[ "str" ] );
		var rect_str = num_str.rect;
		rect_str.x = 60;
		rect_str.y = 43;
		GUI.DrawTexture( rect_str, num_str.texture );

		var num_dex = art_manager.instance.get_number( this.stats[ "dex" ] );
		var rect_dex = num_dex.rect;
		rect_dex.x = 60;
		rect_dex.y = 63;
		GUI.DrawTexture( rect_dex, num_dex.texture );

		var num_int = art_manager.instance.get_number( this.stats[ "int" ] );
		var rect_int = num_int.rect;
		rect_int.x = 60;
		rect_int.y = 83;
		GUI.DrawTexture( rect_int, num_int.texture );

		var num_luk = art_manager.instance.get_number( this.stats[ "luk" ] );
		var rect_luk = num_luk.rect;
		rect_luk.x = 60;
		rect_luk.y = 103;
		GUI.DrawTexture( rect_luk, num_luk.texture );

		var arrow_left = art_manager.instance.level_1_arrow_left;
		var rect_arrow_left = arrow_left.rect;
		rect_arrow_left.x = 36;
		rect_arrow_left.y = 17;
		if ( GUI.Button( rect_arrow_left, arrow_left.texture, new GUIStyle() ) )
		{
			this.rotate_class_left();
		}

		var arrow_right = art_manager.instance.level_1_arrow_right;
		var rect_arrow_right = arrow_right.rect;
		rect_arrow_right.x = 113;
		rect_arrow_right.y = 17;
		if ( GUI.Button( rect_arrow_right, arrow_right.texture, new GUIStyle() ) )
		{
			this.rotate_class_right();
		}

		var dice = art_manager.instance.level_1_dice;
		var rect_dice = dice.rect;
		rect_dice.x = 98;
		rect_dice.y = 78;
		if ( GUI.Button( rect_dice, dice.texture, new GUIStyle() ) )
		{
			this.generate_stats();
		}
	}

	public override string debug_output()
	{
		string output = "level 1\n" + game_manager.instance.gc.ToString() + "\n";

		foreach ( KeyValuePair<string, int> stat in this.stats )
		{
			output += stat.Key + ": " + stat.Value + "\n";
		}

		return output;
	}
}
