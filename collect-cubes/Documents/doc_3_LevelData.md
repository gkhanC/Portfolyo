# LevelData Scriptable Nesnesi
Bu döküman, **LevelData Scriptable Nesnesi'ni** açıklamaktadır. Bu nesne, Level Builderlar 
tarafından kullanılmak üzere tasarlanmıştır ve esneklik ve veri yönetimi kolaylığı sağlamak 
amacıyla Scriptable olarak tasarlanmıştır.

## Genel Bakış
**LevelData**, bir oyun seviyesinin verilerini temsil eden bir Scriptable Nesnesidir. Bu nesne, 
bir seviyede oluşturulacak nesne sayısını ve nesne önfabrikasını içerir. Level Builderlar, bu 
Scriptable Nesne üzerindeki verilere erişerek seviyeleri oluşturabilir.

## Özellikler
**getObjectCount (int)**: Oluşturulacak nesne sayısını belirten bir özelliktir.
**objectPrefab (GameObject)**: Nesne önfabrikasını temsil eden bir özelliktir.

## Kullanım
**LevelData** Scriptable Nesnesi, Unity Editor'ün **CreateAssetMenu** özelliği kullanılarak oluşturulabilir. 
Bu nesne, Level Builderlar tarafından kullanılmak üzere tasarlanmıştır. Level Builderlar, bu Scriptable 
Nesneyi seviye verilerine erişmek ve seviyeleri oluşturmak için kullanabilir.

## Örnek Kod
Aşağıdaki örnek kodda, LevelData Scriptable Nesnesi gösterilmektedir:

````
using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace LeveGenerator.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelGenerator/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] public int getObjectCount { get; protected set; } = 0;
        [field: SerializeField] public float objectSize { get; set; } = 1f;
        [SerializeField] public GameObject objectPrefab { get; protected set; } = null;

        public LevelData(int objectCount, GameObject objectPrefab)
        {
            getObjectCount = objectCount;
            this.objectPrefab = objectPrefab;
        }

        public LevelData()
        {
        }

        private void OnEnable()
        {
            this.LogSuccess($"A new {nameof(LevelData)} Asset has been created");
        }
    }
}
````

Bu örnek kodda, LevelData Scriptable Nesnesi, getObjectCount ve objectPrefab özelliklerine sahiptir. 
Bu özellikler, bir seviyede oluşturulacak nesne sayısını ve nesne önfabrikasını temsil eder. Ayrıca, 
bu örnek kodda OnEnable metodu kullanılarak bir hata ayıklama mesajı oluşturulur.

Bu döküman, LevelData Scriptable Nesnesini açıklamaktadır. Bu nesne, Level Builderlar tarafından kullanılmak
üzere tasarlanmıştır ve Scriptable olarak tasarlanarak esneklik sağlamayı ve veri yönetimini kolaylaştırmayı 
amaçlar. LevelData nesnesi, bir seviyenin nesne sayısını ve nesne önfabrikasını içerir. Bu nesne, Unity Editor'de
CreateAssetMenu özelliği kullanılarak oluşturulabilir ve Level Builderlar tarafından seviye oluşturmak için kullanılabilir.