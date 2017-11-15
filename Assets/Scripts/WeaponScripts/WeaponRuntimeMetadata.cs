namespace Assets.Scripts.WeaponScripts
{
	public class WeaponRuntimeMetadata
	{
		public readonly Weapon Weapon;
		public int CurrentMagazineAmmo { get; internal set; }

		public WeaponRuntimeMetadata(Weapon source)
		{
			Weapon = source;

			var gun = source.GetComponent<Gun>();
			if (gun)
			{
				CurrentMagazineAmmo = gun.CurrentMagazineSize;
			}
		}
	}
}
