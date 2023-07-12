
# AIPlayer Sınıfı
Bu belge, **AIPlayer** sınıfını açıklar. AIPlayer sınıfı, zeka düzeyi parametresine göre derecelendirilen bir AI oyuncusunu oluşturmak için kullanılır.

## Genel Bakış
* Namespace: Player
* Kalıtım: MonoBehaviour
* Scriptable Object: Hayır

## Amaç ve İşlev
AIPlayer sınıfı, AI oyuncusunun hareketini ve davranışını kontrol etmek için kullanılır. Bu sınıf, zeka düzeyini belirlemek için bir parametre alır ve bu parametreye göre hareket, hedef belirleme ve toplama işlemlerini gerçekleştirir.

## Özellikler
* **aiLogicLevel**: AI oyuncusunun zeka düzeyini belirleyen bir float değeri. İsteğe bağlı değerler alabilir: 1f, 2f, 3f, 4f, 5f.
* **pos**: Oyuncunun hedef pozisyonunu temsil eden bir Vector3 değeri.
* **rigidBody**: AI oyuncusunun Rigidbody bileşeni.
* **handle**: AI oyuncusunun toplama işlemini gerçekleştiren AICollectorHandle bileşeni.


##  Yöntemler
* **Start()**: Oyun nesnesi başlatıldığında çağrılan bir yöntem. Başlangıç pozisyonunu ve hareket sınırlarını ayarlar.
* **Update()**: Her güncelleme adımında çağrılan bir yöntem. Pozisyonu günceller ve beklemeyi kontrol eder.
* **MovePosition()**: Oyuncunun pozisyonunu güncelleyen bir yöntem. Oyuncunun hedefe doğru hareket etmesini sağlar.
* **IsInCorner()*: Oyuncunun köşede olup olmadığını kontrol eden bir yöntem. Köşede ise hedefi ve hareket zamanlayıcısını günceller.
* **TakePosition()**: Oyuncunun yeni bir pozisyon almasını sağlayan bir yöntem. Toplama ve serbest bırakma zamanlarını kontrol eder.
* **GetRandomPosition()**: Rastgele bir pozisyon elde etmek için kullanılan bir yöntem. Hareket sınırlarına uygun bir rastgele pozisyon döndürür.
* **GetTargetPosition()**: Hedef bir pozisyon elde etmek için kullanılan bir yöntem. Var olan nesnelerden rastgele bir hedef seçer ve hedefin pozisyonunu döndürür.
* **GetReleasePosition()**: Serbest bırakma pozisyonunu elde etmek için kullanılan bir yöntem. Toplayıcı nesnenin pozisyonunu döndürür.
* **IsReleaseTime(float objectCount)**: Serbest bırakma zamanının olup olmadığını kontrol eden bir yöntem. Belirli bir nesne sayısına göre serbest bırakma zamanını belirler.
* **OnCollisionEnter(Collision other)**: Başka bir nesneyle çarpışma olayında çağrılan bir yöntem. Oyuncu bir duvara çarptığında köşede olduğunu belirtir.