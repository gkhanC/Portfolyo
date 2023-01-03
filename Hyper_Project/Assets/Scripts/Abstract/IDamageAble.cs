namespace HyperProject.Abstract
{
    public interface IDamageAble
    {
        /// <summary>
        /// Add damage
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(float damage = 0f);
    }
}