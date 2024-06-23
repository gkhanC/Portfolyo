# MapLevelData Scriptable Nesnesi
Bu döküman, **MapLevelData Scriptable Nesnesi'ni** açıklamaktadır. Bu nesne, **MapBuilder** sınıfında
kullanılmak üzere verileri konteynırlayan bir depo görevi görür. **MapLevelData** nesnesi, görseldeki 
pikselleri okuyarak bir seviye oluşturma işlemini destekler.

## Genel Bakış
**MapLevelData**, bir seviyenin verilerini temsil eden bir **Scriptable** Nesnesidir. Bu nesne, üst görünüm harita
(topLevelMap), ön görünüm harita (frontLevelMap) ve yan görünüm harita (sideLevelMap) gibi görselleri içerir. 
Bu görsellerin piksellerini okuyarak **MapBuilder** sınıfının seviye oluşturma işlemini destekler.

## Özellikler
topLevelMap (Texture2D): Haritanın üst görümünü temsil eden bir özelliktir.
frontLevelMap (Texture2D): Haritanın ön görümünü temsil eden bir özelliktir.
sideLevelMap (Texture2D): Haritanın yan görümünü temsil eden bir özelliktir.

Sağlıklı bir kullanım için görsellerin aynı boyutta olması önemlidir.

## Kalıtım
**MapLevelData**, **LevelData** sınıfından **kalıtılarak** oluşturulmuştur. Bu sayede **MapLevelData**, **LevelData**
sınıfının özelliklerine ve davranışlarına sahiptir.

## Kullanım
**MapLevelData** Scriptable Nesnesi, Unity Editor'ün **CreateAssetMenu özelliği kullanılarak oluşturulabilir**. 
Bu nesne, **MapBuilder** sınıfında kullanılmak üzere tasarlanmıştır. **MapBuilder**, **MapLevelData** nesnesine 
erişerek görsellerdeki pikselleri okuyabilir ve seviyeleri oluşturabilir.

## Örnek Kod
Aşağıdaki örnek kodda, MapLevelData Scriptable Nesnesi gösterilmektedir:

````
using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace LeveGenerator.Data
{
    [CreateAssetMenu(fileName = "MapLevelData", menuName = "LevelGenerator/MapLevelData")]
    public class MapLevelData : LevelData
    {
        [SerializeField] public Texture2D topLevelMap { get; private set; }
        [SerializeField] public Texture2D frontLevelMap { get; private set; }
        [SerializeField] public Texture2D sideLevelMap { get; private set; }

        public MapLevelData(Texture2D top, Texture2D front, Texture2D side, int objectCount, GameObject objectPrefab)
            : base(objectCount, objectPrefab)
        {
            topLevelMap = top;
            frontLevelMap = front;
            sideLevelMap = side;
        }

        public MapLevelData() : base()
        {
        }

        private void OnEnable()
        {
            this.LogSuccess($"A new {nameof(MapLevelData)} Asset has been created");
        }
    }
}
````