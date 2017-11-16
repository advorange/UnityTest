using System;

namespace Assets.Scripts.WeaponScripts
{
	[Flags]
	public enum WeaponEffectTargets : uint
	{
		Shootable = (1U << 0),
		Reflectable = (1U << 1),
	}
}
