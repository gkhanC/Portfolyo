# ObjectData
Bu belge, ObjectData.cs için açıklayıcı bir formatında rehber sunar.



```
using System;
using UnityEngine;

namespace LeveGenerator.Builders
{

    [Serializable]
    public class ObjectData
    {
    public Color color;
    public Vector3 position;

        public ObjectData(Color c, Vector3 p)
        {
            color = c;
            position = p;
        }

        public ObjectData()
        {
        }
    }
}
```

## ObjectData
ObjectData sınıfı, seviye oluşturma sürecinde oluşturulan nesnelerle ilgili bilgileri depolamak için kullanılır. Serileştirme özelliği sayesinde Unity'de nesnelerin kaydedilmesi ve yüklenmesi kolaylaşır.

## Özellikler
**color** : Nesnenin rengini temsil eden Color türünde bir özellik.
**position**: Nesnenin konumunu temsil eden Vector3 türünde bir özellik.

## Kurucular
ObjectData(Color c, Vector3 p): Color türünde c ve Vector3 türünde p parametrelerini alan bir kurucu metot. color ve position özelliklerini başlatmak için kullanılır.
ObjectData(): Parametre almayan bir kurucu metot. ObjectData sınıfından bir örneği başlatır.

ObjectData sınıfı, genellikle bir [Builder](https://bitbucket.org/gkhanc/collect-cubes-clone/src/master/Documents/doc_2_BuilderBase.md) sınıf içinde kullanılarak oluşturulan nesnelerle ilgili bilgileri depolamak için kullanılır. Bu sınıf, oluşturma sürecinde her bir nesneyle ilgili verileri saklamak ve geri almak için kullanılır.

Daha fazla bilgi için [BuilderBase](https://bitbucket.org/gkhanc/collect-cubes-clone/src/master/Documents/doc_2_BuilderBase.md) ve türetilmiş sınıfları inceleyebilirsiniz.