using UnityEngine;
using System.Collections;

enum quest_state { nothing, started, finished };

public class level_2 : level
{
	quest_state qs = quest_state.nothing;

	public void progress_quest()
	{
		this.qs++;
	}

	public override void initialise()
	{
	}

	public override bool completed()
	{
		return this.qs == quest_state.finished;
	}

	public override void handle()
	{
		if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			this.progress_quest();
		}
	}

	public override string debug_output()
	{
		return "level 2\n" + this.qs.ToString();
	}
}
