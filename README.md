Invoice Check API - Durum Kontrol Servisi
Bu proje, e-fatura süreçlerine yönelik teknik değerlendirme kapsamında geliştirilmiş bir .NET 8 Web API uygulamasıdır. Uygulama, mock bir entegratör servisi simüle ederek fatura durumlarını kontrol eder, sonuçları veritabanına loglar ve belirli kurallar çerçevesinde (ardışık red) bloklama mekanizması uygular.

Teknolojiler ve Gereksinimler
Projenin çalıştırılabilmesi için aşağıdaki bileşenlerin kurulu olması gerekmektedir:

SDK: .NET 8.0 SDK

Veritabanı: PostgreSQL

ORM: Entity Framework Core

Cache: .NET In-Memory Cache

 Kurulum ve Yapılandırma
1. Projeyi Klonlayın/İndirin
Proje dosyalarını bilgisayarınıza indirin ve terminali proje dizininde açın.

2. Veritabanı Ayarları
Uygulamanın veritabanına bağlanabilmesi için appsettings.json dosyasındaki bağlantı cümlesini (Connection String) kendi ortamınıza göre düzenleyiniz.

Dosya: appsettings.json

JSON

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=invoicecheck;Username=postgres;Password=kendi_sifreniz"
}
