using System;

namespace Assets.WeaponScripts.GunScripts.BulletScripts
{
	[Flags]
	public enum BulletEffectTargets : uint
	{
		Shootable = (1U << 0),
		Reflectable = (1U << 1),
	}
}
