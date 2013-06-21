﻿using System;
using ScrapePack.TaskActions.MsBuild;

namespace ScrapePack.TaskActionConfig
{
	public class TaskActionFactory
	{
		private readonly MsBuildActionBuilder _msBuildActionBuilder;

		public TaskActionFactory()
			: this(new MsBuildActionBuilder())
		{
		}

		public TaskActionFactory(MsBuildActionBuilder msBuildActionBuilder)
		{
			_msBuildActionBuilder = msBuildActionBuilder;
		}

		public Action BuildAction<TActionConfigurator>(Type actionBuilderType, Action<TActionConfigurator> actionConfigurator)
		{
			IActionBuilder<TActionConfigurator> actionBuilder = null;

			if (actionBuilderType == typeof(MsBuildActionBuilder))
			{
				actionBuilder = _msBuildActionBuilder as IActionBuilder<TActionConfigurator>;
			}

			Action action;
			if (actionBuilder != null)
			{
				action = actionBuilder.ConfigureAction(actionConfigurator);
			}
			else
			{
				// TODO: Implement with proper exception
				throw new Exception("Unknown builder!");
			}
			return action;
		}
	}
}