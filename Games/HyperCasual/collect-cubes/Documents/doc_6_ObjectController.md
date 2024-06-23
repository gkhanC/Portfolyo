# ObjectController Sınıfı
Bu belge, ObjectController sınıfını açıklar. Bu sınıf, Builder tarafından üretilen nesnelerin üzerinde işlemleri kolaylaştırır
ve yönetir.

## Genel Bakış
* Namespace: Objects
* Kalıtım: MonoBehaviour, IObjectController
* Scriptable Object: Hayır

## Amaç ve İşlev
ObjectController, Builder tarafından üretilen nesnelerin üzerinde işlemleri kolaylaştırır ve yönetir. Bu sınıf, bir oyun nesnesini
kontrol etmek için gerekli işlevleri sağlar. Renk, konum ve boyut gibi özellikleri ayarlamak için yöntemler içerir.

## Özellikler
* getGameObject: Sınıfa ait oyun nesnesini döndüren bir özellik.
* getTransform: Sınıfa ait transform bileşenini döndüren bir özellik.

## Yöntemler
* SetColor(Color color): Nesnenin rengini ayarlamak için kullanılan yöntem. Bir MeshRenderer bileşeni kullanarak nesnenin rengini değiştirir.
* SetLocalPosition(Vector3 position): Nesnenin yerel konumunu ayarlamak için kullanılan yöntem. Yerel konumunu belirtilen position değeriyle günceller.
* SetSize(float size): Nesnenin boyutunu tek bir değerle ayarlamak için kullanılan yöntem. Yerel ölçeğini yatay eksende size değeriyle günceller.
* SetSize(Vector3 size): Nesnenin boyutunu bir vektörle ayarlamak için kullanılan yöntem. Yerel ölçeğini size vektörüyle günceller.

## Örnek Kullanım

````
// ObjectController örneğini al
ObjectController objectController = GetComponent<ObjectController>();

// Nesnenin rengini ayarla
objectController.SetColor(Color.red);

// Nesnenin konumunu ayarla
objectController.SetLocalPosition(new Vector3(0, 0, 5));

// Nesnenin boyutunu ayarla
objectController.SetSize(2f);
````