using UnityEngine;
using System.Collections.Generic;

public class skill
{
	public skill( string name, float max, float remaining )
	{
		this.name = name;
		this.max = max;
		this.remaining = remaining;
	}

	public string name;
	public float max;
	public float remaining;
}

public class level_3 : level
{
	float time_remaining = 60.0f;

	List<skill> active = new List<skill>()
	{
		new skill("skill_a", 5.0f, 5.0f),
		new skill("skill_b", 6.0f, 6.0f),
		new skill("skill_c", 3.0f, 3.0f)
	};

	List<skill> inactive = new List<skill>()
	{
	};

	float health = 100.0f;

	int damage_base = 1;
	float damage_timer_max = 1.0f;
	float damage_timer_current = 0.0f;

	public void skill_pressed( string name )
	{
        for ( int a = 0; a < this.active.Count; a++ )
		{
			if ( this.active[ a ].name == name )
			{
				this.active[ a ].remaining = this.active[ a ].max;
				return;
			}
		}

		int i = 0;
		for ( ; i < this.inactive.Count && this.inactive[ i ].name != name; i++ );

		this.inactive[ i ].remaining = this.inactive[ i ].max;
		this.active.Add( this.inactive[ i ] );
		this.inactive.RemoveAt( i );
	}

	public override void initialise()
	{
	}

	public override bool completed()
	{
		return this.time_remaining <= 0.0f;
	}

	public override void handle()
	{
		foreach ( skill s in this.active )
		{
			s.remaining -= Time.deltaTime;
			if ( s.remaining <= 0.0f )
			{
				this.inactive.Add( s );
			}
		}

		foreach ( skill s in this.inactive )
		{
			this.active.Remove( s );
		}

		if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			this.skill_pressed( "skill_a" );
		}

		if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			this.skill_pressed( "skill_b" );
		}

		if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
		{
			this.skill_pressed( "skill_c" );
		}

		this.time_remaining -= Time.deltaTime;
		
		this.damage_timer_current += Time.deltaTime;
		if ( this.damage_timer_current >= this.damage_timer_max)
		{
			this.health -= this.damage_base * this.inactive.Count;
			this.damage_timer_current = 0.0f;
		}

		if ( this.health <= 0 && this.time_remaining > 0.0f )
		{
			game_manager.instance.game_over();
		}
	}

	public override string debug_output()
	{
		string output = "level 3\ntr: " + (int)this.time_remaining + "\nh: " + this.health + "\n";

		foreach ( skill s in this.active )
		{
			output += s.name + ": " + (int)s.remaining + "\n";
		}

		return output;
	}
}
