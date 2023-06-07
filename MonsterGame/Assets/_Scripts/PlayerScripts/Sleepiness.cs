
using System;

namespace _Scripts.PlayerScripts
{
	public sealed class PlayerTiredChangedEvent : PlayerEvent
	{
		public PlayerTiredChangedEvent(Player pPlayer) : base(pPlayer)
		{
		}
	}
	
	public sealed class PlayerSleepEvent : PlayerEvent
	{
		public PlayerSleepEvent(Player pPlayer) : base(pPlayer)
		{
			
		}
	}


	/// <summary>
	/// Integer based sleepiness system for entities
	/// </summary>
	public class Sleepiness
	{
		private readonly Player _player;
		private PlayerSleepEvent _playerSleepEvent;
		private PlayerTiredChangedEvent _playerTiredChangedEvent;
		
		public Action OnSleep;
		public Action<int> OnTiredChanged;

		private int _tired;
		public int tired
		{
			get => _tired;
			set
			{
				if (value > _maxTired) _tired = _maxTired;
				else if (value > 0) _tired = value;
				else
				{
					_tired = 0;
					EventBus<PlayerSleepEvent>.Publish(new PlayerSleepEvent(_player));
					OnSleep?.Invoke();
				}
				OnTiredChanged?.Invoke(_tired);
				EventBus<PlayerTiredChangedEvent>.Publish(new PlayerTiredChangedEvent(_player));
			}
		}

		private int _maxTired;
		public int maxTired
		{
			get => _maxTired;
			set => _maxTired = value <= 0 ? 1 : value;
		}

		public Sleepiness(Player pPlayer, int pMaxTired, int pStartingTired = -1)
		{
			_player = pPlayer;
			_maxTired = pMaxTired;
			if (pStartingTired < 0) tired = pMaxTired;
		}

		public void Tire(int pAmount)
		{
			tired -= pAmount;
		}

		public void Waken(int pAmount)
		{
			tired += pAmount;
		}

		public void SendToSleep()
		{
			tired = 0;
		}
	
		public bool WouldNotFallAsleep(int pDamage)
		{
			return tired - pDamage > 0;
		}

	}
}