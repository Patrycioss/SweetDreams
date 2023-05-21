
namespace _Scripts.PlayerScripts
{
	/// <summary>
	/// Integer based vitality system for entities
	/// </summary>
	public class Vitality
	{
		public delegate void OnDeath();
		public event OnDeath OnDeathEvent;

		public delegate void OnHealthChanged(int pNewHealth);
		public event OnHealthChanged OnHealthChangedEvent;

		private int _health;
		public int health
		{
			get => _health;
			set
			{
				if (value > _maxHealth) _health = _maxHealth;
				else if (value > 0) _health = value;
				else
				{
					_health = 0;
					OnDeathEvent?.Invoke();
				}
			
				OnHealthChangedEvent?.Invoke(_health);
			}
		}

		private int _maxHealth;
		public int maxHealth
		{
			get => _maxHealth;
			set => _maxHealth = value <= 0 ? 1 : value;
		}

		public Vitality(int pMaxHealth, int pStartingHealth = -1)
		{
			_maxHealth = pMaxHealth;
			if (pStartingHealth < 0) health = pMaxHealth;
		}

		public void Damage(int pDamage)
		{
			health -= pDamage;
		}

		public void Heal(int pAmount)
		{
			health += pAmount;
		}

		public void Kill()
		{
			health = 0;
		}
	
		public bool WouldSurvive(int pDamage)
		{
			return health - pDamage > 0;
		}

	}
}