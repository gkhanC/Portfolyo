using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace HypeFire.Library.Utilities.Extensions.Vector
{
	public static class VectorExtensions
	{
		public static Vector3 Multiplier(this Vector3 baseValue, Vector3 Value)
		{
			float x = 0f, y = 0f, z = 0f;
			x = baseValue.x * Value.x;
			y = baseValue.y * Value.y;
			z = baseValue.z * Value.z;
			return new Vector3(x, y, z);
		}

		/// <summary>
		/// Vektor iz düşümünün tersini işaret etmek için vektor büyüklüğünü flip eder
		/// Dönüş değeri 0 veya 1'dir,
		/// <para>
		/// 0'dan büyük değerler için 0, 0 ve 0'dan küçük değerler için 1 döndürür.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public static Vector3 Flip(this Vector3 Value)
		{
			float x = 0f, y = 0f, z = 0f;
			x = Value.x > 0f ? 0f : 1f;
			y = Value.y > 0f ? 0f : 1f;
			z = Value.z > 0f ? 0f : 1f;
			return new Vector3(x, y, z);
		}

		public static Vector3 FlipWithBoolean(this Vector3 Value)
		{
			Value.x = Value.x > 0 ? 0 : 1f;
			Value.y = Value.y > 0 ? 0 : 1f;
			Value.z = Value.z > 0 ? 0 : 1f;
			return Value;
		}

		public static Vector3 GetDirection(this Vector3 Value, Vector3 target) => (target - Value).normalized;

		public static Vector3 ChangeToX(this Vector3 Value, float newX)
		{
			Value.x = newX;
			return Value;
		}

		public static Vector3 ChangeToY(this Vector3 Value, float newY)
		{
			Value.y = newY;
			return Value;
		}

		public static Vector3 ChangeToZ(this Vector3 Value, float newZ)
		{
			Value.z = newZ;
			return Value;
		}

		/// <summary>
		/// Vector değerinin kordinatını değiştirir.
		/// <para>
		/// Vector(x,y,z) için => (2,1,3) paremetresi Vector(y,x,z) sonucunu döndürür.
		/// </para>
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="Xorder"></param>
		/// <param name="Yorder"></param>
		/// <param name="Zorder"></param>
		/// <returns></returns>
		public static Vector3 OrderChange(this Vector3 Value, int Xorder = 0, int Yorder = 0, int Zorder = 0)
		{
			var result = new Vector3();

			if (Xorder != 0 && Yorder != 0 && Xorder == Yorder)
			{
				Value.LogWarning(nameof(OrderChange), "Y value overrides to X value");
			}

			if (Xorder != 0 && Zorder != 0 && Xorder == Zorder)
			{
				Value.LogWarning(nameof(OrderChange), "Z value overrides to X value");
			}

			if (Yorder != 0 && Zorder != 0 && Yorder == Zorder)
			{
				Value.LogWarning(nameof(OrderChange), "Z value overrides to Y value");
			}

			switch (Xorder)
			{
				case 1:
					result.x = Value.x;
					break;

				case 2:
					result.y = Value.x;
					break;

				case 3:
					result.z = Value.x;
					break;
				default:

					break;
			}

			switch (Yorder)
			{
				case 1:
					result.x = Value.y;
					break;

				case 2:
					result.y = Value.y;
					break;

				case 3:
					result.z = Value.y;
					break;
				default:

					break;
			}

			switch (Zorder)
			{
				case 1:
					result.x = Value.z;
					break;

				case 2:
					result.y = Value.z;
					break;

				case 3:
					result.z = Value.z;
					break;
				default:

					break;
			}

			return result;
		}

		/// <summary>
		/// Vector değerinin kordinatını değiştirir.
		/// <para>
		/// Vector(x,y,z) için => (2,1,3) paremetresi Vector(y,x,z) sonucunu döndürür.
		/// </para>
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="Xorder"></param>
		/// <param name="Yorder"></param>
		/// <param name="Zorder"></param>
		/// <returns></returns>
		public static Vector3 OrderChange(this Vector2 Value, int Xorder = 0, int Yorder = 0, int Zorder = 0)
		{
			var result = new Vector3();

			if (Xorder != 0 && Yorder != 0 && Xorder == Yorder)
			{
				Value.LogWarning(nameof(OrderChange), "Y value overrides to X value");
			}

			if (Xorder != 0 && Zorder != 0 && Xorder == Zorder)
			{
				Value.LogWarning(nameof(OrderChange), "Z value overrides to X value");
			}

			if (Yorder != 0 && Zorder != 0 && Yorder == Zorder)
			{
				Value.LogWarning(nameof(OrderChange), "Z value overrides to Y value");
			}

			switch (Xorder)
			{
				case 1:
					result.x = Value.x;
					break;

				case 2:
					result.y = Value.x;
					break;

				case 3:
					result.z = Value.x;
					break;
				default:

					break;
			}

			switch (Yorder)
			{
				case 1:
					result.x = Value.y;
					break;

				case 2:
					result.y = Value.y;
					break;

				case 3:
					result.z = Value.y;
					break;
				default:

					break;
			}

			switch (Zorder)
			{
				case 1:
					result.x = 0;
					break;

				case 2:
					result.y = 0;
					break;

				case 3:
					result.z = 0;
					break;
				default:

					break;
			}

			return result;
		}
	}
}