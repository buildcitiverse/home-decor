using System;

namespace GGMatch3
{
	public class SnowCover : SlotComponent
	{
		public override bool isBlockingBurriedElement
		{
			get
			{
				return true;
			}
		}

		public override bool isBlockingCarpetSpread
		{
			get
			{
				return true;
			}
		}

		public override bool isAttachGrowingElementSuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isPlaceBubbleSuspended
		{
			get
			{
				return true;
			}
		}

		public override void AddToGoalsAtStart(Match3Goals goals)
		{
		}

		public override bool IsCompatibleWithPickupGoal(Match3Goals.ChipTypeDef chipTypeDef)
		{
			return false;
		}

		public override int sortingOrder
		{
			get
			{
				return this.initProperties.sortingOrder;
			}
		}

		public void Init(SnowCover.InitProperties initProperties)
		{
			this.initProperties = initProperties;
		}

		public override bool isBlockingChip
		{
			get
			{
				return true;
			}
		}

		public override int blockerLevel
		{
			get
			{
				return 1;
			}
		}

		public override bool IsAvailableForDiscoBombSuspended(bool replaceWithBombs)
		{
			return true;
		}

		public override bool isSlotMatchingSuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isMoveIntoSlotSuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isSlotGravitySuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isSlotSwapSuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isPreventingGravity
		{
			get
			{
				return true;
			}
		}

		public override bool isPowerupReplacementSuspended
		{
			get
			{
				return true;
			}
		}

		public override bool isCreatePowerupWithThisSlotSuspended
		{
			get
			{
				return false;
			}
		}

		public override bool isMovingWithConveyor
		{
			get
			{
				return false;
			}
		}

		public override bool isDestroyedByMatchingNextTo
		{
			get
			{
				return true;
			}
		}

		public override SlotDestroyResolution OnDestroyNeighbourSlotComponent(Slot slotBeingDestroyed, SlotDestroyParams destroyParams)
		{
			SlotDestroyResolution result = default(SlotDestroyResolution);
			if (destroyParams.isHitByBomb && !destroyParams.isBombAllowingNeighbourDestroy)
			{
				return result;
			}
			result.isDestroyed = true;
			result.stopPropagation = true;
			return this.DestroyLayer(destroyParams);
		}

		public override SlotDestroyResolution OnDestroySlotComponent(SlotDestroyParams destroyParams)
		{
			SlotDestroyResolution result = default(SlotDestroyResolution);
			result.isDestroyed = true;
			if (destroyParams.isFromTap)
			{
				result.stopPropagation = true;
				return result;
			}
			result.stopPropagation = true;
			result.isNeigbourDestroySuspendedForThisChipOnly = true;
			return this.DestroyLayer(destroyParams);
		}

		private SlotDestroyResolution DestroyLayer(SlotDestroyParams destroyParams)
		{
			SlotDestroyResolution result = default(SlotDestroyResolution);
			result.isDestroyed = true;
			result.isNeigbourDestroySuspendedForThisChipOnly = true;
			result.stopPropagation = true;
			Match3Game game = this.lastConnectedSlot.game;
			game.Play(GGSoundSystem.SFXType.SnowDestroy);
			if (this.slot != null)
			{
				game.particles.CreateParticles(this.slot.localPositionOfCenter, Match3Particles.PositionType.BubblesDestroy);
			}
			if (this.slot != null)
			{
				this.slot.GetSlotComponent<Chip>();
			}
			if (this.slot != null && this.initProperties.wobbleSettings != null)
			{
				this.slot.Wobble(this.initProperties.wobbleSettings);
			}
			base.RemoveFromGame();
			return result;
		}

		private SnowCover.InitProperties initProperties;

		public struct InitProperties
		{
			public WobbleAnimation.Settings wobbleSettings;

			public int sortingOrder;
		}
	}
}
