using Assets.Scripts.WeaponScripts;

namespace Assets.Scripts
{
	public abstract class Damagable : EntityWeaponCollisionInteraction
	{
		public int StartingLife = 100;
		protected int _CurrentLife;

		protected virtual void Awake()
		{
			_CurrentLife = StartingLife;
		}
	}
}
