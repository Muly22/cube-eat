namespace Lesson
{
	interface IWeapon
	{
		void Fire();
	}
	interface ITrowingWeapon : IWeapon
	{
		void Trovn();
	}
	class Gan : IWeapon
	{
		public void Fire()
		{
			Console.WriteLine(GetType().Name + " в бан");
		}
	}
	class Laser : IWeapon
	{
		public void Fire()
		{
			Console.WriteLine(GetType().Name + " в бан вжжжжжж");
		}
	}
	class Knife : ITrowingWeapon
	{
		public void Fire()
		{
			Console.WriteLine(GetType().Name + " в бан килллл");
		}

		public void Trovn()
		{
			Console.WriteLine(GetType().Name + " в бан получай по щам");
		}
	}
	class Pleyer
	{
		public void Give(IWeapon weapon)
		{
			weapon.Fire();
		}
		public void Throw(ITrowingWeapon trowingWeapon)
		{
			trowingWeapon.Trovn();
		}
	}
	class Program
	{
		static void Main(string[] ager)
		{
			Pleyer pleyer = new Pleyer();
			IWeapon[] weapons = { new Gan(), new Laser(), new Knife() };
			foreach (var item in weapons)
			{
				pleyer.Give(item);
			}
			pleyer.Throw(new Knife());
		}
	}
}