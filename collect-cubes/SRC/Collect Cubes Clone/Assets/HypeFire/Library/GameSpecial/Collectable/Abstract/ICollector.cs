namespace HypeFire.Library.GameSpecial.Collectable.Abstract
{
	public interface ICollector
	{
		public void TakeCollectable(ICollectableObject cObject);
		public void TakeCollectable<T>(ICollectableGeneric<T> cObject);
	}
}