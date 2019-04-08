using UnityEngine;
using System.Collections;

public class level
{
	public virtual void initialise()
	{
		
	}

	public virtual bool completed()
	{
		return false;
	}

	public virtual void handle()
	{
	}

	public virtual void on_gui()
	{
	}

	public virtual string debug_output()
	{
		return "";
	}
}
