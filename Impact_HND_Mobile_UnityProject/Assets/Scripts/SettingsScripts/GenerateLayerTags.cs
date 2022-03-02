using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class GenerateLayerTags : MonoBehaviour
	{

		public bool LayerPlayer = false;
		public bool LayerGround = false;
		public bool LayerPlayerChild = false;
		public bool TagPlayer = false;
		public bool TagPlanet = false;
		public bool TagGameController = false;
		public bool TagEnemy = false;

		void Awake()
		{

			if (LayerPlayer)
			{
				gameObject.layer = LayerMask.NameToLayer(Const.PLAYER);
			}
			else if (LayerPlayerChild)
			{
				gameObject.layer = LayerMask.NameToLayer(Const.PLAYER_CHILD);
			}
			else if (LayerGround)
			{
				gameObject.layer = LayerMask.NameToLayer(Const.GROUND);
			}

			if (TagPlayer)
			{
				gameObject.tag = Const.PLAYER;
			}
			else if (TagPlanet)
			{
				gameObject.tag = Const.PLANET;
			}
			else if (TagGameController)
			{
				gameObject.tag = Const.GAMECONTROLLER;
			}
			else if (TagEnemy)
			{
				gameObject.tag = Const.ENEMY;
			}
		}
	}
}
