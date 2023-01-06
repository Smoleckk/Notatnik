# Notatnik - aplikacja internetowa do przechowywania szyfrowanych notatek
Ta aplikacja internetowa pozwala na przechowywanie notatek w sposób szyfrowany,
dzięki czemu mogą być one bezpiecznie przechowywane i udostępniane. Aplikacja została zaprojektowana z myślą
o prostocie obsługi i szybkim dostępie do notatek. Możliwe jest również dodawanie notatek w formacie markdown,
co umożliwia lepsze formatowanie tekstu i dodawanie elementów takich jak linki czy listy.

### Wymagania
Aby skorzystać z tej aplikacji, potrzebujesz:

* Dostępu do Internetu
* Przeglądarki internetowej obsługującej JavaScript
* Zainstalowanego Dockera na swoim komputerze (https://www.docker.com/)

### Instalacja
Aby uruchomić aplikację za pomocą Dockera, należy:
1. Sklonowac repozytorium:
git clone https://github.com/Smoleckk/Notatnik.git
2. Build the Docker image:
cd Notatnik/Server
docker build -t secure-notes-app .
Run the Docker container:


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
