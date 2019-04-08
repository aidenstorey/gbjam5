using UnityEngine;
using System.Collections.Generic;

public class level_4 : level
{
	float spawn_timer_max = 5.0f;
	float spawn_timer_current = 0.0f;

	int boss_health = 100;
	int boss_position = 80;
	int boss_damage = 10;

	float damage_timer_max = 2.0f;
	float damage_timer_current = 0.0f;

	int player_health = 100;
	int player_position = 0;
	Dictionary<game_class, int> player_damage = new Dictionary<game_class, int>()
	{
		{ game_class.warrior, 6 },
		{ game_class.magician, 4 },
		{ game_class.thief, 5 },
		{ game_class.bowman, 5 },
	};

	Dictionary<game_class, int> player_distance = new Dictionary<game_class, int>()
	{
		{ game_class.warrior, 20 },
		{ game_class.magician, 40 },
		{ game_class.thief, 20 },
		{ game_class.bowman, 60 },
	};

	float skill_cooldown = 1.0f;
	float skill_cooldown_current = 0.0f;

	int tolerance = 5;

	public bool in_tolerance()
	{
		int distance_required = this.player_distance[ game_manager.instance.gc ];
		int distance_current = Mathf.Abs( this.player_position - this.boss_position );

		return 
			distance_required - this.tolerance <= distance_current &&
			distance_current <= distance_required + this.tolerance;
	}

	public override void initialise()
	{
	}

	public override bool completed()
	{
		return this.boss_health <= 0;
	}

	public override void handle()
	{
		if ( this.spawn_timer_current < this.spawn_timer_max )
		{
			this.spawn_timer_current += Time.deltaTime;
		}
		else
        {
			this.damage_timer_current += Time.deltaTime;
			if ( this.damage_timer_current >= this.damage_timer_max )
			{
				if ( !this.in_tolerance() )
				{
					this.player_health -= this.boss_damage;
				}

				this.damage_timer_current = 0.0f;
			}
		}

		if ( Input.GetKeyDown( KeyCode.LeftArrow ) )
		{
			this.player_position = Mathf.Max( this.player_position - 1, 0 );
		}

		if ( Input.GetKeyDown( KeyCode.RightArrow ) )
		{
			this.player_position = Mathf.Min( this.player_position + 1, 160 );
		}

		this.skill_cooldown_current = Mathf.Max( this.skill_cooldown_current - Time.deltaTime, 0.0f );

		if ( Input.GetKey( KeyCode.Alpha1 ) )
		{
			if ( this.skill_cooldown_current <= 0.0f )
			{
				if ( this.in_tolerance() && this.spawn_timer_current >= this.spawn_timer_max )
				{
					int damage = this.player_damage[ game_manager.instance.gc ];
					if ( game_manager.instance.perfect_stats )
					{
						damage *= 2;
					}

					this.boss_health -= damage;
				}

				this.skill_cooldown_current = this.skill_cooldown;
			}
		}


		if ( this.player_health <= 0 && this.boss_health > 0 )
		{
			game_manager.instance.game_over();
		}
	}

	public override string debug_output()
	{
		string output =
			"level 4" + 
			"\nbs: " + (this.spawn_timer_current >= this.spawn_timer_max).ToString() + 
			"\nbh: " + this.boss_health +
			"\nph: " + this.player_health +
			"\nd: " + Mathf.Abs( this.player_position - this.boss_position ) + 
			"\nit: " + this.in_tolerance();

		return output;
	}
}
