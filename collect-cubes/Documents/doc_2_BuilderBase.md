# BuilderBase Soyut Sınıfı
Bu döküman, BuilderBase soyut sınıfını açıklamaktadır. Bu sınıf, Level Builderler için işlev tanımayan bir soyutlama olarak tasarlanmıştır ve sürdürülebilirlik ilkelerini desteklemeyi amaçlar.

## Genel Bakış
BuilderBase soyut sınıfı, Level Builderler için temel bir şablondur. Bu sınıf, Level Builderlerin yapısını ve işleyişini belirlemek için kullanılır. Bir BuilderBase alt sınıfı, bir Level Builder'ın belirli bir özelliğini uygulayarak, belirli bir nesne koleksiyonunu veya veri yapısını oluşturur.

## Özellikler

**isBuilt (bool)**: Bir Level Builder'ın tamamlandığını veya inşa edildiğini belirten bir özelliktir.

**Build() (ObjectData[])**: Soyut bir metot olup, bir Level Builder'ın nesne koleksiyonunu veya veri yapısını oluşturur. Bu metot, türetilmiş sınıflar tarafından uygulanmalıdır.

## Kullanım
BuilderBase sınıfını kalıtım alan bir Level Builder sınıfı, Build() metodunu uygulamalı ve ilgili nesne 
koleksiyonunu veya veri yapısını döndürmelidir. Ayrıca, inşanın tamamlanıp tamamlanmadığını belirlemek 
için **isBuilt** özelliğini güncellemelidir.

Örnek Kod
Aşağıdaki örnek kodda, BuilderBase sınıfını kalıtım alan bir soyut sınıf gösterilmektedir:

````

namespace LeveGenerator.Builders.Abstract
{
	public abstract class BuilderBase
	{
		protected bool isBuilt { get; set; } = false;
		[CanBeNull] public abstract ObjectData[] Build();
	}
}

````
````
namespace LeveGenerator.Builders
{
    public class LevelBuilder : BuilderBase
    {        
        public overrride ObjectData[]? Build()
        {
            var objects = new ObjectData[1];
            
            //Level oluşturma ile ilgili işlemleri burada yapın
            
            isBuilt = true;
            return objects;
        }
    }
}
````

Bu örnek kodda, BuilderBase sınıfı isBuilt özelliği ve soyut Build() metoduyla tanımlanmıştır. Bu sınıf, bir Level Builder sınıfının temelini oluşturmak için kullanılabilir.

Bu döküman, BuilderBase soyut sınıfının kullanımını ve amacını açıklamaktadır. Bu soyut sınıf, Level Builderler için bir temel oluşturur ve sürdürülebilirlik ilkesine uygun bir yapı sağlar. BuilderBase sınıfını kalıtım alan sınıflar, Build() metodunu uygulayarak belirli bir nesne koleksiyonunu veya veri yapısını oluşturmalıdır.