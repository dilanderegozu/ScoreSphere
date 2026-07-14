# ⚽ ScoreSphere: Çok Katmanlı Spor Veri Platformu

**ScoreSphere**, futbol liglerini, maçları, puan durumlarını ve canlı skorları yöneten, uçtan uca geliştirilmiş bir spor veri platformudur. Proje; katmanlı mimari (N-Tier Architecture), Repository Pattern ve AutoMapper kullanılarak, veriyi API üzerinden yöneten ve iki bağımsız istemci (WebApi + WebUI) tarafından tüketilen bir yapıda kurulmuştur.

## 🏗️ Mimari

Proje, sorumlulukları net şekilde ayıran **6 katmanlı bir çözüm (solution)** olarak tasarlandı:

```
ScoreSphere.EntityLayer      → Veritabanı tablolarını temsil eden Entity sınıfları
ScoreSphere.DtoLayer         → Katmanlar arası veri taşıyan DTO'lar (Result / Create / Update)
ScoreSphere.DataAccessLayer  → Repository Pattern + Entity Framework Core + SQL Server
ScoreSphere.BusinessLayer    → İş kuralları, servis katmanı, AutoMapper konfigürasyonu
ScoreSphere.WebApi           → RESTful API, Swagger ile dokümante edilmiş
ScoreSphere.WebUI            → ASP.NET Core MVC, WebApi'ye HttpClientFactory ile bağlanan bağımsız istemci
```

### Katman Bağımlılık Zinciri

```
EntityLayer   ← hiçbir katmana bağımlı değil
DtoLayer      ← hiçbir katmana bağımlı değil (Entity'den tamamen izole)
DataAccessLayer  ← EntityLayer
BusinessLayer    ← DataAccessLayer, EntityLayer, DtoLayer
WebApi           ← BusinessLayer
WebUI            ← (HTTP üzerinden WebApi'ye bağlanır, hiçbir iç katmana referans vermez)
```

**WebUI ve WebApi tamamen bağımsız iki process olarak çalışır.** WebUI, kendi DTO kopyalarını taşır ve `IHttpClientFactory` ile WebApi'nin JSON endpoint'lerini tüketir — bu sayede iki istemci birbirinden habersiz, ayrı ayrı deploy edilebilir yapıdadır.

## 🗄️ Veri Modeli

| Entity | Açıklama |
|---|---|
| `Team` | Takım bilgileri (isim, logo, stadyum, şehir) |
| `League` | Lig bilgileri (isim, ülke, logo) |
| `Season` | Sezon bilgileri (başlangıç/bitiş tarihi, aktiflik durumu) |
| `Match` | Maç bilgileri (takımlar, tarih, skor, durum, hafta) |
| `Goal` | Gol kayıtları (oyuncu, dakika, penaltı/kendi kalesine) |
| `MatchEvent` | Maç olayları (gol, kart, oyuncu değişikliği, VAR) |
| `MatchStat` | Maç istatistikleri (topa sahip olma, şut, korner, faul, kart) |

`Team` ile `Match` arasındaki çift foreign key ilişkisi (`HomeTeamId`/`AwayTeamId`), Fluent API ile açıkça tanımlanarak EF Core'un cascade delete çakışmalarından kaçınıldı.

## 🔧 Teknik Kararlar

### Repository Pattern (Generic + Özelleştirilmiş)
`IGenericDal<T>` üzerinden ortak CRUD operasyonları sağlanırken, `IMatchDal` gibi entity'ye özel repository'ler (`GetMatchesByWeekAsync`, `GetMatchesByStatusAsync`, `GetMatchWithDetailsByIdAsync`) ile özel sorgu ihtiyaçları karşılandı.

### AutoMapper
Entity ↔ Dto dönüşümleri `GeneralMapping.cs` üzerinden merkezi olarak yönetiliyor. Enum alanları (`MatchStatus`, `EventType`) API katmanında `string`'e çevrilerek WebUI'nin bağımsız Dto kopyalarıyla uyumsuzluk riskini ortadan kaldırdı.

### Puan Durumu Hesaplaması — Computed Yaklaşım
Ayrı bir `Standing` tablosu tutmak yerine, puan durumu **her istekte `Match` tablosundan anlık hesaplanıyor** (`StandingManager.GetStandingsBySeasonAsync`). Bu, veri senkronizasyon riskini tamamen ortadan kaldırıyor — bir maç güncellendiğinde puan durumu otomatik olarak doğru yansır, ekstra bir güncelleme adımına gerek kalmaz.

### MatchDetailDto — Zengin Detay Sayfası
Maç detay sayfası için `Match`, `Goals`, `MatchEvents` ve `MatchStat`'ı tek bir istekte birleştiren `MatchDetailDto` tasarlandı (`GET /api/Match/{id}/detail`), çoklu API çağrısı ihtiyacını ortadan kaldırdı.

## 🎨 Tasarım Sistemi

WebUI, Material Design 3 renk token'ları üzerine kurulu, dark-mode odaklı özel bir Tailwind CSS konfigürasyonu kullanıyor. Tasarım, `_Layout.cshtml` (public site) ile paylaşılan header/footer/mobile-nav partial'ları üzerinden tutarlı bir görünüm sağlıyor.

## 📄 Tamamlanan Sayfalar (Public Site)

### 🏠 Ana Sayfa
- Hero section: canlı/öne çıkan maçın gerçek zamanlı gösterimi
- Feature cards: platformun temel özelliklerinin tanıtımı
<img width="1896" height="870" alt="Ekran görüntüsü 2026-07-14 162037" src="https://github.com/user-attachments/assets/1382ec43-b299-49ee-b0cc-90ce9256876b" />
<img width="1889" height="869" alt="Ekran görüntüsü 2026-07-14 162047" src="https://github.com/user-attachments/assets/c858e23e-2029-45c3-81db-ef7a931f8bb5" />


### ⚡ Canlı Skorlar (`/Match`)
- **Filtreleme:** Hepsi / Canlı / Bitenler / Yaklaşanlar (server-side, sayfa yenilenerek)
- **Sayfalama:** Server-side pagination, her sayfada 4 maç
- **Lig gruplama:** Maçlar `GroupBy` ile ligler altında toplanarak gösteriliyor
- **Sidebar:** Popüler ligler listesi + haftanın maçı kartı
<img width="1893" height="865" alt="Ekran görüntüsü 2026-07-14 162058" src="https://github.com/user-attachments/assets/5ce71501-4fa9-4c31-a075-f802da40a705" />
<img width="1891" height="865" alt="Ekran görüntüsü 2026-07-14 162107" src="https://github.com/user-attachments/assets/80d9fd16-50dd-4e9e-b274-4a2acdcd8773" />
<img width="1891" height="863" alt="Ekran görüntüsü 2026-07-14 162122" src="https://github.com/user-attachments/assets/238d496a-d76c-4422-ad57-d19002601b12" />


### 📅 Fikstür (`/Fixture`)
- Lig ve hafta seçici (dropdown, birbirine bağımlı — lig değişince o ligin gerçek ilk haftası otomatik seçilir)
- Gün bazlı gruplanmış maç listesi
- Sidebar: derbi/öne çıkan maç kartı + lig filtre listesi (maç sayılarıyla)
<img width="1880" height="851" alt="Ekran görüntüsü 2026-07-14 162141" src="https://github.com/user-attachments/assets/3b60bd13-ee35-4191-b245-e148e8bfc2fa" />
<img width="1886" height="861" alt="Ekran görüntüsü 2026-07-14 162151" src="https://github.com/user-attachments/assets/e2cfa80d-6bb5-4c38-a2cc-9f0229e1f8ae" />


### 🏆 Puan Durumu (`/Standing`)
- Dinamik lig seçici
- Şampiyonlar Ligi / Küme Düşme bölgelerinin otomatik renklendirilmesi (sabit değil, takım sayısına göre hesaplanan)
- Son 5 maçlık form gösterimi (Galibiyet/Beraberlik/Mağlubiyet)
<img width="1881" height="866" alt="Ekran görüntüsü 2026-07-14 162159" src="https://github.com/user-attachments/assets/9335603e-be75-42b4-840f-a8ef412650cd" />
<img width="1883" height="867" alt="Ekran görüntüsü 2026-07-14 162209" src="https://github.com/user-attachments/assets/829d737d-603e-4a53-8ccb-ced9323c487a" />


### 📊 Maç Detay (`/MatchDetail/Index/{id}`)
- Skor tablosu ve maç durumu (Canlı / Bitti / Başlamadı)
- Gol listesi (ev sahibi/deplasman ayrımına göre görsel hizalama)
- İstatistik çubukları (topa sahip olma, şut, pas isabeti, korner, faul, ofsayt)
- Maç akışı / zaman çizelgesi (gol, kart, oyuncu değişikliği kronolojik sırayla)
<img width="1895" height="870" alt="Ekran görüntüsü 2026-07-14 162241" src="https://github.com/user-attachments/assets/7f823285-8303-44e3-b779-355cfef2e5f6" />
<img width="1881" height="870" alt="Ekran görüntüsü 2026-07-14 162250" src="https://github.com/user-attachments/assets/7ddb95a2-8adb-4d90-9495-0185cd2721d0" />
<img width="1886" height="867" alt="Ekran görüntüsü 2026-07-14 162257" src="https://github.com/user-attachments/assets/84e6c966-91df-4dd9-aa3a-92e9c6eb1621" />



## 🔜 Sırada — Admin Panel

Public site tamamlandıktan sonra, Team/League/Season/Match/Goal/MatchEvent yönetimi için ayrı bir Admin Paneli (Areas ile izole edilmiş) geliştirilecek.

## 🛠️ Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 10 Web API, Entity Framework Core, SQL Server
- **Frontend:** ASP.NET Core MVC, Tailwind CSS (CDN), Material Symbols
- **Mimari:** Repository Pattern, Generic Repository, AutoMapper, DTO Pattern
- **API Dokümantasyonu:** Swagger / Swashbuckle
- **HTTP İstemcisi:** IHttpClientFactory (Named Client)

---

*Bu proje, .NET ile çok katmanlı mimari, Repository Pattern ve modern frontend entegrasyonu öğrenme/portfolyo amacıyla geliştirilmiştir.*
