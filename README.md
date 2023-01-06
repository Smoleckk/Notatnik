# Notatnik - aplikacja internetowa do przechowywania szyfrowanych notatek
Ta aplikacja internetowa pozwala na przechowywanie notatek w sposób szyfrowany,
dzięki czemu mogą być one bezpiecznie przechowywane i udostępniane. Aplikacja została zaprojektowana z myślą
o prostocie obsługi i szybkim dostępie do notatek. Możliwe jest również dodawanie notatek w formacie markdown,
co umożliwia lepsze formatowanie tekstu i dodawanie elementów takich jak linki czy listy.

### Wymagania
Aby skorzystać z tej aplikacji, potrzebujesz:
* Docker

### Instalacja
Aby uruchomić aplikację za pomocą Dockera, należy:
1. Sklonowac repozytorium:
-git clone https://github.com/Smoleckk/Notatnik.git
2. Przejść do folderu z apliakcja:
-cd Notatnik/Server
2. Zbudowac obraz za pomoca komedy:
- docker-compose build
3. Uruchomić kontener za pomocą komendy:
- docker-compose up
4. Dostęp do aplikacji będziemy mieli na porcie 5000
- https://localhost:5000/login
5. Na koniec możemy wyłączyc kontener:
- docker-compose down


### Użytkowanie
Po założeniu konta i zalogowaniu się do aplikacji, możesz zacząć tworzyć i przechowywać swoje szyfrowane notatki.
Aby utworzyć nową notatkę, wystarczy kliknąć na przycisk "Nowa notatka" i wprowadzić tekst do pola tekstowego.
W zależności od wyboru sposobu przechowywania notatki, będzie ona szyfrowana i przechowywana w bezpieczny sposób.

### Technologie
Aplikacja została napisana w języku C# z wykorzystaniem framework'u .NET 6 API oraz technologii Blazor do tworzenia aplikacji internetowych.
Do przechowywania danych wykorzystujemy bazę danych MS SQL, a całość jest zcontainerowana za pomocą Dockera.

### Bezpieczeństwo
Bezpieczeństwo twoich notatek jest dla nas priorytetem. Dlatego wszystkie notatki są szyfrowane za pomocą silnego algorytmu szyfrującego,
a hasła są przechowywane w sposób zabezpieczony przed wyciekiem.

##### Sposób zabezpieczenia aplikacji:
* bezpieczne połączenie z aplikacją poprzez protokół https,
* wszystkie dane wejściowe od użytkownika podlegają walidacji z negatywnym nastawieniem,
* weryfikowany jest dostęp użytkowników do zasobów,
* weryfikacja liczby nieudanych prób logowania,
* sprawdzanie jakości hasła poprzez entropie,
* opóźnienie podczas logowania po nieudanych probach,
* ograniczone informowanie o błędach,
* bezpieczne przechowywanie hasła, wykorzystanie algorytmu skrótu szyfrowania SHA-512

##### Sposób zabezpieczenia notatek:
* wykorzysatnie szyfrowania za pomoca AES z trybem CBC
