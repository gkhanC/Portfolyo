# MapBuilder Sınıfı
Bu belge, MapBuilder sınıfını açıklar. Bu sınıf, LevelGenerator tarafından kullanılmak üzere oluşturulmuş bir Builder sınıfıdır. Temel olarak, MapLevelData adlı bir ScriptableObject tarafından sağlanan bilgileri kullanarak bir seviye oluşturur.

## Genel Bakış
* Namespace: LeveGenerator.Builders
* Kalıtım: BuilderBase
* Scriptable Object: Hayır

## Amaç ve İşlev
MapBuilder, MapLevelData adlı bir ScriptableObject tarafından sağlanan bilgileri kullanarak bir seviye oluşturma görevini üstlenir. 
Bu sınıf, MapLevelData nesnesinin içerdiği görüntülerin piksellerini okur ve bu pikselleri kullanarak ObjectData verileri üretir. 
Oluşturulan veriler, seviye oluşturucuya iletilir ve seviyenin inşa edilmesinde kullanılır.

## Özellikler
* isMapUsable: MapSizeCheck() yöntemi tarafından belirlenen koşullara göre haritanın kullanılabilir olup olmadığını gösteren bir özellik.

## Yöntemler
* Build(): Haritayı kullanarak seviye nesnelerini oluşturan yöntem. Oluşturulan ObjectData öğelerini içeren bir dizi döndürür. Harita kullanılamazsa null döner.

## Örnek Kullanım

````
// MapLevelData nesnesini oluştur
MapLevelData levelData = new MapLevelData();

// MapBuilder'ı oluştur
MapBuilder mapBuilder = new MapBuilder(levelData);

// Seviyeyi oluştur
ObjectData[] objects = mapBuilder.Build();

// Oluşturulan seviye nesnelerini kullan
foreach (ObjectData obj in objects)
{
// ...
}
````