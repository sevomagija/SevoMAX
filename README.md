# 🎬 ŠEVOMAX - Katalog Filmova i Serija

**ŠEVOMAX** je moderna web aplikacija za upravljanje personalizovanim katalogom filmova i serija. Projekat je nastao sa ciljem da spoji funkcionalnost baze podataka sa modernim "Dark Mode" dizajnom, pružajući korisniku iskustvo slično popularnim streaming platformama.

---


## 📖 Opis projekta
Ova aplikacija predstavlja kompletan sistem za katalogizaciju video sadržaja. Glavna specifičnost projekta je korišćenje **XML persistencije podataka**. Umesto klasičnih SQL baza, podaci se čuvaju u strukturiranim XML fajlovima, što omogućava da aplikacija bude potpuno portabilna – može se pokrenuti na bilo kom računaru bez potrebe za konfigurisanjem database servera.

---

## 🛠 Ključne Tehnologije

| Tehnologija | Namena |
| **ASP.NET MVC** | Arhitektura aplikacije i back-end logika |
| **C# (.NET 4.7.2)** | Programski jezik za obradu podataka |
| **XML** | Skladištenje podataka (NoSQL pristup) |
| **Bootstrap 5** | Osnova za responzivni dizajn |
| **Custom CSS3** | Unikatni Dark Mode stilovi i animacije |
| **jQuery** | Dinamička interakcija i validacija formi |

---

## ✨ Funkcionalnosti

### 🎥 Filmovi i Serije
* **CRUD Operacije:** Potpuno upravljanje (dodavanje, pregled, izmena i brisanje) sadržaja.
* **Napredna Pretraga:** Pronalaženje naslova po ključnim rečima.
* **Galerija:** Pregledan vizuelni prikaz postera filmova.
* **Upload sistema:** Mogućnost dodavanja slika za svaki film ili seriju lokalno.

### 🔐 Korisnički Sistem
* **Autentifikacija:** Registracija novih i prijava postojećih korisnika.
* **Session Management:** Zaštita određenih delova sajta (npr. brisanje sadržaja) samo za ulogovane korisnike.
* **Profil:** Pregled i ažuriranje osnovnih informacija o korisniku.

---

## 📂 Struktura Projekta


FilmSerija/
├── App_Data/          # XML fajlovi koji služe kao baza podataka
├── App_Start/         # Konfiguracija ruta i bundle-ova
├── Controllers/       # Kontroleri (Account, Film, Serija, Home)
├── Models/            # Definicije objekata i biznis logika
├── Services/          # Servisni sloj za direktan rad sa XML-om
├── Views/             # CSHTML stranice (Razor engine)
└── Content/           # CSS stilovi i lokalno sačuvane slike


🚀 Instalacija i Pokretanje
Preduslovi: Instaliran Visual Studio 2019/2022 i .NET Framework 4.7.2.

Kloniranje:

Bash
git clone [https://github.com/tvoj-username/FilmSerija.git](https://github.com/sevomagija/SevoMAX.git)
Pokretanje: Otvorite FilmSerija.sln, sačekajte da Visual Studio učita zavisnosti i pritisnite F5.
Napomena: Nije potrebno podešavanje baze podataka, sve radi "out-of-the-box".

📊 Modeli Podataka
🎞 Film / Serija
Id: Jedinstveni broj unosa

Naslov: Naziv filma ili serije

Zanr: Kategorija sadržaja

Godina: Godina izdanja/početka emitovanja

PutanjaSlike: Relativni link ka posteru u sistemu

👨‍💻 Autor
Vukašin Ševo

🎓 Učenik tehničke škole
