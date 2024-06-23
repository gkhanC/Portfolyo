namespace HypeFire.Library.GameSpecial.Qualification.Abstract
{
    public interface IDamageAble
    {
        public void TakeDamage(float damage);
    }

    public interface IDamageAble<in T>
    {
        public void TakeDamage(T damageData);
    }
}