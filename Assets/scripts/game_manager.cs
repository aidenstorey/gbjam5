using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum level_selector { level_1 = 0, level_2, level_3, level_4, max };
public enum game_class { warrior = 0, magician, thief, bowman, max };

public class game_manager : MonoBehaviour
{
	static game_manager gm;

	[System.NonSerialized]
	public game_class gc = game_class.warrior;

	[System.NonSerialized]
	public bool perfect_stats = false;

	level_selector current_ls = level_selector.level_1;
	level current_level;

	public Text debug_text;


	public static game_manager instance
	{
		get { return gm; }
	}

	public void set_level( level_selector ls )
	{
		switch ( ls )
		{
			case level_selector.level_1:
				this.current_level = new level_1();
				break;
			case level_selector.level_2:
				this.current_level = new level_2();
				break;
			case level_selector.level_3:
				this.current_level = new level_3();
				break;
			case level_selector.level_4:
				this.current_level = new level_4();
				break;
		}

		this.current_level.initialise();
	}

	public void level_completed()
	{
		this.current_ls++;

		if ( this.current_ls == level_selector.max )
		{
			// TODO:	Game finished
		}
		else
		{
			this.set_level( this.current_ls );
		}
	}

	public void game_over()
	{
	}

	public void destroy_game_object( GameObject go )
	{
		DestroyImmediate( go );
	}

	void Awake()
	{
		if ( gm == null )
		{
			gm = this;

			this.set_level( this.current_ls );
		}
		else
		{
			DestroyImmediate( this.gameObject );
		}
	}

	void Update()
	{
		if ( this.current_level.completed() )
		{
			this.level_completed();
		}
		else
		{
			this.current_level.handle();
		}

		this.debug_text.text = this.current_level.debug_output();
	}

	void OnGUI()
	{
		if ( this.current_level != null )
		{
			this.current_level.on_gui();
		}
	}
}
