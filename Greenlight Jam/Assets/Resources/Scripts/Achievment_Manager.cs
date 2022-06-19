using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievment_Manager : Data_Manager
{

	class Achievement
	{
		int current, needed;
		string type;

		public void add(int val)
		{
			int temp = current + val;
			if (temp >= needed)
			{
				current = finishGoal(temp);
			}
		}

		private int finishGoal(int total)
		{
			int remainder = 0;
			if (total > needed)
				remainder = total - needed;
			setGoal();
			//Notify user
			return remainder;
		}

		public void setGoal()
		{
			int newGoal = needed;

			if (needed < 50)
			{
				newGoal += 10;
			}
			else if (needed < 100)
			{
				newGoal += 50;
			}
			else if (needed < 500)
			{
				newGoal += 1000;
			}
		}
	}
}
