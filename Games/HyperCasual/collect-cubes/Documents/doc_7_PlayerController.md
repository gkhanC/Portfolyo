# PlayerController Sınıfı
Bu belge, PlayerController sınıfını açıklar. Bu sınıf, oyun karakterini kontrol etmek ve yönetmek için kullanılır.

## Genel Bakış
* Namespace: Player
* Kalıtım: MonoBehaviour, IInputListener
* Scriptable Object: Hayır

## Amaç ve İşlev
PlayerController sınıfı, oyun karakterinin hareketini ve diğer işlevlerini kontrol etmek için kullanılır. Bu sınıf, bir oyun karakterinin fiziksel hareketini, dönüşünü ve sınırlarını yönetmek için gerekli işlevleri sağlar. Ayrıca, giriş olaylarını dinlemek ve toplama işlemlerini gerçekleştirmek gibi ek özelliklere sahiptir.

## Özellikler
* **isLevelStarted**: Oyun seviyesinin başlayıp başlamadığını belirten bir boolean değer.
* **collectedColor**: Toplanan nesnelerin renk değeri.
* **collectorHandle**: Toplama işlemini gerçekleştiren koleksiyoncu bileşeni.
* **playerData**: Oyuncu verilerini içeren bir PlayerData nesnesi.
* **moveAbleArea**: Hareketin sınırlarını belirleyen bir PositionalConstraint nesnesi.


# Yöntemler

* **Awake()**: Oyun nesnesi uyanırken çağrılan bir yöntem. Gerekli bileşenleri başlatır ve bazı kontrolleri gerçekleştirir.
* **Start()**: Oyun nesnesi başlatıldığında çağrılan bir yöntem. Giriş olaylarını dinlemeyi başlatır ve diğer başlatma kontrollerini gerçekleştirir.
* **FixedUpdate()**: Fizik güncellemelerinde çağrılan bir yöntem. Oyun karakterinin hareketini ve sınırlarını günceller.
* **ApplyConstraints()**: Hareketin sınırlarını uygulayan bir yöntem. Oyun karakterinin konumunu sınırlarla kısıtlayarak günceller.
* **LevelStartedListener(ObserverData<bool> data)**: Oyun seviyesinin başladığı olayı dinleyen bir yöntem. isLevelStarted özelliğini günceller.
* **LevelStartedErrorListener(ObserverData<bool> data)**: Oyun seviyesinin başlama hatası olayını dinleyen bir yöntem. isLevelStarted özelliğini günceller.
* **InputListening(IInputResult result)**: Giriş olaylarını dinleyen bir yöntem. Gelen giriş sonuçlarını işler.
* **OnTriggerEnter(Collider other)**: Oyun karakterinin başka bir nesneyle temas ettiğinde çağrılan bir yöntem. Toplama işlemini gerçekleştirir.